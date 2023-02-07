using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Amino
{
    public class SubClient
    {

        private Amino.Client client;
        public bool debug { get; set; }
        private string communityId;



        //headers.
        private IDictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", client.deviceID);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.aminoapps.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            headers.Add("User-Agent", "Apple iPhone13,4 iOS v15.6.1 Main/3.12.9");
            if (client.sessionID != null) { headers.Add("NDCAUTH", $"sid={client.sessionID}"); }
            return Task.CompletedTask;
        }


        public SubClient(Amino.Client _client, string _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId;
            headerBuilder();
        }

        public SubClient(Amino.Client _client, int _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId.ToString();
            headerBuilder();
        }


        
        public List<Amino.Objects.InviteCode> get_invite_codes(string status = "normal", int start = 0, int size = 25)
        {
            try
            {
                List<Amino.Objects.InviteCode> inviteCodeList = new List<Objects.InviteCode>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation?status={status}&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray inviteCodesJson = jsonObj["communityInvitationList"];
                foreach(JObject code in inviteCodesJson)
                {
                    Amino.Objects.InviteCode invCode = new Objects.InviteCode(code);
                    inviteCodeList.Add(invCode);
                }
                Console.WriteLine(response.Content);
                return inviteCodeList;

            } catch(Exception e) { throw new Exception(e.Message); }

        } 

        public Task generate_invite_code(int duration = 0, bool force = true)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("duration", duration);
                data.Add("force", force);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task delete_invite_code(string inviteId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation/{inviteId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task post_blog(string title, string content, List<byte[]> imageList = null, bool fansOnly = false, string backgroundColor = null)
        {
            try
            {
                JArray mediaList = new JArray();
                JObject extensionData = new JObject();

                if(imageList != null)
                {
                    JArray tempMedia = new JArray();
                    foreach(byte[] bytes in imageList)
                    {
                        tempMedia = new JArray();
                        tempMedia.Add(100);
                        tempMedia.Add(this.client.upload_media(bytes, Types.upload_File_Types.Image));
                        tempMedia.Add(null);
                        mediaList.Add(tempMedia);
                    }
                }
                if(fansOnly) { extensionData.Add("fansOnly", fansOnly); }
                if(backgroundColor != null) { JObject color = new JObject(); color.Add("backgroundColor", backgroundColor); extensionData.Add(new JProperty("style", color)); }

                

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("address", null);
                data.Add("title", title);
                data.Add("content", content);
                data.Add("mediaList", new JArray(mediaList));
                data.Add(new JProperty("extensions", extensionData));
                data.Add("latitude", 0);
                data.Add("longitude", 0);
                data.Add("eventSource", "GlobalComposeMenu");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }

                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task post_wiki(string title, string content, List<byte[]> imageList = null, bool fansOnly = false, string backgroundColor = null)
        {
            try
            {
                JArray mediaList = new JArray();
                JObject extensionData = new JObject();

                if (imageList != null)
                {
                    JArray tempMedia = new JArray();
                    foreach (byte[] bytes in imageList)
                    {
                        tempMedia = new JArray();
                        tempMedia.Add(100);
                        tempMedia.Add(this.client.upload_media(bytes, Types.upload_File_Types.Image));
                        tempMedia.Add(null);
                        mediaList.Add(tempMedia);
                    }
                }
                if (fansOnly) { extensionData.Add("fansOnly", fansOnly); }
                if (backgroundColor != null) { JObject color = new JObject(); color.Add("backgroundColor", backgroundColor); extensionData.Add(new JProperty("style", color)); }


                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/item");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("label", title);
                data.Add("content", content);
                data.Add("eventSource", "GlobalComposeMenu");
                data.Add("mediaList", new JArray(mediaList));
                data.Add(new JProperty("extensions", extensionData));
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            } catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task edit_blog(string blogId, string title = null, string content = null, List<byte[]> imageList = null, bool fansOnly = false, string backgroundColor = null)
        {
            try
            {
                JArray mediaList = new JArray();
                JObject extensionData = new JObject();

                if (imageList != null)
                {
                    JArray tempMedia = new JArray();
                    foreach (byte[] bytes in imageList)
                    {
                        tempMedia = new JArray();
                        tempMedia.Add(100);
                        tempMedia.Add(this.client.upload_media(bytes, Types.upload_File_Types.Image));
                        tempMedia.Add(null);
                        mediaList.Add(tempMedia);
                    }
                }
                if (fansOnly) { extensionData.Add("fansOnly", fansOnly); }
                if (backgroundColor != null) { JObject color = new JObject(); color.Add("backgroundColor", backgroundColor); extensionData.Add(new JProperty("style", color)); }



                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("address", null);
                data.Add("title", title);
                data.Add("content", content);
                data.Add("mediaList", new JArray(mediaList));
                data.Add(new JProperty("extensions", extensionData));
                data.Add("latitude", 0);
                data.Add("longitude", 0);
                data.Add("eventSource", "PostDetailView");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public Task delete_blog(string blogId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog/{blogId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            } catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task delete_wiki(string wikiId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/item/{wikiId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            } catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task repost_blog(string postId, Types.Repost_Types type, string content = null)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("content", content);
                data.Add("refObjectId", postId);
                data.Add("refObjectType", (int)type);
                data.Add("type", 2);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }catch(Exception e) { throw new Exception(e.Message); }
        }


        public Task check_in(int? timezone = null)
        {
            try
            {
                int? tz;
                if (timezone != null) { tz = timezone; } else { tz = helpers.getTimezone(); }
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/check-in");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add("timezone", tz);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task repair_check_in(Types.Repair_Types repairType)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/check-in/repair");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add("repairMethod", Convert.ToString((int)repairType));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task lottery(int? timezone = null)
        {
            try
            {
                int? tz;
                if (timezone != null) { tz = timezone; } else { tz = helpers.getTimezone(); }
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/check-in");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add("timezone", tz);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public Task edit_profile(string nickname = null, string content = null, byte[] icon = null, List<byte[]> imageList = null, List<string> captionList = null, string backgroundColor = null, byte[] backgroundImage = null, string defaultChatBubbleId = null)
        {
            try
            {
                JObject data = new JObject();

                JArray mediaList = new JArray();
                JObject extensionData = new JObject();

                if (imageList != null)
                {
                    JArray tempMedia = new JArray();
                    for(int i = 0; i != imageList.Count; i++)
                    {
                        tempMedia = new JArray();
                        tempMedia.Add(100);
                        tempMedia.Add(this.client.upload_media(imageList[i], Types.upload_File_Types.Image));
                        tempMedia.Add(captionList[i]);
                        mediaList.Add(tempMedia);
                    }
                }
                if (backgroundColor != null) { JObject color = new JObject(); color.Add("backgroundColor", backgroundColor); extensionData.Add(new JProperty("style", color)); }
                if(backgroundImage != null) { JObject bgImg = new JObject(); JArray bgArr = new JArray(); bgArr.Add(100);bgArr.Add(this.client.upload_media(backgroundImage, Types.upload_File_Types.Image));bgArr.Add(null); bgArr.Add(null); bgArr.Add(null); bgImg.Add("backgroundMediaList", new JArray(bgArr)); extensionData.Add(new JProperty("style", bgImg)); }
                if(defaultChatBubbleId != null) { JObject dchtbl = new JObject(); dchtbl.Add("defaultBubbleId", defaultChatBubbleId); extensionData.Add(dchtbl); }


                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add(new JProperty("extensions", extensionData));
                if(nickname != null) { data.Add("nickname", nickname); }
                if(content != null) { data.Add("content", content); }
                if(icon != null) { data.Add("icon", this.client.upload_media(icon, Types.upload_File_Types.Image)); }

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/user-profile/{this.client.userID}");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) throw new Exception(response.Content);
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task vote_poll(string postId, string optionId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog/{postId}/poll/option/{optionId}/vote");
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add("eventSource", "PostDetailView");
                data.Add("value", 1);
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task comment(string message, Amino.Types.Comment_Types type, string targetId, bool isGuest = false)
        {
            try
            {
                string endPoint = null;
                JObject data = new JObject();
                data.Add("content", message);
                data.Add("strickerId", null);
                data.Add("type", 0);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                string comType;
                if (isGuest) { comType = "g-comment"; } else { comType = "comment"; }
                switch(type)
                {
                    case Types.Comment_Types.Blog:
                        data.Add("eventSource", "PostDetailView");
                        endPoint = $"/x{communityId}/s/blog/{targetId}/{comType}";
                        break;
                    case Types.Comment_Types.Wiki:
                        data.Add("eventSource", "PostDetailView");
                        break;
                    case Types.Comment_Types.Reply:
                        data.Add("respondTo", targetId);
                        endPoint = $"/x{communityId}/s/item/{targetId}/{comType}";
                        break;
                    case Types.Comment_Types.User:
                        data.Add("eventSource", "UserProfileView");
                        endPoint = $"/x{communityId}/s/user-profile/{targetId}/{comType}";
                        break;
                }

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task delete_comment(string commentId, Amino.Types.Comment_Types type, string targetId)
        {
            try
            {
                string endPoint = null;
                switch(type)
                {
                    case Types.Comment_Types.Blog:
                        endPoint = $"/x{communityId}/s/blog/{targetId}/comment/{commentId}";
                        break;
                    case Types.Comment_Types.Wiki:
                        endPoint = $"/x{communityId}/s/item/{targetId}/comment/{commentId}";
                        break;
                    case Types.Comment_Types.Reply:
                        break;
                    case Types.Comment_Types.User:
                        endPoint = $"/x{communityId}/s/user-profile/{targetId}/comment/{commentId}";
                        break;
                }


                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
        



        public void Dispose()
        {
            this.communityId = null;
            this.client = null;
            this.headers = null;
        }


    }


}
