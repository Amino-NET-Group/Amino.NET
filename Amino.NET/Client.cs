﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amino.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace Amino
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Client
    {
        //Values the Amino Client can hold
        /// <summary>
        /// Represents the current device ID used by the current Client
        /// </summary>
        public string DeviceId { get; private set; }
        /// <summary>
        /// Represents the current Session ID of the current Client / Amino account instance
        /// </summary>
        public string SessionId { get; private set; }
        /// <summary>
        /// Represents the Login Secret of the current Amino account
        /// </summary>
        public string Secret { get; private set; }
        /// <summary>
        /// Represents the current user ID (object ID) of the current Amino account
        /// </summary>
        public string UserId { get; private set; }
        /// <summary>
        /// Represents the full JSON response of the current Amino account
        /// </summary>
        public string Json { get; private set; }
        /// <summary>
        /// Represents the google ID of the current Amino account
        /// </summary>
        public string GoogleId { get; private set; }
        /// <summary>
        /// Represents the apple ID of the current Amino account
        /// </summary>
        public string AppleId { get; private set; }
        /// <summary>
        /// Represents the facebook ID of the current Amino account
        /// </summary>
        public string FacebookId { get; private set; }
        /// <summary>
        /// Represents the twitter ID of the current Amino account
        /// </summary>
        public string TwitterId { get; private set; }
        /// <summary>
        /// Represents the Icon image URL of the current Amino account
        /// </summary>
        public string IconUrl { get; private set; }
        /// <summary>
        /// Represents the amino ID of the current Amino account
        /// </summary>
        public string AminoId { get; private set; }
        /// <summary>
        /// Represents the email address of the current Amino account
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// Represents the phone number of the current Amino account
        /// </summary>
        public string PhoneNumber { get; private set; }
        /// <summary>
        /// Represents the nickname of the current Amino account
        /// </summary>
        public string Nickname { get; private set; }
        /// <summary>
        /// Represents if the current Amino accounts profile is profile or not
        /// </summary>
        public bool IsGlobal { get; private set; }
        /// <summary>
        /// Represents the current Clients debug state, if put to true, all API responses and webSocket messages get printed to Trace
        /// </summary>
        public bool Debug { get; set; } = false;


        private SubClient subClient = null;
        private string userAgent;


        //The value to access the websocket manager
        private Amino.WebSocketHandler webSocket;
        //Events
        /// <summary>
        /// Fires each time an Amino Text Message has been received by the current Amino account
        /// </summary>
        public event Action<Objects.Message> onMessage;
        /// <summary>
        /// Fires each time an Amino Image Message has been received by the current Amino account
        /// </summary>
        public event Action<Objects.ImageMessage> onImageMessage;
        /// <summary>
        /// Fires each time an Amino WebSocket Message has been receveived by the current Amino Client
        /// </summary>
        public event Action<string> onWebSocketMessage;
        /// <summary>
        /// Fires each time an Amino YouTube message has been received by the current Amino account
        /// </summary>
        public event Action<Objects.YouTubeMessage> onYouTubeMessage;
        /// <summary>
        /// Fires each time an Amino Voice message / note has been received by the current Amino account
        /// </summary>
        public event Action<Objects.VoiceMessage> onVoiceMessage;
        /// <summary>
        /// Fires each time an Amino Sticker message has been received by the current Amino account
        /// </summary>
        public event Action<Objects.StickerMessage> onStickerMessage;
        /// <summary>
        /// Fires each time an Amino message has been deleted (in a chat where the current Amino account is in)
        /// </summary>
        public event Action<Objects.DeletedMessage> onMessageDeleted;
        /// <summary>
        /// Fires each time an Amino member joined a chat where the current Amino account is in
        /// </summary>
        public event Action<Objects.JoinedChatMember> onChatMemberJoin;
        /// <summary>
        /// Fires each time an Amino member left a chat where the current Amino account is in
        /// </summary>
        public event Action<Objects.LeftChatMember> onChatMemberLeave;
        /// <summary>
        /// Fires each time an Amino chat background has changed (only chats where the current Amino account is in)
        /// </summary>
        public event Action<Objects.ChatEvent> onChatBackgroundChanged;
        /// <summary>
        /// Fires each time an Amino chat title has been changed (only chats where the current Amino account is in)
        /// </summary>
        public event Action<Objects.ChatEvent> onChatTitleChanged;
        /// <summary>
        /// Fires each time an Amino chat ddescription has been changed (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatEvent> onChatContentChanged;
        /// <summary>
        /// Fires each time an Amino chat Announcement has been pinned (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatAnnouncement> onChatAnnouncementPin;
        /// <summary>
        /// Fires each time an Amino Chat Announcement has been removed from pin (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatEvent> onChatAnnouncementUnpin;
        /// <summary>
        /// Fires each time an Amino Chat has been put on View Mode (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ViewMode> onChatViewModeOn;
        /// <summary>
        /// Fires each time an Amino Chat has been put out of View Mode (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ViewMode> onChatViewModeOff;
        /// <summary>
        /// Fires each time an Amino Chat has enabled chat tipping (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatTipToggle> onChatTipEnabled;
        /// <summary>
        /// Fires each time an Amino Chat has disabled chat tipping (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatTipToggle> onChatTipDisabled;
        /// <summary>
        /// Fires each time an Amino Chat Message has been removed by a moderator of the current Community (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.SpecialChatEvent> onMessageForceRemovedByAdmin;
        /// <summary>
        /// Fires each time someone has tipped coins in an Amino Chat (only in chats where the currnt Amino account is in)
        /// </summary>
        public event Action<Objects.ChatTip> onChatTip;

        //headers.
        public Dictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", DeviceId);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.aminoapps.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            headers.Add("User-Agent", userAgent);
            headers.Add("AUID", UserId);
            if (SessionId != null) { headers.Add("NDCAUTH", $"sid={SessionId}"); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates an instance of the Amino.Client object and builds headers
        /// <para>This object can hold a deviceId, if left empty, it will generate one.</para>
        /// </summary>
        /// <param>This object can hold a deviceId, if left empty, it will generate one.</param>
        /// <param name="_deviceID">The Device ID of your Account, can remain null</param>
        /// <param name="userAgent">The User Agent of your client, default: latest compatible</param>
        public Client(string _deviceID = null, string userAgent = "Apple iPhone13,4 iOS v15.6.1 Main/3.12.9", string auid = null)
        {
            this.DeviceId = (_deviceID == null) ? helpers.generate_device_id() : _deviceID;
            this.UserId = auid == null ? Guid.NewGuid().ToString() : auid;
            this.userAgent = userAgent;
            headerBuilder();
        }

        /// <summary>
        /// Allows you to request a verification code for amino
        /// <para>This function requires an email address, to reset a password, put the resetPassword alue to true</para>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        public Task request_verify_code(string email, bool resetPassword = false)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/request-security-validation", Method.Post);
                JObject data = new JObject()
                {
                    { "identity", email },
                    { "deviceID", this.DeviceId },
                    { "type", 1 }
                };
                if (resetPassword)
                {
                    data.Add("level", 2);
                    data.Add("purpose", "reset-password");
                }
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (Debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows you to log into your account
        /// <para>This function will set all client values and wíll enable the webSocket to activate</para>
        /// <para>Successfully calling this function will enable the Client events</para>
        /// </summary>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_secret"></param>
        /// <param name="connectSocket">Determines if the WebSocket should be connected, will disable Events if false</param>
        /// <returns></returns>
        public Task login(string _email, string _password = null, string _secret = null, bool connectSocket = true)
        {
            JObject data = new JObject
                {
                    { "email", _email },
                    { "v", 2 },
                    { "secret", (_secret == null) ? $"0 {_password}" : _secret },
                    { "deviceID", DeviceId },
                    { "clientType", 100 },
                    { "action", "normal" },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/auth/login", Method.Post);
            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            Json = response.Content;
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(Json);
            SessionId = (string)jsonObj["sid"];
            this.Secret = (string)jsonObj["secret"];
            UserId = (string)jsonObj["account"]["uid"];
            GoogleId = (string)jsonObj["account"]["googleID"];
            AppleId = (string)jsonObj["account"]["appleID"];
            FacebookId = (string)jsonObj["account"]["facebookID"];
            TwitterId = (string)jsonObj["account"]["twitterID"];
            IconUrl = (string)jsonObj["userProfile"]["icon"];
            AminoId = (string)jsonObj["account"]["aminoId"];
            Email = (string)jsonObj["account"]["email"];
            PhoneNumber = (string)jsonObj["account"]["phoneNumber"];
            Nickname = (string)jsonObj["userProfile"]["nickname"];
            IsGlobal = (bool)jsonObj["userProfile"]["isGlobal"];
            headerBuilder();
            if (connectSocket)
            {
                Amino.WebSocketHandler _webSocket = new WebSocketHandler(this);
                this.webSocket = _webSocket;
            }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to log out of the current account
        /// <para>A successful function call will clear the Client values and close the webSocket connection</para>
        /// </summary>
        /// <returns></returns>
        public Task logout()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                JObject data = new JObject()
                {
                    { "deviceID", this.DeviceId },
                    { "clientType", 100 },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/logout");
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (Debug) { Trace.WriteLine(response.Content); }
                SessionId = null;
                Secret = null;
                UserId = null;
                Json = null;
                GoogleId = null;
                AppleId = null;
                FacebookId = null;
                TwitterId = null;
                IconUrl = null;
                AminoId = null;
                Email = null;
                PhoneNumber = null;
                Nickname = null;
                IsGlobal = false;
                headerBuilder();
                _ = webSocket.DisconnectSocket();
                if (subClient != null)
                {
                    subClient.Dispose();
                    subClient = null;
                }

                return Task.CompletedTask;

            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to log into your Amino Account using a Session Token
        /// </summary>
        /// <param name="sessionId">The Session token of your Account</param>
        /// <param name="fetchProfile">Determines if the profile data should be fetched</param>
        /// <param name="connectSocket">Determines if the WebSocket should connect</param>
        /// <returns></returns>
        public Task login_sid(string sessionId, bool fetchProfile = true, bool connectSocket = true)
        {
            this.SessionId = sessionId;
            this.UserId = helpers.sid_to_uid(sessionId);
            headerBuilder();
            if (fetchProfile)
            {
                Objects.UserAccount currentAccount = get_account_info();

                this.UserId = currentAccount.UserId;
                this.GoogleId = currentAccount.GoogleId;
                this.AppleId = currentAccount.AppleId;
                this.TwitterId = currentAccount.TwitterId;
                this.IconUrl = currentAccount.IconUrl;
                this.AminoId = currentAccount.AminoId;
                this.Email = currentAccount.Email;
                this.PhoneNumber = currentAccount.PhoneNumber;
                this.Nickname = currentAccount.Nickname;
                this.IsGlobal = true;

            }
            headerBuilder();
            if (connectSocket)
            {
                Amino.WebSocketHandler _webSocket = new WebSocketHandler(this);
                this.webSocket = _webSocket;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to register an Amino account using a google account
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="googleToken"></param>
        /// <param name="password"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task register_google(string nickname, string googleToken, string password, string deviceId = null)
        {
            deviceId = deviceId == null ? helpers.generate_device_id() : deviceId;
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/auth/login");
            request.AddHeaders(headers);

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "secret", $"32 {googleToken}" },
                { "secret2", $"0 {password}" },
                { "deviceID", deviceId },
                { "clientType", 100 },
                { "nickname", nickname },
                { "latitude", 0 },
                { "longitude", 0 },
                { "address", null },
                { "clientCallbackURL", "narviiapp://relogin" },
                { "timestamp", helpers.GetTimestamp() * 1000 },
            };

            request.AddJsonBody(System.Text.Json.JsonSerializer.Serialize(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));

            var response = client.ExecutePost(request);
            if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if(Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        


        /// <summary>
        /// Allows you to register an Amino account
        /// <para>This function requires a verification code, refer to <b>request_verify_code()</b></para>
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_verificationCode"></param>
        /// <param name="_deviceID"></param>
        /// <returns></returns>
        public Task register(string _name, string _email, string _password, string _verificationCode, string _deviceID = null)
        {
            if (_deviceID == null) { if (DeviceId != null) { _deviceID = DeviceId; } else { _deviceID = helpers.generate_device_id(); } }
            JObject data = new JObject()
                {
                    { "secret", $"0 {_password}" },
                    { "deviceID", _deviceID },
                    { "email", _email },
                    { "clientType", 100 },
                    { "nickname", _name },
                    { "latitude", 0 },
                    { "longtitude", 0 },
                    { "address", String.Empty },
                    { "clientCallbackURL", "narviiapp://relogin" },
                    { "validationContext", new JObject()
                        {
                            { "data", new JObject() { {"code", _verificationCode } } },
                            { "type", 1 },
                            { "identity", _email }
                        }
                    },
                    { "type", 1 },
                    { "identity", _email },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/auth/register");
            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;

        }

        /// <summary>
        /// Allows you to restore an Amino account
        /// </summary>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_deviceID"></param>
        /// <returns></returns>
        public Task restore_account(string _email, string _password, string _deviceID = null)
        {

            try
            {
                JObject data = new JObject()
                {
                    { "secret", $"0 {_password}" },
                    { "deviceID", (_deviceID == null) ? helpers.generate_device_id() : _deviceID },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/delete-request/cancel");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (Debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to delete the current Amino account
        /// <para>This function requires a verification code, refer to <b>request_verify_code()</b></para>
        /// </summary>
        /// <param name="_password"></param>
        /// <returns></returns>
        public Task delete_account(string _password)
        {
            JObject data = new JObject() { { "deviceID", DeviceId }, { "secret", $"0 {_password}" } };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/account/delete-request");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
            request.AddJsonBody(data);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            SessionId = null;
            Secret = null;
            UserId = null;
            Json = null;
            GoogleId = null;
            AppleId = null;
            FacebookId = null;
            TwitterId = null;
            IconUrl = null;
            AminoId = null;
            Email = null;
            PhoneNumber = null;
            Nickname = null;
            IsGlobal = false;
            headerBuilder();
            _ = webSocket.DisconnectSocket();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to activate the current Amino account
        /// <para>This function requires a verification code, refer to <b>request_verify_code()</b></para>
        /// </summary>
        /// <param name="_email"></param>
        /// <param name="_verificationCode"></param>
        /// <param name="_deviceID"></param>
        /// <returns></returns>
        public Task activate_account(string _email, string _verificationCode, string _deviceID = null)
        {
            if (_deviceID == null) { if (DeviceId != null) { _deviceID = DeviceId; } else { _deviceID = helpers.generate_device_id(); } }

            JObject data = new JObject()
            {
                { "type", 1 },
                { "identity", _email },
                { "data", new JObject() { "code", _verificationCode } },
                { "deviceId", _deviceID }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/auth/activate-email");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to configure the current Amino account
        /// </summary>
        /// <param name="_gender"></param>
        /// <param name="_age"></param>
        /// <returns></returns>
        public Task configure_account(Types.account_gender _gender = Types.account_gender.Non_Binary, int _age = 18)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (_age <= 12) { throw new Exception("The given account age is too low"); }
            JObject data = new JObject()
            {
                { "age", _age },
                { "gender", (int)_gender },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };

            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/persona/profile/basic");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to change the password of the current Amino account
        /// </summary>
        /// <param name="_email"></param>
        /// <param name="_password"></param>
        /// <param name="_verificationCode"></param>
        /// <returns></returns>
        public Task change_password(string _email, string _password, string _verificationCode)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
            {
                { "updateSecret", $"0 {_password}" },
                { "emailValidationContext", new JObject()
                    {
                        { "data", new JObject() { "code", _verificationCode } },
                        { "type", 1 },
                        { "identity", _email },
                        { "level", 2 },
                        { "deviceID", this.DeviceId }
                    }
                },
                { "phoneNumberValidationContext", String.Empty },
                { "deviceID", this.DeviceId }
            };

            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/reset-password");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (Debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get information about a user Profile
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>Object : Amino.Objects.GlobalProfile</returns>
        public Amino.Objects.GlobalProfile get_user_info(string _userId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{_userId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            Amino.Objects.GlobalProfile profile = System.Text.Json.JsonSerializer.Deserialize<GlobalProfile>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userAccount").GetRawText());
            return profile;
        }

        /// <summary>
        /// Allows you to check if a Device ID is valid
        /// </summary>
        /// <param name="_deviceId">The Device ID you want to check</param>
        /// <returns>bool : true / false</returns>
        public bool check_device(string _deviceId)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            JObject data = new JObject
            {
                { "deviceID", _deviceId },
                { "bundleID", "com.narvii.amino.master" },
                { "clientType", 100 },
                { "systemPushEnabled", true },
                { "timezone", 0 },
                { "locale", currentCulture.Name },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };

            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/device");
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { return false; }
                if (Debug) { Trace.WriteLine(response.Content); }
                return true;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the event log of the current Amino account
        /// </summary>
        /// <returns>Object : Amino.Objects.EventLog</returns>
        public Amino.Objects.EventLog get_event_log()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/eventlog/profile?language=en");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            Amino.Objects.EventLog eventLog = System.Text.Json.JsonSerializer.Deserialize<EventLog>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
            return eventLog;

        }


        /// <summary>
        /// Allows you to get a list of Communities the current Amino account is in
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.Community</returns>
        public List<Objects.Community> get_subClient_communities(int start = 0, int size = 25)
        {

            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/community/joined?v=1&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }

            return System.Text.Json.JsonSerializer.Deserialize<List<Community>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("communityList").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of community profiles of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.CommunityProfile</returns>
        public List<Objects.GenericProfile> get_subClient_profiles(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/community/joined?v=1&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }

            return System.Text.Json.JsonSerializer.Deserialize<List<GenericProfile>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userInfoInCommunities").GetRawText());
        }

        /// <summary>
        /// Allows you to get information about the current Amino account
        /// </summary>
        /// <returns>Object : Amino.Objects.UserAccount</returns>
        public Objects.UserAccount get_account_info()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/account");
            request.AddHeaders(headers);
            request.AddHeader("SMDEVICEID", Guid.NewGuid().ToString());
            request.AddOrUpdateHeader("Content-Type", "application/x-www-form-urlencoded");
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<UserAccount>(JsonDocument.Parse(response.Content).RootElement.GetProperty("account").GetRawText());

        }
        /// <summary>
        /// Allows you to get a list of chat threads the current Amino account is in 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.Chat</returns>
        public List<Objects.Chat> get_chat_threads(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread?type=joined-me&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }


            return System.Text.Json.JsonSerializer.Deserialize<List<Chat>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("threadList").GetRawText());

        }

        /// <summary>
        /// Allows you to get a single chat thread using a chatId
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns>Object : Amino.Objects.Chat</returns>
        public Objects.Chat get_chat_thread(string chatId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }

            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<Chat>(JsonDocument.Parse(response.Content).RootElement.GetProperty("thread").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of users within a chat thread
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.ChatMember</returns>
        public List<Objects.ChatMember> get_chat_users(string chatId, int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member?start={start}&size={size}&type=default&cv=1.2");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<ChatMember>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("memberList").GetRawText());
        }
        /// <summary>
        /// Allows you to join a chat with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task join_chat(string chatId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{UserId}");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to leave a chat with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task leave_chat(string chatId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{UserId}");
            request.AddHeaders(headers);
            request.Method = Method.Delete;
            var response = client.Execute(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to invite one or multiple members into a chat thread with the current Amino account
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task invite_to_chat(string[] userIds, string chatId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject();
            data.Add("timestamp", (Math.Round(helpers.GetTimestamp())) * 1000);
            data.Add("uids", JToken.FromObject(new { userIds }));
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/invite");
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));

            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to kick someone from a chat thread with the current Amino account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <param name="allowRejoin"></param>
        /// <returns></returns>
        public Task kick_from_chat(string userId, string chatId, bool allowRejoin = true)
        {
            int _allowRejoin;
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (allowRejoin) { _allowRejoin = 1; } else { _allowRejoin = 0; }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{userId}?allowRejoin={_allowRejoin}");
            request.AddHeaders(headers);
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to get a list of chat messages of a specific chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="size"></param>
        /// <param name="pageToken"></param>
        /// <returns>List : Amino.Objects.MessageCollection</returns>
        public List<Objects.MessageCollection> get_chat_messages(string chatId, int size = 25, string pageToken = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string endPoint;
            if (!string.IsNullOrEmpty(pageToken) || !string.IsNullOrWhiteSpace(pageToken)) { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&pageToken={pageToken}&size={size}"; } else { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&size={size}"; }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest(endPoint);
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<MessageCollection>>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to search a community based on its ID (aminoId <b>not</b> objectId) and get information about it"/>
        /// </summary>
        /// <param name="aminoId"></param>
        /// <returns>List : Amino.Objects.CommunityInfo</returns>
        public List<Objects.CommunityInfo> search_community(string aminoId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/search/amino-id-and-link?q={aminoId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<CommunityInfo>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("resultList").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of users a person is following
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.UserFollowings</returns>
        public List<Objects.UserProfile> get_user_following(string userId, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/joined?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<UserProfile>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userProfileList").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of users that follow a person
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Obejcts.UserFollowings</returns>
        public List<Objects.UserProfile> get_user_followers(string userId, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/member?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<UserProfile>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userProfileList").GetRawText());

        }

        /// <summary>
        /// Allows you to get a list of users that visited a persons Amino account page
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.UserVisitor</returns>
        public List<Objects.UserVisitor> get_user_visitors(string userId, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/visitors?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<UserVisitor>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("visitors").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of users the current Amino account has blocked
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.BlockedUser</returns>
        public List<Objects.GenericProfile> get_blocked_users(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/block?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.StatusCode); }
            return System.Text.Json.JsonSerializer.Deserialize<List<GenericProfile>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userProfileList").GetRawText());
        }

        /// <summary>
        /// Allows you to get a list of userIds of the users that have blocked the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : string</returns>
        public List<string> get_blocker_users(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            List<string> blockerUserList = new List<string>();
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/block/full-list?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("blockedUidList").GetRawText());

        }

        /// <summary>
        /// Allows you to get a list of wall comments on a persons Amino Profile
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.Comment</returns>
        public List<Objects.Comment> get_wall_comments(string userId, Types.Sorting_Types type, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            List<Objects.Comment> commentList = new List<Objects.Comment>();
            string _sorting_type;
            switch (type)
            {
                case Types.Sorting_Types.Newest:
                    _sorting_type = "newest";
                    break;
                case Types.Sorting_Types.Oldest:
                    _sorting_type = "oldest";
                    break;
                case Types.Sorting_Types.Top:
                    _sorting_type = "vote";
                    break;
                default:
                    _sorting_type = "newest";
                    break;
            }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/g-comment?sort={_sorting_type}&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("commentList").GetRawText());
        }

        /// <summary>
        /// Allows you to flag a post with or without the current Amino account
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="flagType"></param>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <param name="asGuest"></param>
        /// <returns></returns>
        public Task flag(string reason, Types.Flag_Types flagType, Types.Flag_Targets targetType, string targetId, bool asGuest)
        {
            if (!asGuest) { if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); } }
            string _flag = asGuest ? "g-flag" : "flag";
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/{_flag}");
            var data = new
            {
                flagType = (int)flagType,
                message = reason,
                timestamp = helpers.GetTimestamp() * 1000,
                objectId = targetId,
                objetType = (int)targetType
            };
            request.AddHeaders(headers);
            request.AddJsonBody(data);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to delete a message in a chat thread with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="messageId"></param>
        /// <param name="asStaff"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public Task delete_message(string chatId, string messageId, bool asStaff = false, string reason = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
                {
                    { "adminOpName", 102 },
                    { "adminOpNote", new JObject() { { "content", reason } } },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message/{messageId}");
            if (asStaff) { request.Resource = request.Resource + "/admin"; }
            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Allows you to mark a message as read with the current Amino account
        /// </summary>
        /// <param name="_chatId"></param>
        /// <param name="_messageId"></param>
        /// <returns></returns>
        public Task mark_as_read(string _chatId, string _messageId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
            {
                { "messageId", _messageId },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{_chatId}/mark-as-read");
            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to visit a users Amino profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task visit(string userId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}?action=visit");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to follow a user with the current Amino account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task follow_user(string userId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/member");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to unfollow a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task unfollow_user(string _userId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{_userId}/member/{UserId}");
            request.AddHeaders(headers);
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Allows you to block a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task block_user(string _userId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/block/{_userId}");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to unblock a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task unblock_user(string _userId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/block/{_userId}");
            request.AddHeaders(headers);
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to join a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public Task join_community(string communityId, string invitationCode = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject();
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            if (invitationCode != null) { data.Add("invitationId", invitationCode); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/x{communityId}/s/community/join");
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to request membership to a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task join_community_request(string communityId, string message = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/x{communityId}/s/community/membership-request");
            var data = new JObject()
            {
                { "message", message },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to leave a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task leave_community(string communityId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/s/community/leave");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to flag a community usign the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="reason"></param>
        /// <param name="flagType"></param>
        /// <param name="asGuest"></param>
        /// <returns></returns>
        public Task flag_community(string communityId, string reason, Types.Flag_Types flagType, bool asGuest = false)
        {
            if (!asGuest) { if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); } }
            int _flagType;
            string _flag = asGuest ? "g-flag" : "flag";
            JObject data = new JObject()
            {
                { "objectId", communityId },
                { "objectType", 16 },
                { "flagType", (int)flagType },
                { "message", reason },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/x{communityId}/s/{_flag}");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;

        }

        /// <summary>
        /// Allows you to upload media files to the Amino servers
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns>string : The URL to the media file you just uploaded</returns>
        public string upload_media(string filePath, Types.upload_File_Types type)
        {
            if(filePath.StartsWith("http"))
            {
                byte[] fileBytes = new HttpClient().GetAsync(filePath).Result.Content.ReadAsByteArrayAsync().Result;
                return upload_media(fileBytes, type);
            }
            return upload_media(File.ReadAllBytes(filePath), type);
        }

        /// <summary>
        /// Allows you to upload media files to the Amino servers
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns>string : The URL to the media file you just uploaded</returns>
        public string upload_media(byte[] file, Types.upload_File_Types type)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _fileType;
            switch (type)
            {
                case Types.upload_File_Types.Audio:
                    _fileType = "audio/aac";
                    break;
                case Types.upload_File_Types.Image:
                    _fileType = "image/jpg";
                    break;
                case Types.upload_File_Types.Gif:
                    _fileType = "image/gif";
                    break;
                default:
                    _fileType = "image/jpg";
                    break;
            }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/media/upload");
            request.AddHeaders(headers);
            request.AddOrUpdateHeader("Content-Type", _fileType);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_file_signiture(file));
            request.AddBody(file, _fileType);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
            return jsonObj["mediaValue"];

        }
        /// <summary>
        /// Allows you to edit the profile page of the current Amino account
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="content"></param>
        /// <param name="icon"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="backgroundMediaUrl"></param>
        /// <param name="defaultChatbubbleId"></param>
        /// <returns></returns>
        public Task edit_profile(string nickname = null, string content = null, byte[] icon = null, string backgroundColor = null, string backgroundMediaUrl = null, string defaultChatbubbleId = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject();
            data.Add("address", null);
            data.Add("latitude", 0);
            data.Add("longtitude", 0);
            data.Add("mediaList", null);
            data.Add("eventSource", "UserProfileView");
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            if (nickname != null) { data.Add("nickname", nickname); }
            if (icon != null) { data.Add("icon", upload_media(icon, Types.upload_File_Types.Image)); }
            if (content != null) { data.Add("content", content); }
            if (backgroundColor != null)
            {

                if (data["extensions"] != null)
                {
                    JObject subDataColor = (JObject)data["extensions"]["style"];
                    subDataColor.Add(new JObject(new JProperty("backgroundColor", backgroundColor)));
                    data.AddAnnotation(subDataColor);
                }
                else
                {
                    data.Add(new JProperty("extensions", new JObject(new JProperty("style", new JObject(new JProperty("backgroundColor", backgroundColor))))));
                }
            }
            if (backgroundMediaUrl != null)
            {
                var jsonArray = new object[][]
                {
                    new object[]
                    {
                        100,
                        backgroundMediaUrl,
                        null,
                        null,
                        null
                    }
                };
                if (data["extensions"] != null)
                {

                    //100, backgroundColor, null, null, null
                    JObject subDataMedia = (JObject)data["extensions"]["style"];
                    subDataMedia.Add(new JProperty("backgroundMediaList", JArray.FromObject(jsonArray)));
                    data.AddAnnotation(subDataMedia);
                }
                else
                {
                    data.Add(new JProperty("extensions", new JObject(new JProperty("style", new JObject(new JProperty("backgroundMediaList", JArray.FromObject(jsonArray)))))));
                }

            }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"g/s/user-profile/{UserId}");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to set the privacy status of the current Amino account
        /// </summary>
        /// <param name="isAnonymous"></param>
        /// <param name="getNotifications"></param>
        /// <returns></returns>
        public Task set_privacy_status(bool isAnonymous = false, bool getNotifications = true)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            int _privacyMode = isAnonymous ? 2 : 1;
            int _notificationStatus = getNotifications ? 2 : 1;
            JObject data = new JObject()
            {
                { "timestamp", helpers.GetTimestamp() * 1000 },
                { "notificationStatus", _notificationStatus },
                { "privacyMode", _privacyMode }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/account/visit-settings");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to set the Amino ID of the current Amino account
        /// </summary>
        /// <param name="aminoId"></param>
        /// <returns></returns>
        public Task set_amino_id(string aminoId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
            {
                { "timestamp", helpers.GetTimestamp() * 1000 },
                { "aminoId", aminoId }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/account/change-amino-id");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Adds a linked community to the current Amino profile
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task add_linked_community(int communityId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{UserId}/linked-community/{communityId}");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes a linked community from the current Amino profile
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task remove_linked_community(int communityId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile/{UserId}/linked-community/{communityId}");
            request.AddHeaders(headers);
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to comment on a wall / post using the current Amino account
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public Task comment(string message, Types.Comment_Types type, string objectId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _eventSource;
            bool _isReply = false;
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest();
            request.AddHeaders(headers);
            JObject data = new JObject();
            data.Add("content", message);
            data.Add("stickerId", null);
            data.Add("type", 0);
            data.Add("timestamp", helpers.GetTimestamp() * 1000);


            switch (type)
            {
                case Types.Comment_Types.User:
                    request.Resource = $"/g/s/user-profile/{objectId}/g-comment";
                    _eventSource = "UserProfileView";
                    break;
                case Types.Comment_Types.Blog:
                    request.Resource = $"/g/s/blog/{objectId}/g-comment";
                    _eventSource = "PostDetailView";
                    break;
                case Types.Comment_Types.Wiki:
                    request.Resource = $"/g/s/item/{objectId}/g-comment";
                    _eventSource = "PostDetailView";
                    break;
                case Types.Comment_Types.Reply:
                    _isReply = true;
                    _eventSource = "";
                    break;
                default:
                    request.Resource = $"/g/s/user-profile/{objectId}/g-comment";
                    _eventSource = "UserProfileView";
                    break;
            }
            if (!_isReply) { data.Add("eventSource", _eventSource); } else { data.Add("respondTo", objectId); }
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));
            request.AddJsonBody(data);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to delete a comment from a users wall / post using the current Amino account
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="type"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public Task delete_comment(string commentId, Types.Comment_Types type, string objectId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            switch (type)
            {
                case Types.Comment_Types.User:
                    request.Resource = $"/g/s/user-profile/{objectId}/g-comment/{commentId}";
                    break;
                case Types.Comment_Types.Blog:
                    request.Resource = $"/g/s/blog/{objectId}/g-comment/{commentId}";
                    break;
                case Types.Comment_Types.Wiki:
                    request.Resource = $"/g/s/item/{objectId}/g-comment/{commentId}";
                    break;
                default:
                    request.Resource = $"/g/s/user-profile/{objectId}/g-comment/{commentId}";
                    break;
            }
            request.AddHeaders(headers);
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to like a post using the current Amino account
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task like_post(string objectId, Types.Post_Types type)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _eventSource;
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest();
            switch (type)
            {
                case Types.Post_Types.Blog:
                    request.Resource = $"/g/s/blog/{objectId}/g-vote?cv=1.2";
                    _eventSource = "UserProfileView";
                    break;
                case Types.Post_Types.Wiki:
                    request.Resource = $"/g/s/item/{objectId}/g-vote?cv=1.2";
                    _eventSource = "PostDetailView";
                    break;
                default:
                    request.Resource = $"/g/s/blog/{objectId}/g-vote?cv=1.2";
                    _eventSource = "UserProfileView";
                    break;
            }
            JObject data = new JObject()
            {
                { "value", 4 },
                { "timestamp", helpers.GetTimestamp() * 1000 },
                { "eventSource", _eventSource }
            };
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to remove a like on a post using the current Amino account
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task unlike_post(string objectId, Types.Post_Types type)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest();
            request.AddHeaders(headers);

            switch (type)
            {
                case Types.Post_Types.Blog:
                    request.Resource = $"/g/s/blog/{objectId}/g-vote?eventSource=UserProfileView";
                    break;
                case Types.Post_Types.Wiki:
                    request.Resource = $"/g/s/item/{objectId}/g-vote?eventSource=PostDetailView";
                    break;
                default:
                    request.Resource = $"/g/s/blog/{objectId}/g-vote?eventSource=UserProfileView";
                    break;
            }
            var response = client.Delete(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to get the Amino+ membership status of the current Amino account
        /// </summary>
        /// <returns>Object : Amino.Objects.MembershipInfo</returns>
        public Objects.MembershipInfo get_membership_info()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/membership?force=true");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<MembershipInfo>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to get the Team Amino announcement Posts
        /// </summary>
        /// <param name="language"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Obejcts.Amino.Post</returns>
        public List<Objects.Post> get_ta_announcements(Types.Supported_Languages language = Types.Supported_Languages.English, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            string _language;
            switch (language)
            {
                case Types.Supported_Languages.English:
                    _language = "en";
                    break;
                case Types.Supported_Languages.Spanish:
                    _language = "es";
                    break;
                case Types.Supported_Languages.Portuguese:
                    _language = "pt";
                    break;
                case Types.Supported_Languages.Arabic:
                    _language = "ar";
                    break;
                case Types.Supported_Languages.Russian:
                    _language = "ru";
                    break;
                case Types.Supported_Languages.French:
                    _language = "fr";
                    break;
                case Types.Supported_Languages.German:
                    _language = "de";
                    break;
                default:
                    _language = "en";
                    break;
            }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/announcement?language={_language}&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<Post>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("blogList").GetRawText());
        }

        /// <summary>
        /// Allows you to get the current Amino accounts wallet info
        /// </summary>
        /// <returns>Object : Objects.WalletInfo</returns>
        public Objects.WalletInfo get_wallet_info()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/wallet");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<WalletInfo>(JsonDocument.Parse(response.Content).RootElement.GetProperty("wallet").GetRawText());
        }

        /// <summary>
        /// Allows you to get the wallet transaction history of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.CoinHistoryEntry</returns>
        public List<Objects.CoinHistoryEntry> get_wallet_history(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/wallet/coin/history?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<CoinHistoryEntry>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("coinHistoryList").GetRawText());
        }

        /// <summary>
        /// Allows you to get a user ID based on a device ID
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>string : the target users ID</returns>
        public string get_from_deviceId(string deviceId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/auid?deviceId={deviceId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return JsonDocument.Parse(response.Content).RootElement.GetProperty("auid").GetString();
        }

        /// <summary>
        /// Allows you to get information an Amino URL
        /// </summary>
        /// <param name="aminoUrl"></param>
        /// <returns>Object : Amino.Objects.FromCode</returns>
        public Objects.FromCode get_from_code(string aminoUrl)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/link-resolution?q={aminoUrl}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<FromCode>(JsonDocument.Parse(response.Content).RootElement.GetProperty("linkInfoV2").GetRawText());
        }

        /// <summary>
        /// Allows you to get information about an Amino ID
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <param name="communityId"></param>
        /// <returns>Object : Amino.Objects.FromId</returns>
        public Objects.FromCode get_from_id(string objectId, Amino.Types.Object_Types type, string communityId = null)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
            {
                { "objectId", objectId },
                { "targetCode", 1 },
                { "timestamp", helpers.GetTimestamp() * 1000 },
                { "objectType", (int)type }
            };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest();
            if (communityId != null)
            {
                request.Resource = $"/g/s-x{communityId}/link-resolution";
            }
            else { request.Resource = $"/g/s/link-resolution"; }
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<FromCode>(JsonDocument.Parse(response.Content).RootElement.GetProperty("linkInfoV2").GetRawText());
        }

        /// <summary>
        /// Allows you to get the supported langauges of Amino
        /// </summary>
        /// <returns>string[] : language keys</returns>
        public string[] get_supported_languages()
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/community-collection/supported-languages?start=0&size=100");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("suppotedLanguages").GetRawText()).ToArray();
        }

        /// <summary>
        /// Allows you to claim the new user coupon using the current Amino account
        /// </summary>
        /// <returns></returns>
        public Task claim_new_user_coupon()
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/coupon/new-user-coupon/claim");
            request.AddHeaders(headers);
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }


        /// <summary>
        /// Allows you to get a list of Amino users
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.UserProfile</returns>
        public List<Objects.UserProfile> get_all_users(int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/user-profile?type=recent&start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<UserProfile>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("userProfileList").GetRawText());
        }

        /// <summary>
        /// Allows you to get Information about a Community
        /// </summary>
        /// <param name="communityId">The ID of the Community you want to get info from</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.AdvancedCommunityInfo get_community_info(string communityId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s-x{communityId}/community/info?withInfluencerList=1&withTopicList=true&influencerListOrderStrategy=fansCount");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<AdvancedCommunityInfo>(JsonDocument.Parse(response.Content).RootElement.GetProperty("community").GetRawText());
        }

        /// <summary>
        /// Allows you to get Information about a Community
        /// </summary>
        /// <param name="communityId">The ID of the Community you want to get info from</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.AdvancedCommunityInfo get_community_info(int communityId)
        {
            return get_community_info(communityId.ToString());
        }

        /// <summary>
        /// Allows you to accept host / organizer of a chatroom with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Task accept_host(string chatId, string requestId)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/transfer-organizer/{requestId}/accept");
            request.AddHeaders(headers);
            var data = new { };
            request.AddJsonBody(data);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }


        /// <summary>
        /// Allows you to accept host / organizer of a chatroom with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Task accept_organizer(string chatId, string requestId)
        {
            accept_host(chatId, requestId);
            return Task.CompletedTask;
        }


        /// <summary>
        /// Allows you to get information about an Amino Invite Code
        /// </summary>
        /// <param name="inviteCode"></param>
        /// <returns>Object : Amino.Objects.FromInvite</returns>
        public Amino.Objects.FromInvite link_identify(string inviteCode)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/community/link-identify?q=http%3A%2F%2Faminoapps.com%2Finvite%2F{inviteCode}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            JObject json = JObject.Parse(response.Content);
            return System.Text.Json.JsonSerializer.Deserialize<FromInvite>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to change the wallet config of the current Amino account
        /// </summary>
        /// <param name="walletLevel"></param>
        /// <returns></returns>
        public Task wallet_config(Types.Wallet_Config_Levels walletLevel)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject()
                {
                    { "adsLevel", (int)walletLevel },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest("/g/s/wallet/ads/config");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to get a list of available Avatar Frames of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Object.AvatarFrame</returns>
        public List<Objects.AvatarFrame> get_avatar_frames(int start = 0, int size = 25)
        {
            if (SessionId == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/avatar-frame?start={start}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<AvatarFrame>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("avatarFrameList").GetRawText());
        }

        /// <summary>
        /// Allows you to invite a user to a voice chat
        /// </summary>
        /// <param name="chatId">The ID of the chat you invite the user to</param>
        /// <param name="userId">The ID of the user you invite to the chat</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task invite_to_vc(string chatId, string userId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/vvchat-presenter/invite");

            JObject data = new JObject()
                {
                    { "uid", userId },
                    { "timestamp", helpers.GetTimestamp() * 1000 }
                };

            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));

            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to send a Text message to a chat
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatId"></param>
        /// <param name="messageType"></param>
        /// <param name="replyTo"></param>
        /// <param name="mentionUserIds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.Message send_message(string message, string chatId, Types.Message_Types messageType = Types.Message_Types.General, string replyTo = null, List<string> mentionUserIds = null)
        {
            List<JObject> mentions = new List<JObject>();
            if (mentionUserIds == null) { mentionUserIds = new List<string>(); }
            else
            {
                foreach (string user in mentionUserIds)
                {
                    JObject _mention = new JObject();
                    _mention.Add("uid", user);
                    mentions.Add(_mention);
                }
            }
            message = message.Replace("<$", "").Replace("$>", "");
            JObject data = new JObject();
            data.Add("type", (int)messageType);
            data.Add("content", message);
            data.Add("clientRefId", helpers.GetTimestamp() / 10 % 1000000000);
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            data.Add("extensions", new JObject() { { "mentionedArray", new JArray(mentions) } });
            if (replyTo != null) { data.Add("replyMessageId", replyTo); }

            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<Message>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to send a media file to a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="file"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task send_file_message(string chatId, byte[] file, Types.upload_File_Types fileType)
        {
            JObject data = new JObject();
            data.Add("clientRefId", helpers.GetTimestamp() / 10 % 1000000000);
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            data.Add("content", null);
            data.Add("type", 0);

            switch (fileType)
            {
                case Types.upload_File_Types.Image:
                    data.Add("mediaType", 100);
                    data.Add("mediaUploadValueContentType", "image/jpg");
                    data.Add("mediaUhqEnabled", true);
                    break;
                case Types.upload_File_Types.Gif:
                    data.Add("mediaType", 100);
                    data.Add("mediaUploadValueContentType", "image/gif");
                    data.Add("enableUhqEnabled", true);
                    break;
                case Types.upload_File_Types.Audio:
                    data.Remove("type");
                    data.Add("type", 2);
                    data.Add("mediaType", 110);
                    break;
            }
            data.Add("mediaUploadValue", Convert.ToBase64String(file));


            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to send a media file to a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task send_file_message(string chatId, string filePath, Types.upload_File_Types fileType)
        {
            send_file_message(chatId, File.ReadAllBytes(filePath), fileType);
            return Task.CompletedTask;

        }


        public Task send_embed(string chatId, string content = null, string embedId = null, string embedLink = null, string embedTitle = null, string embedContent = null, byte[] embedImage = null)
        {

            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message");
            JObject data = new JObject
                {
                    { "type", 0 },
                    { "content", content },
                    { "clientRefId", helpers.GetTimestamp() / 10 % 1000000000 },
                    { "attachedObject", new JObject()
                        {
                            { "objectId", embedId },
                            { "objectType", null },
                            { "link", embedLink },
                            { "title", embedTitle },
                            { "content", embedContent },
                            { "mediaList", (embedImage == null) ? null : new JArray() { new JArray() { 100, this.upload_media(embedImage, Types.upload_File_Types.Image), null } } }
                        }
                    },
                    { "extensions", new JObject()
                        {
                            { "mentionedArray", new JArray() }
                        }
                    },
                    { "timestamp", helpers.GetTimestamp() }
                };


            request.AddHeaders(headers);
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        public Task send_embed(string chatId, string content = null, string embedId = null, string embedLink = null, string embedTitle = null, string embedContent = null, string embedImagePath = null)
        {
            send_embed(chatId, content, embedId, embedLink, embedTitle, embedContent, (embedImagePath == null) ? null : File.ReadAllBytes(embedImagePath));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to send a Sticker message to a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="stickerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task send_sticker(string chatId, string stickerId)
        {
            JObject data = new JObject();
            JObject attachementSub = new JObject();
            JObject extensionSub = new JObject();
            JObject extensionSuBArray = new JObject();
            data.Add("type", 3);
            data.Add("content", null);
            data.Add("clientRefId", helpers.GetTimestamp() / 10 % 1000000000);
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            attachementSub.Add("objectId", null);
            attachementSub.Add("objectType", null);
            attachementSub.Add("link", null);
            attachementSub.Add("title", null);
            attachementSub.Add("content", null);
            attachementSub.Add("mediaList", null);
            extensionSuBArray.Add("link", null);
            extensionSuBArray.Add("mediaType", 100);
            extensionSuBArray.Add("mediaUploadValue", null);
            extensionSuBArray.Add("mediaUploadValueContentType", "image/jpg");
            extensionSub.Add("mentionedArray", new JArray());
            extensionSub.Add("linkSnippetList", new JArray(extensionSuBArray));
            data.Add("attachedObject", attachementSub);
            data.Add("extensions", extensionSub);
            data.Add("stickerId", stickerId);

            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message");
            request.AddHeaders(headers);
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            var response = client.ExecutePost(request);
            if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if (Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to get information about a blog post
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.Blog get_blog_info(string blogId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/blog/{blogId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if(Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<Blog>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }


        /// <summary>
        /// Allows you to get information about a wiki post
        /// </summary>
        /// <param name="wikiId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.Wiki get_wiki_info(string wikiId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/item/{wikiId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if(Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<Wiki>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to get information about a message in a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Objects.Message get_message_info(string chatId, string messageId)
        {
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/message/{messageId}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if(Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<Message>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        /// <summary>
        /// Allows you to get comments from a blog, THIS FUNCTION IS NOT FINISHED
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Objects.Comment> get_blog_comments(string blogId, int start = 0, int size = 25, Types.Sorting_Types sorting = Types.Sorting_Types.Newest)
        {
            string sortingType = "";
            switch(sorting)
            {
                case Types.Sorting_Types.Newest:
                    sortingType = "newest";
                    break;
                case Types.Sorting_Types.Oldest:
                    sortingType = "oldest";
                    break;
                case Types.Sorting_Types.Top:
                    sortingType = "vote";
                    break;
            }
            RestClient client = new RestClient(helpers.BaseUrl);
            RestRequest request = new RestRequest($"/g/s/blog/{blogId}?sort={sortingType}&start={size}&size={size}");
            request.AddHeaders(headers);
            var response = client.ExecuteGet(request);
            if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
            if(Debug) { Trace.WriteLine(response.Content); }

            return null; // FINISH LATER

        }


        /// <summary>
        /// Sets the SubClient of the Client, not for development use
        /// </summary>
        /// <param name="subClient"></param>
        /// <exception cref="Exception"></exception>
        public void SetSubClient(Amino.SubClient subClient)
        {
            if (subClient == null) { throw new Exception("No SubClient provided!"); }
            this.subClient = subClient;
        }

        /// <summary>
        /// Handles the Event calls, do not manually interact with this Class or its functions!
        /// </summary> 
        public class Events
        {
            public void callMessageEvent(Amino.Client _client, object _sender, Amino.Objects.Message _message)
            {
                if (_client.onMessage != null)
                {
                    _client.onMessage.Invoke(_message);
                }
            }

            public void callWebSocketMessageEvent(Amino.Client _client, string _webSocketMessage)
            {
                if (_client.onWebSocketMessage != null)
                {
                    _client.onWebSocketMessage.Invoke(_webSocketMessage);
                }
            }

            public void callImageMessageEvent(Amino.Client _client, Amino.Objects.ImageMessage _image)
            {
                if (_client.onImageMessage != null)
                {
                    _client.onImageMessage.Invoke(_image);
                }
            }

            public void callYouTubeMessageEvent(Amino.Client _client, Amino.Objects.YouTubeMessage _youtubeMessage)
            {
                if (_client.onYouTubeMessage != null)
                {
                    _client.onYouTubeMessage.Invoke(_youtubeMessage);
                }
            }

            public void callVoiceMessageEvent(Amino.Client _client, Amino.Objects.VoiceMessage _voiceMessage)
            {
                if (_client.onVoiceMessage != null)
                {
                    _client.onVoiceMessage.Invoke(_voiceMessage);
                }
            }

            public void callStickerMessageEvent(Amino.Client _client, Amino.Objects.StickerMessage _stickerMessage)
            {
                if (_client.onStickerMessage != null)
                {
                    _client.onStickerMessage.Invoke(_stickerMessage);
                }
            }

            public void callMessageDeletedEvent(Amino.Client _client, Amino.Objects.DeletedMessage _deletedMessage)
            {
                if (_client.onMessageDeleted != null)
                {
                    _client.onMessageDeleted.Invoke(_deletedMessage);
                }
            }

            public void callChatMemberJoinEvent(Amino.Client _client, Amino.Objects.JoinedChatMember _joinedMember)
            {
                if (_client.onChatMemberJoin != null)
                {
                    _client.onChatMemberJoin.Invoke(_joinedMember);
                }
            }

            public void callChatMemberLeaveEvent(Amino.Client _client, Amino.Objects.LeftChatMember _leftMember)
            {
                if (_client.onChatMemberLeave != null)
                {
                    _client.onChatMemberLeave.Invoke(_leftMember);
                }
            }

            public void callChatBackgroundChangedEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if (_client.onChatBackgroundChanged != null)
                {
                    _client.onChatBackgroundChanged.Invoke(_chatEvent);
                }
            }
            public void callChatTitleChangedEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if (_client.onChatTitleChanged != null)
                {
                    _client.onChatTitleChanged.Invoke(_chatEvent);
                }
            }

            public void callChatContentChangedEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if (_client.onChatContentChanged != null)
                {
                    _client.onChatContentChanged.Invoke(_chatEvent);
                }
            }

            public void callChatPinAnnouncementEvent(Amino.Client _client, Amino.Objects.ChatAnnouncement _chatAnnouncement)
            {
                if (_client.onChatAnnouncementPin != null)
                {
                    _client.onChatAnnouncementPin.Invoke(_chatAnnouncement);
                }
            }

            public void callChatUnpinAnnouncementEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if (_client.onChatAnnouncementUnpin != null)
                {
                    _client.onChatAnnouncementUnpin.Invoke(_chatEvent);
                }
            }

            public void callChatViewModeOnEvent(Amino.Client _client, Amino.Objects.ViewMode _viewMode)
            {
                if (_client.onChatViewModeOn != null)
                {
                    _client.onChatViewModeOn.Invoke(_viewMode);
                }
            }

            public void callChatViewModeOffEvent(Amino.Client _client, Amino.Objects.ViewMode _viewMode)
            {
                if (_client.onChatViewModeOff != null)
                {
                    _client.onChatViewModeOff.Invoke(_viewMode);
                }
            }

            public void callChatTipEnabledEvent(Amino.Client _client, Amino.Objects.ChatTipToggle _chatTip)
            {
                if (_client.onChatTipEnabled != null)
                {
                    _client.onChatTipEnabled.Invoke(_chatTip);
                }
            }

            public void callChatTipDisabledEvent(Amino.Client _client, Amino.Objects.ChatTipToggle _chatTip)
            {
                if (_client.onChatTipDisabled != null)
                {
                    _client.onChatTipDisabled.Invoke(_chatTip);
                }
            }

            public void callMessageForceRemovedByAdminEvent(Amino.Client _client, Amino.Objects.SpecialChatEvent _chatEvent)
            {
                if (_client.onMessageForceRemovedByAdmin != null)
                {
                    _client.onMessageForceRemovedByAdmin.Invoke(_chatEvent);
                }
            }

            public void callChatTipEvent(Amino.Client _client, Amino.Objects.ChatTip _chatTip)
            {
                if (_client.onChatTip != null)
                {
                    _client.onChatTip.Invoke(_chatTip);
                }
            }
        }
    }
}
