using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public string deviceID { get; private set; } 
        public string sessionID { get; private set; }
        public string secret { get; private set; }
        public string userID { get; private set; }
        public string json { get; private set; }
        public string googleID { get; private set; }
        public string appleID { get; private set; }
        public string facebookID { get; private set; }
        public string twitterID { get; private set; }
        public string iconURL { get; private set; }
        public string aminoID { get; private set; }
        public string email { get; private set; }
        public string phoneNumber { get; private set; }
        public string nickname { get; private set; }
        public bool is_Global { get; private set; }
        public bool debug { get; set; } = false;



        //The value to access the websocket manager
        private Amino.WebSocketHandler webSocket;
        //Events
        public event EventHandler<Amino.Events.messageEvent> onMessage;
        public event EventHandler<Amino.Events.webSocketMessageEvent> onWebSocketMessage;

        //headers.
        private IDictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", deviceID);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.narvii.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            if(sessionID != null) { headers.Add("NDCAUTH", $"sid={sessionID}"); }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates an instance of the Amino.Client object and builds headers
        /// 
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
        public Task Login(string _email, string _password, string _secret = null)
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
        public Task Logout()
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
        public Task Register(string _name, string _email, string _password, string _verificationCode, string _deviceID = null)
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
        public Task Restore_account(string _email, string _password, string _deviceID = null)
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
        public Task Delete_account(string _password)
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
        public Task Change_password(string _email, string _password, string _verificationCode)
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
                Console.WriteLine(response.Content);
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


            return Task.CompletedTask;

        }
        public class Events
        {
            public void callMessageEvent(Amino.Client _client, object _sender, Amino.Objects.Message _message)
            {
                if(_client.onMessage != null)
                {
                    _client.onMessage.Invoke(_sender, new Amino.Events.messageEvent(_message));
                }
            }

            public void callWebSocketMessageEvent(Amino.Client _client, object _sender, JObject _webSocketMessage)
            {
                if(_client.onWebSocketMessage != null)
                {
                    _client.onWebSocketMessage.Invoke(_sender, new Amino.Events.webSocketMessageEvent(_webSocketMessage));
                }
            }
        }
    }
}
