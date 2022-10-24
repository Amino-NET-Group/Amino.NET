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

        public Client(string _deviceID = null)
        {
            if(_deviceID == null) { deviceID = helpers.generate_device_id(); } else { deviceID = _deviceID; }
            headerBuilder();
        }

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

        public Amino.Objects.globalProfile get_user_info(string _userId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s/user-profile/{_userId}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                Amino.Objects.globalProfile profile = new Amino.Objects.globalProfile((JObject)JObject.Parse(response.Content));
                return profile;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

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

        public Amino.Objects.eventLog get_event_log()
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
                Amino.Objects.eventLog eventLog = new Amino.Objects.eventLog(JObject.Parse(response.Content));
                return eventLog;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }


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

        public List<Objects.communityProfile> get_subClient_profiles(int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.communityProfile> profileList = new List<Objects.communityProfile>();
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
                    Objects.communityProfile C_Profile = new Objects.communityProfile(profile);
                    profileList.Add(C_Profile);
                }

                return profileList;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public Objects.userAccount get_account_info()
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
                Objects.userAccount account = new Objects.userAccount(JObject.Parse(response.Content));
                return account;
               
            }
            catch (Exception e) { throw new Exception(e.Message); }

        }

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

        public List<Objects.chatMember> get_chat_users(string chatId, int start = 0, int size = 25)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            if (start < 0) { throw new Exception("start cannot be lower than 0"); }
            try
            {
                List<Objects.chatMember> chatMembers = new List<Objects.chatMember>();
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
                    Objects.chatMember member = new Objects.chatMember(chatUser);
                    chatMembers.Add(member);
                }
                return chatMembers;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
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
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

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

        public List<Objects.messageCollection> get_chat_messages(string chatId, int size = 25, string pageToken = null)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            string endPoint;
            if (pageToken != null) { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&pageToken={pageToken}&size={size}"; } else { endPoint = $"/g/s/chat/thread/{chatId}/message?v=2&pagingType=t&size={size}"; }
            try
            {
                List<Objects.messageCollection> messageCollection = new List<Objects.messageCollection>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                Console.WriteLine(response.Content);
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray chatMessageList = jsonObj["messageList"];
                foreach (JObject chatMessage in chatMessageList)
                {
                    Objects.messageCollection message = new Objects.messageCollection(chatMessage);
                    messageCollection.Add(message);
                }
                return messageCollection;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /* Not a part of Client
        public Task start_chat(string[] userIds, string message, string title = null, string content = null, bool isGlobal = false, bool publishToGlobal = false)
        {
            if (sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/chat/thread");
                request.AddHeaders(headers);
                JObject json = new JObject();
                json.Add("title", title);
                json.Add("inviteeUids", JToken.FromObject(new { userIds }));
                json.Add("initialMessageContent", message);
                json.Add("content", content);
                json.Add("timestamp", (Math.Round(helpers.GetTimestamp())) * 1000);
                if(isGlobal) { json.Add("type", 2); json.Add("eventSource", "GlobalComposeMenu"); } else { json.Add("type", 0); }
                if(publishToGlobal) { json.Add("publishToGlobal", 1); } else { json.Add("publishToGlobal", 0); }
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(System.Text.Json.JsonSerializer.Serialize(JsonConvert.SerializeObject(json))));
                request.AddJsonBody(JsonConvert.SerializeObject(json));

                Console.WriteLine(json.ToString());
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
               
            }catch(Exception e) { throw new Exception(e.Message); }
        } */

        /* WILL BE ADDED LATER
        public Task upload_media(Amino.Types.upload_File_Types fileType, byte[] file)
        {
            string mediaType;
            switch(fileType)
            {
                case Amino.Types.upload_File_Types.Audio:
                    mediaType = "audio/aac";
                    break;
                case Amino.Types.upload_File_Types.Image:
                    mediaType = "image/jpg";
                    break;
                default:
                    mediaType = "image/jpg";
                    break;
            }
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/media/upload");
                request.AddHeaders(headers);
                request.AddOrUpdateHeader("Content-Type", mediaType);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(file.ToString()));
                request.AddHeader("Content-Length", file.ToString().Length);
                request.AddJsonBody(file);
                var response = client.ExecutePost(request);
                Console.WriteLine(response.Content);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return Task.CompletedTask;
        }
        */

       

        public class Events
        {
            public void callMessageEvent(Amino.Client _client, object _sender, Amino.Objects.Message _message)
            {
                if(_client.onMessage != null)
                {
                    _client.onMessage.Invoke(_sender, new Amino.Events.messageEvent(_message));
                }
            }
        }
    }
}
