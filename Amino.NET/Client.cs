using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public string deviceID { get; private set; } 
        /// <summary>
        /// Represents the current Session ID of the current Client / Amino account instance
        /// </summary>
        public string sessionID { get; private set; }
        /// <summary>
        /// Represents the Login Secret of the current Amino account
        /// </summary>
        public string secret { get; private set; }
        /// <summary>
        /// Represents the current user ID (object ID) of the current Amino account
        /// </summary>
        public string userID { get; private set; }
        /// <summary>
        /// Represents the full JSON response of the current Amino account
        /// </summary>
        public string json { get; private set; }
        /// <summary>
        /// Represents the google ID of the current Amino account
        /// </summary>
        public string googleID { get; private set; }
        /// <summary>
        /// Represents the apple ID of the current Amino account
        /// </summary>
        public string appleID { get; private set; }
        /// <summary>
        /// Represents the facebook ID of the current Amino account
        /// </summary>
        public string facebookID { get; private set; }
        /// <summary>
        /// Represents the twitter ID of the current Amino account
        /// </summary>
        public string twitterID { get; private set; }
        /// <summary>
        /// Represents the Icon image URL of the current Amino account
        /// </summary>
        public string iconURL { get; private set; }
        /// <summary>
        /// Represents the amino ID of the current Amino account
        /// </summary>
        public string aminoID { get; private set; }
        /// <summary>
        /// Represents the email address of the current Amino account
        /// </summary>
        public string email { get; private set; }
        /// <summary>
        /// Represents the phone number of the current Amino account
        /// </summary>
        public string phoneNumber { get; private set; }
        /// <summary>
        /// Represents the nickname of the current Amino account
        /// </summary>
        public string nickname { get; private set; }
        /// <summary>
        /// Represents if the current Amino accounts profile is profile or not
        /// </summary>
        public bool is_Global { get; private set; }
        /// <summary>
        /// Represents the current Clients debug state, if put to true, all API responses and webSocket messages get printed to Trace
        /// </summary>
        public bool debug { get; set; } = false;



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

        //headers.
        private IDictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", deviceID);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.aminoapps.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            headers.Add("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.2; SM-G965N Build/star2ltexx-user 7.1.; com.narvii.amino.master/3.4.33602)");
            if(sessionID != null) { headers.Add("NDCAUTH", $"sid={sessionID}"); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates an instance of the Amino.Client object and builds headers
        /// <para>This object can hold a deviceId, if left empty, it will generate one.</para>
        /// </summary>
        /// <param>This object can hold a deviceId, if left empty, it will generate one.</param>
        /// <param name="_deviceID"></param>
        public Client(string _deviceID = null)
        {
            if(_deviceID == null) { deviceID = helpers.generate_device_id(); } else { deviceID = _deviceID; }
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
                if(!resetPassword) { request.AddJsonBody(new { identity = email, type = 1, deviceID = deviceID}); } else { request.AddJsonBody(new { identity = email, type = 1, deviceID = deviceID, level = 2, purpose = "reset-password"}); }
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }catch(Exception e)
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
        /// <returns></returns>
        public Task login(string _email, string _password, string _secret = null)
        {
            try
            {
                string secret;
                if (_secret == null) { secret = $"0 {_password}"; } else { secret = _secret; }
                var data = new { email = _email, v = 2, secret = secret, deviceID = deviceID, clientType = 100, action = "normal", timestamp = helpers.GetTimestamp() * 1000};

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/login", Method.Post);
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                json = response.Content;
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(json);
                try
                {
                    sessionID = (string)jsonObj["sid"];
                    this.secret = (string)jsonObj["secret"];
                    userID = (string)jsonObj["account"]["uid"];
                    googleID = (string)jsonObj["account"]["googleID"];
                    appleID = (string)jsonObj["account"]["appleID"];
                    facebookID = (string)jsonObj["account"]["facebookID"];
                    twitterID = (string)jsonObj["account"]["twitterID"];
                    iconURL = (string)jsonObj["userProfile"]["icon"];
                    aminoID = (string)jsonObj["account"]["aminoId"];
                    email = (string)jsonObj["account"]["email"];
                    phoneNumber = (string)jsonObj["account"]["phoneNumber"];
                    nickname = (string)jsonObj["userProfile"]["nickname"];
                    is_Global = (bool)jsonObj["userProfile"]["isGlobal"];
                }catch(Exception e) { throw new Exception(e.Message); }
                headerBuilder();
                Amino.WebSocketHandler _webSocket = new WebSocketHandler(this);
                this.webSocket = _webSocket;
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Allows you to log out of the current account
        /// <para>A successful function call will clear the Client values and close the webSocket connection</para>
        /// </summary>
        /// <returns></returns>
        public Task logout()
        {
            if(sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                var data = new
                {
                    deviceID = this.deviceID,
                    clientType = 100,
                    timestamp = helpers.GetTimestamp() * 1000
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/logout");
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                sessionID = null;
                secret = null;
                userID = null;
                json = null;
                googleID = null;
                appleID = null;
                facebookID = null;
                twitterID = null;
                iconURL = null;
                aminoID = null;
                email = null;
                phoneNumber = null;
                nickname = null;
                is_Global = false;
                headerBuilder();
                _ = webSocket.disconnect_socket();
                return Task.CompletedTask;

            }catch(Exception e) { throw new Exception(e.Message); }
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
            try
            {
                if (_deviceID == null) { if (deviceID != null) { _deviceID = deviceID; } else { _deviceID = helpers.generate_device_id(); } }
                var data = new
                {
                    secret = $"0 {_password}",
                    deviceID = _deviceID,
                    email = _email,
                    clientType = 100,
                    nickname = _name,
                    latitude = 0,
                    longtitude = 0,
                    address = String.Empty,
                    clientCallbackURL = "narviiapp://relogin",
                    validationContext = new
                    {
                        data = new { code = _verificationCode },
                        type = 1,
                        identity = _email
                    },
                    type = 1,
                    identity = _email,
                    timestamp = helpers.GetTimestamp() * 1000
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/register");
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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
                if (_deviceID == null) { if (deviceID != null) { _deviceID = deviceID; } else { _deviceID = helpers.generate_device_id(); } }
                var data = new { secret = $"0 {_password}", deviceID = _deviceID, email = _email, timestamp = helpers.GetTimestamp() * 1000 };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/delete-request/cancel");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to delete the current Amino account
        /// <para>This function requires a verification code, refer to <b>request_verify_code()</b></para>
        /// </summary>
        /// <param name="_password"></param>
        /// <returns></returns>
        public Task delete_account(string _password)
        {
            var data = new { deviceID = deviceID, secret = $"0 {_password}" };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/delete-request");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                sessionID = null;
                secret = null;
                userID = null;
                json = null;
                googleID = null;
                appleID = null;
                facebookID = null;
                twitterID = null;
                iconURL = null;
                aminoID = null;
                email = null;
                phoneNumber = null;
                nickname = null;
                is_Global = false;
                headerBuilder();
                _ = webSocket.disconnect_socket();
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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
            if (_deviceID == null) { if (deviceID != null) { _deviceID = deviceID; } else { _deviceID = helpers.generate_device_id(); } }
            var data = new
            {
                type = 1,
                identity = _email,
                data = new { code = _verificationCode },
                deviceID = _deviceID
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/activate-email");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to configure the current Amino account
        /// </summary>
        /// <param name="_gender"></param>
        /// <param name="_age"></param>
        /// <returns></returns>
        public Task configure_account(Types.account_gender _gender, int _age)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (_age <= 12) { throw new Exception("The given account age is too low"); }
            int gender;
            switch(_gender)
            {
                case Types.account_gender.Male:
                    gender = 1;
                    break;
                case Types.account_gender.Female:
                    gender = 2;
                    break;
                case Types.account_gender.Non_Binary:
                    gender = 255;
                    break;
                default:
                    gender = 255;
                    break;
            }
            var data = new { age = _age, gender = gender, timestamp = helpers.GetTimestamp() * 1000 };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/persona/profile/basic");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            } catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            var data = new
            {
                updateSecret = $"0 {_password}",
                emailValidationContext = new
                {
                    data = new { code = _verificationCode },
                    type = 1,
                    identity = _email,
                    level = 2,
                    deviceID = deviceID
                },
                phoneNumberValidationContext = String.Empty,
                deviceID = deviceID
            };

            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/reset-password");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
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
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{_userId}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                Amino.Objects.GlobalProfile profile = new Amino.Objects.GlobalProfile((JObject)JObject.Parse(response.Content));
                return profile;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to check if a Device ID is valid
        /// </summary>
        /// <param name="_deviceId"></param>
        /// <returns>bool : true / false</returns>
        public bool check_device(string _deviceId)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            var data = new
            {
                deviceID = _deviceId,
                bundleID = "com.narvii.amino.master",
                clientType = 100,
                systemPushEnabled = true,
                timezone = 0,
                locale = currentCulture.Name,
                timestamp = (Math.Round(helpers.GetTimestamp())) * 1000
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/device");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { return false; }
                if (debug) { Trace.WriteLine(response.Content); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/eventlog/profile?language=en");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                Amino.Objects.EventLog eventLog = new Amino.Objects.EventLog(JObject.Parse(response.Content));
                return eventLog;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }


        /// <summary>
        /// Allows you to get a list of Communities the current Amino account is in
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.Community</returns>
        public List<Objects.Community> get_subClient_communities(int start = 0, int size = 25)
        {
            List<Amino.Objects.Community> communityList = new List<Objects.Community>();

            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/community/joined?v=1&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }

                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray _jsonArray = jsonObj["communityList"];
                foreach(JObject _communityJson in _jsonArray)
                {
                    Amino.Objects.Community community = new Objects.Community(_communityJson);
                    communityList.Add(community);
                }
                return communityList;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of community profiles of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.CommunityProfile</returns>
        public List<Objects.CommunityProfile> get_subClient_profiles(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.CommunityProfile> profileList = new List<Objects.CommunityProfile>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/community/joined?v=1&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                var profiles = ((JObject)JObject.Parse(response.Content)["userInfoInCommunities"])
                .Properties()
                .Select(x => x.Value)
                .Cast<JObject>()
                .ToList();
                foreach(var profile in profiles)
                {
                    Objects.CommunityProfile C_Profile = new Objects.CommunityProfile(profile);
                    profileList.Add(C_Profile);
                }

                return profileList;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get information about the current Amino account
        /// </summary>
        /// <returns>Object : Amino.Objects.UserAccount</returns>
        public Objects.UserAccount get_account_info()
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                Objects.UserAccount account = new Objects.UserAccount(JObject.Parse(response.Content));
                return account;
               
            }
            catch (Exception e) { throw new Exception(e.Message); }

        }
        /// <summary>
        /// Allows you to get a list of chat threads the current Amino account is in 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.Chat</returns>
        public List<Objects.Chat> get_chat_threads(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }

            try
            {
                List<Objects.Chat> chatList = new List<Objects.Chat>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread?type=joined-me&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }

                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray _chatList = jsonObj["threadList"];
                foreach (JObject chatJson in _chatList)
                {
                    Objects.Chat chat = new Objects.Chat(chatJson);

                    chatList.Add(chat);
                }

                return chatList;

            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a single chat thread using a chatId
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns>Object : Amino.Objects.Chat</returns>
        public Objects.Chat get_chat_thread(string chatId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }

            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                Objects.Chat chat = new Objects.Chat(jsonObj["thread"]);
                return chat;
            }
            catch (Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.ChatMember> chatMembers = new List<Objects.ChatMember>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member?start={start}&size={size}&type=default&cv=1.2");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray chatUserList = jsonObj["memberList"];
                foreach(JObject chatUser in chatUserList)
                {
                    Objects.ChatMember member = new Objects.ChatMember(chatUser);
                    chatMembers.Add(member);
                }
                return chatMembers;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
        /// <summary>
        /// Allows you to join a chat with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task join_chat(string chatId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{userID}");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to leave a chat with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task leave_chat(string chatId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{userID}");
                request.AddHeaders(headers);
                request.Method = Method.Delete;
                var response = client.Execute(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to invite one or multiple members into a chat thread with the current Amino account
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task invite_to_chat(string[] userIds, string chatId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
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
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (allowRejoin) { _allowRejoin = 1; } else { _allowRejoin = 0; }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/member/{userId}?allowRejoin={_allowRejoin}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string endPoint;
            if (!string.IsNullOrEmpty(pageToken) || !string.IsNullOrWhiteSpace(pageToken)) { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&pageToken={pageToken}&size={size}"; } else { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&size={size}"; }
            try
            {
                List<Objects.MessageCollection> messageCollection = new List<Objects.MessageCollection>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray chatMessageList = jsonObj["messageList"];
                foreach (JObject chatMessage in chatMessageList)
                {
                    Objects.MessageCollection message = new Objects.MessageCollection(chatMessage, JObject.Parse(response.Content));
                    messageCollection.Add(message);
                }
                return messageCollection;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to search a community based on its ID (aminoId <b>not</b> objectId) and get information about it
        /// </summary>
        /// <param name="aminoId"></param>
        /// <returns>List : Amino.Objects.CommunityInfo</returns>
        public List<Objects.CommunityInfo> search_community(string aminoId)
        {
            try
            {
                List<Objects.CommunityInfo> communityInfoList = new List<Objects.CommunityInfo>(); 
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/search/amino-id-and-link?q={aminoId}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray communityInfo = jsonObj["resultList"];
                foreach(JObject community in communityInfo)
                {
                    Objects.CommunityInfo com = new Objects.CommunityInfo(community);
                    communityInfoList.Add(com);
                }
                return communityInfoList;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of users a person is following
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.UserFollowings</returns>
        public List<Objects.UserFollowings> get_user_following(string userId, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.UserFollowings> userFollowingsList = new List<Objects.UserFollowings>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/joined?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray userFollowings = jsonObj["userProfileList"];
                foreach (JObject following in userFollowings)
                {
                    Objects.UserFollowings _following = new Objects.UserFollowings(following);
                    userFollowingsList.Add(_following);
                }
                return userFollowingsList;

            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of users that follow a person
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Obejcts.UserFollowings</returns>
        public List<Objects.UserFollowings> get_user_followers(string userId, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.UserFollowings> userFollowerList = new List<Objects.UserFollowings>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/member?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray userFollowers = jsonObj["userProfileList"];
                foreach (JObject follower in userFollowers)
                {
                    Objects.UserFollowings _follower = new Objects.UserFollowings(follower);
                    userFollowerList.Add(_follower);
                }
                return userFollowerList;

            }catch(Exception e) { throw new Exception(e.Message); }

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
            try
            {
                List<Objects.UserVisitor> userVisitorList = new List<Objects.UserVisitor>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/visitors?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray userVisitors = jsonObj["visitors"];
                foreach (JObject visitor in userVisitors)
                {
                    Objects.UserVisitor _visitor = new Objects.UserVisitor(visitor, jsonObj);
                    userVisitorList.Add(_visitor);
                }
                return userVisitorList;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of users the current Amino account has blocked
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.BlockedUser</returns>
        public List<Objects.BlockedUser> get_blocked_users(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.BlockedUser> blockedUserList = new List<Objects.BlockedUser>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/block?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.StatusCode); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray blockedUsers = jsonObj["userProfileList"];
                foreach (JObject user in blockedUsers)
                {
                    Objects.BlockedUser _blockedUser = new Objects.BlockedUser(user);
                    blockedUserList.Add(_blockedUser);
                }
                return blockedUserList;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of userIds of the users that have blocked the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : string</returns>
        public List<string> get_blocker_users(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<string> blockerUserList = new List<string>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/block/full-list?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray blockerUsers = jsonObj["blockerUidList"];
                foreach(var user in blockerUsers)
                {
                    blockerUserList.Add(user.ToString());
                }
                return blockerUserList;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            try
            {
                List<Objects.Comment> commentList = new List<Objects.Comment>();
                string _sorting_type;
                switch(type)
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
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray comments = jsonObj["commentList"];
                foreach (JObject comment in comments)
                {
                    Objects.Comment _comment = new Objects.Comment(comment);
                    commentList.Add(_comment);
                }
                return commentList;

            }
            catch(Exception e) { throw new Exception(e.Message); }
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
            if(!asGuest) { if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); } }
            int _objectType;
            int _flagType;
            string _flag;
            switch(targetType)
            {
                case Types.Flag_Targets.User:
                    _objectType = 0;
                    break;
                case Types.Flag_Targets.Blog:
                    _objectType = 1;
                    break;
                case Types.Flag_Targets.Wiki:
                    _objectType = 2;
                    break;
                default:
                    _objectType = 0;
                    break;
            }
            switch(flagType)
            {
                case Types.Flag_Types.Aggression:
                    _flagType = 0;
                    break;
                case Types.Flag_Types.Spam:
                    _flagType = 2;
                    break;
                case Types.Flag_Types.Off_Topic:
                    _flagType = 4;
                    break;
                case Types.Flag_Types.Violence:
                    _flagType = 106;
                    break;
                case Types.Flag_Types.Intolerance:
                    _flagType = 107;
                    break;
                case Types.Flag_Types.Suicide:
                    _flagType = 108;
                    break;
                case Types.Flag_Types.Trolling:
                    _flagType = 109;
                    break;
                case Types.Flag_Types.Pronography:
                    _flagType = 110;
                    break;
                default:
                    _flagType = 0;
                    break;
            }
            if(asGuest) { _flag = "g-flag"; } else { _flag = "flag"; }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/{_flag}");
                var data = new
                {
                    flagType = _flagType,
                    message = reason,
                    timestamp = helpers.GetTimestamp() * 1000,
                    objectId = targetId,
                    objetType = _objectType
                };
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                var data = new
                {
                    adminOpName = 102,
                    adminOpNote = new { content = reason },
                    timestamp = helpers.GetTimestamp() * 1000
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest();
                if(asStaff) { request.Resource = $"/g/s/chat/thread/{chatId}/message/{messageId}/admin"; } else { request.Resource = $"/g/s/chat/thread/{chatId}/message/{messageId}"; }
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
        /// <summary>
        /// Allows you to mark a message as read with the current Amino account
        /// </summary>
        /// <param name="_chatId"></param>
        /// <param name="_messageId"></param>
        /// <returns></returns>
        public Task mark_as_read(string _chatId, string _messageId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            var data = new
            {
                messageId = _messageId,
                timestamp = helpers.GetTimestamp() * 1000
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{_chatId}/mark-as-read");
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to visit a users Amino profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task visit(string userId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userId}?action=visit");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to follow a user with the current Amino account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task follow_user(string userId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userId}/member");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to unfollow a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task unfollow_user(string _userId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{_userId}/member/{userID}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }catch(Exception e) { throw new Exception(e.Message); }
        }
        /// <summary>
        /// Allows you to block a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task block_user(string _userId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/block/{_userId}");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to unblock a user with the current Amino account
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public Task unblock_user(string _userId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/block/{_userId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to join a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public Task join_community(string communityId, string invitationCode = null)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                if(invitationCode != null) { data.Add("invitationId", invitationCode); }
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/community/join");
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to request membership to a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task join_community_request(string communityId, string message = null)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/community/membership-request");
                var data = new
                {
                    message = message,
                    timestamp = helpers.GetTimestamp() * 1000
                };
                request.AddJsonBody(data);
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to leave a community with the current Amino account
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task leave_community(string communityId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/s/community/leave");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            if(!asGuest) { if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); } }
            int _flagType;
            string _flag;
            switch(flagType)
            {
                case Types.Flag_Types.Aggression:
                    _flagType = 0;
                    break;
                case Types.Flag_Types.Spam:
                    _flagType = 2;
                    break;
                case Types.Flag_Types.Off_Topic:
                    _flagType = 4;
                    break;
                case Types.Flag_Types.Violence:
                    _flagType = 106;
                    break;
                case Types.Flag_Types.Intolerance:
                    _flagType = 107;
                    break;
                case Types.Flag_Types.Suicide:
                    _flagType = 108;
                    break;
                case Types.Flag_Types.Pronography:
                    _flagType = 110;
                    break;
                default:
                    _flagType = 0;
                    break;
            }
            if(asGuest) { _flag = "g-flag"; } else { _flag = "flag"; }
            var data = new
            {
                objectId = communityId,
                objectType = 16,
                flagType = _flagType,
                message = reason,
                timestamp = helpers.GetTimestamp() * 1000
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/{_flag}");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch(Exception e) { throw new Exception(e.Message); }

        }

        /// <summary>
        /// Allows you to upload media files to the Amino servers
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns>string : The URL to the media file you just uploaded</returns>
        public string upload_media(string filePath, Types.upload_File_Types type)
        {
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _fileType;
            switch(type)
            {
                case Types.upload_File_Types.Audio:
                    _fileType = "audio/aac";
                    break;
                case Types.upload_File_Types.Image:
                    _fileType = "image/jpg";
                    break;
                default:
                    _fileType = "image/jpg";
                    break;
            }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/media/upload");
                request.AddHeaders(headers);
                request.AddOrUpdateHeader("Content-Type", _fileType);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_file_signiture(file));
                request.AddBody(file, _fileType);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                return jsonObj["mediaValue"];
            }catch(Exception e) { throw new Exception(e.Message); }

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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            JObject data = new JObject();
            data.Add("address", null);
            data.Add("latitude", 0);
            data.Add("longtitude", 0);
            data.Add("mediaList", null);
            data.Add("eventSource", "UserProfileView");
            data.Add("timestamp", helpers.GetTimestamp() * 1000);
            if(nickname != null) { data.Add("nickname", nickname); }
            if(icon != null) { data.Add("icon", upload_media(icon, Types.upload_File_Types.Image)); }
            if(content != null) { data.Add("content", content); }
            if (backgroundColor != null) {

                if(data["extensions"] != null)
                {
                    JObject subDataColor = (JObject)data["extensions"]["style"];
                    subDataColor.Add(new JObject(new JProperty("backgroundColor", backgroundColor)));
                    data.AddAnnotation(subDataColor);
                } else
                {
                    data.Add(new JProperty("extensions", new JObject(new JProperty("style", new JObject(new JProperty("backgroundColor", backgroundColor))))));
                }
            }
            if(backgroundMediaUrl != null)
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
                } else
                {
                    data.Add(new JProperty("extensions", new JObject(new JProperty("style", new JObject(new JProperty("backgroundMediaList", JArray.FromObject(jsonArray)))))));
                }

            }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"g/s/user-profile/{userID}");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to set the privacy status of the current Amino account
        /// </summary>
        /// <param name="isAnonymous"></param>
        /// <param name="getNotifications"></param>
        /// <returns></returns>
        public Task set_privacy_status(bool isAnonymous = false, bool getNotifications = true)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            int _privacyMode;
            int _notificationStatus;
            if(isAnonymous) { _privacyMode = 2; } else { _privacyMode = 1; }
            if (getNotifications) { _notificationStatus = 2; } else { _notificationStatus = 1; }
            var data = new
            {
                timestamp = helpers.GetTimestamp() * 1000,
                notificationStatus = _notificationStatus,
                privacyMode = _privacyMode
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/visit-settings");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to set the Amino ID of the current Amino account
        /// </summary>
        /// <param name="aminoId"></param>
        /// <returns></returns>
        public Task set_amino_id(string aminoId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            var data = new
            {
                timestamp = helpers.GetTimestamp() * 1000,
                aminoId = aminoId
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/change-amino-id");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Adds a linked community to the current Amino profile
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task add_linked_community(int communityId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userID}/linked-community/{communityId}");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Removes a linked community from the current Amino profile
        /// </summary>
        /// <param name="communityId"></param>
        /// <returns></returns>
        public Task remove_linked_community(int communityId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{userID}/linked-community/{communityId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _eventSource;
            bool _isReply = false;
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest();
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("content", message);
                data.Add("stickerId", null);
                data.Add("type", 0);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);


                switch(type)
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
                if(!_isReply) { data.Add("eventSource", _eventSource); } else { data.Add("respondTo", objectId); }
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data.ToString())));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
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
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest();
                switch(type)
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
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
                
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to like a post using the current Amino account
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task like_post(string objectId, Types.Post_Types type)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string _eventSource;
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest();
                switch(type)
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
                var data = new
                {
                    value = 4,
                    timestamp = helpers.GetTimestamp() * 1000,
                    eventSource = _eventSource
                };
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to remove a like on a post using the current Amino account
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task unlike_post(string objectId, Types.Post_Types type)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest();
                request.AddHeaders(headers);

                switch(type)
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
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the Amino+ membership status of the current Amino account
        /// </summary>
        /// <returns>Object : Amino.Objects.MembershipInfo</returns>
        public Objects.MembershipInfo get_membership_info()
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/membership?force=true");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                Objects.MembershipInfo membershipInfo = new Objects.MembershipInfo(JObject.Parse(response.Content));
                return membershipInfo;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the Team Amino announcement Posts
        /// </summary>
        /// <param name="language"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Obejcts.Amino.Post</returns>
        public List<Objects.Post> get_ta_announcements(Types.Supported_Languages language = Types.Supported_Languages.english, int start = 0, int size = 25)
        {
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            string _language;
            switch(language)
            {
                case Types.Supported_Languages.english:
                    _language = "en";
                    break;
                case Types.Supported_Languages.spanish:
                    _language = "es";
                    break;
                case Types.Supported_Languages.portuguese:
                    _language = "pt";
                    break;
                case Types.Supported_Languages.arabic:
                    _language = "ar";
                    break;
                case Types.Supported_Languages.russian:
                    _language = "ru";
                    break;
                case Types.Supported_Languages.french:
                    _language = "fr";
                    break;
                case Types.Supported_Languages.german:
                    _language = "de";
                    break;
                default:
                    _language = "en";
                    break;
            }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/announcement?language={_language}&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray announcements = jsonObj["blogList"];
                List<Objects.Post> ta_announcements = new List<Objects.Post>();
                foreach (JObject post in announcements)
                {
                    Objects.Post _post = new Objects.Post(post);
                    ta_announcements.Add(_post);
                }
                return ta_announcements;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the current Amino accounts wallet info
        /// </summary>
        /// <returns>Object : Objects.WalletInfo</returns>
        public Objects.WalletInfo get_wallet_info()
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/wallet");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                JObject jsonObj = JObject.Parse(response.Content);
                Objects.WalletInfo _walletInfo = new Objects.WalletInfo(jsonObj);
                return _walletInfo;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the wallet transaction history of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Objects.CoinHistoryEntry</returns>
        public List<Objects.CoinHistoryEntry> get_wallet_history(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/wallet/coin/history?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray historyEntry = jsonObj["coinHistoryList"];
                List<Objects.CoinHistoryEntry> coinHistoryList = new List<Objects.CoinHistoryEntry>();
                foreach (JObject entry in historyEntry)
                {
                    Objects.CoinHistoryEntry _entry = new Objects.CoinHistoryEntry(entry);
                    coinHistoryList.Add(_entry);
                }
                return coinHistoryList;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a user ID based on a device ID
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>string : the target users ID</returns>
        public string get_from_deviceId(string deviceId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/auid?deviceId={deviceId}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                Console.WriteLine(response.Content);
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                return jsonObj["auid"];
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get information an Amino URL
        /// </summary>
        /// <param name="aminoUrl"></param>
        /// <returns>Object : Amino.Objects.FromCode</returns>
        public Objects.FromCode get_from_code(string aminoUrl)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/link-resolution?q={aminoUrl}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                Amino.Objects.FromCode fromCode = new Objects.FromCode(jsonObj);
                return fromCode;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
        
        /// <summary>
        /// Allows you to get information about an Amino ID
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <param name="communityId"></param>
        /// <returns>Object : Amino.Objects.FromId</returns>
        public Objects.FromId get_from_id(string objectId, Amino.Types.Object_Types type, string communityId = null)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            int _type = helpers.get_ObjectTypeID(type);
            var data = new
            {
                objectId = objectId,
                targetCode = 1,
                timestamp = helpers.GetTimestamp() * 1000,
                objectType = _type
            };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest();
                if(communityId != null)
                {
                    request.Resource = $"/g/s-x{communityId}/link-resolution";
                } else { request.Resource = $"/g/s/link-resolution"; }
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                Amino.Objects.FromId fromId = new Objects.FromId(jsonObj);
                return fromId;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get the supported langauges of Amino
        /// </summary>
        /// <returns>string[] : language keys</returns>
        public string[] get_supported_languages()
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/community-collection/supported-languages?start=0&size=100");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                Console.WriteLine(response.Content);
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray languageArray = jsonObj["supportedLanguages"];
                List<string> langList = new List<string>();
                foreach(JObject language in languageArray)
                {
                    langList.Add(language.ToString());
                }
                return langList.ToArray();
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to claim the new user coupon using the current Amino account
        /// </summary>
        /// <returns></returns>
        public Task claim_new_user_coupon()
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/coupon/new-user-coupon/claim");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
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
            try
            {
                List<Objects.UserProfile> userList = new List<Objects.UserProfile>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile?type=recent&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray userArray = jsonObj["userProfileList"];
                foreach (JObject user in userArray)
                {
                    Objects.UserProfile _user = new Objects.UserProfile(user);
                    userList.Add(_user);
                }
                return userList;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to accept host / organizer of a chatroom with the current Amino account
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Task accept_host(string chatId, string requestId)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/chat/thread/{chatId}/transfer-organizer/{requestId}/accept");
                request.AddHeaders(headers);
                var data = new { };
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
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
        /// <returns>Obejct : Amino.Objects.FromInvite</returns>
        public Amino.Objects.FromInvite link_identify(string inviteCode)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/community/link-identify?q=http%3A%2F%2Faminoapps.com%2Finvite%2F{inviteCode}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                JObject json = JObject.Parse(response.Content);
                Objects.FromInvite fromInvite = new Objects.FromInvite(json);
                return fromInvite;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to change the wallet config of the current Amino account
        /// </summary>
        /// <param name="walletLevel"></param>
        /// <returns></returns>
        public Task wallet_config(Types.Wallet_Config_Levels walletLevel)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            int _walletLevel;
            if(walletLevel == Types.Wallet_Config_Levels.lvl_1) { _walletLevel = 1; } else { _walletLevel = 2; }
            try
            {
                var data = new
                {
                    adsLevel = _walletLevel,
                    timestamp = helpers.GetTimestamp() * 1000
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/wallet/ads/config");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(data)));
                request.AddJsonBody(data);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to get a list of available Avatar Frames of the current Amino account
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List : Amino.Object.AvatarFrame</returns>
        public List<Objects.AvatarFrame> get_avatar_frames(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.AvatarFrame> _avataFrameList = new List<Objects.AvatarFrame>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/avatar-frame?start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray avatarFrameArrray = jsonObj["avatarFrameList"];
                foreach(JObject avatarFrame in avatarFrameArrray)
                {
                    Objects.AvatarFrame _avatarFrame = new Objects.AvatarFrame(avatarFrame);
                    _avataFrameList.Add(_avatarFrame);
                }
                return _avataFrameList;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Handles the Event calls, do not manually interact with this Class or its functions!
        /// </summary> 
        public class Events
        {
            public void callMessageEvent(Amino.Client _client, object _sender, Amino.Objects.Message _message)
            {
                if(_client.onMessage != null)
                {
                    _client.onMessage.Invoke(_message);
                }
            }

            public void callWebSocketMessageEvent(Amino.Client _client, JObject _webSocketMessage)
            {
                if(_client.onWebSocketMessage != null)
                {
                    _client.onWebSocketMessage.Invoke(_webSocketMessage.ToString());
                }
            }

            public void callImageMessageEvent(Amino.Client _client, Amino.Objects.ImageMessage _image)
            {
                if(_client.onImageMessage != null)
                {
                    _client.onImageMessage.Invoke(_image);
                }
            }

            public void callYouTubeMessageEvent(Amino.Client _client, Amino.Objects.YouTubeMessage _youtubeMessage)
            {
                if(_client.onYouTubeMessage != null)
                {
                    _client.onYouTubeMessage.Invoke(_youtubeMessage);
                }
            }

            public void callVoiceMessageEvent(Amino.Client _client, Amino.Objects.VoiceMessage _voiceMessage)
            {
                if(_client.onVoiceMessage != null)
                {
                    _client.onVoiceMessage.Invoke(_voiceMessage);
                }
            }

            public void callStickerMessageEvent(Amino.Client _client, Amino.Objects.StickerMessage _stickerMessage)
            {
                if(_client.onStickerMessage != null)
                {
                    _client.onStickerMessage.Invoke(_stickerMessage);
                }
            }

            public void callMessageDeletedEvent(Amino.Client _client, Amino.Objects.DeletedMessage _deletedMessage)
            {
                if(_client.onMessageDeleted != null)
                {
                    _client.onMessageDeleted.Invoke(_deletedMessage);
                }
            }

            public void callChatMemberJoinEvent(Amino.Client _client, Amino.Objects.JoinedChatMember _joinedMember)
            {
                if(_client.onChatMemberJoin != null)
                {
                    _client.onChatMemberJoin.Invoke(_joinedMember);
                }
            }

            public void callChatMemberLeaveEvent(Amino.Client _client, Amino.Objects.LeftChatMember _leftMember)
            {
                if(_client.onChatMemberLeave != null)
                {
                    _client.onChatMemberLeave.Invoke(_leftMember);
                }
            }

            public void callChatBackgroundChangedEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if(_client.onChatBackgroundChanged != null)
                {
                    _client.onChatBackgroundChanged.Invoke(_chatEvent);
                }
            }
            public void callChatTitleChangedEvent(Amino.Client _client, Amino.Objects.ChatEvent _chatEvent)
            {
                if(_client.onChatTitleChanged != null)
                {
                    _client.onChatTitleChanged.Invoke(_chatEvent);
                }
            }
        }
    }
}
