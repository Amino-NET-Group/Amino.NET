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
        public Dictionary<string, string> headers = new Dictionary<string, string>();

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

        /// <summary>
        /// Creates an instance of the Amino.SubClient object and builds headers
        /// </summary>
        /// <param name="_client"></param>
        /// <param name="_communityId"></param>
        public SubClient(Amino.Client _client, string _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId;
            headerBuilder();
        }

        /// <summary>
        /// Creates an instance of the Amino.SubClient object and builds headers
        /// </summary>
        /// <param name="_client"></param>
        /// <param name="_communityId"></param>
        public SubClient(Amino.Client _client, int _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId.ToString();
            headerBuilder();
        }


        /// <summary>
        /// Allows you to get inviite codes of the current community (might require staff permissions)
        /// </summary>
        /// <param name="status"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to generate a Community invite code (might require staff permissions)
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="force"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to delete an invite code using its ID
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to post a Blog post in the current Community
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="imageList"></param>
        /// <param name="fansOnly"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to post a Wiki post on the current Community
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="imageList"></param>
        /// <param name="fansOnly"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to edit an existing Blog on the current Community
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="imageList"></param>
        /// <param name="fansOnly"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to delete a Blog post
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to delete a Wiki post
        /// </summary>
        /// <param name="wikiId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to repost a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to check in on the current Community
        /// </summary>
        /// <param name="timezone"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to repair your check in streak using either Amino Coins or Amino+
        /// </summary>
        /// <param name="repairType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to claim the check in lottery
        /// </summary>
        /// <param name="timezone"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you edit your community profile
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="content"></param>
        /// <param name="icon"></param>
        /// <param name="imageList"></param>
        /// <param name="captionList"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="defaultChatBubbleId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to vote on a poll
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to comment on a Blog, Wiki, Reply, Wall
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <param name="isGuest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Allows you to delete a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="type"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Allows you to like a Post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="isWiki"></param>
        /// <returns></returns>
        public Task like_post(string postId, bool isWiki = false)
        {
            try
            {
                string endPoint = null;

                JObject data = new JObject();
                data.Add("value", 4);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                if(!isWiki) { data.Add("eventSource", "UserProfileView"); endPoint = $"/x{communityId}/s/blog/{postId}/vote?cv=1.2"; } else { data.Add("eventSource", "PostDetailView"); endPoint = $"/x{communityId}/s/item/{postId}/vote?cv=1.2"; }

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to unlike a Post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="isWiki"></param>
        /// <returns></returns>
        public Task unlike_post(string postId, bool isWiki = false)
        {
            try
            {
                string endPoint = null;
                if(!isWiki) { endPoint = $"/x{communityId}/s/blog/{postId}/vote?eventSource=UserProfileView"; } else { endPoint = $"/x{communityId}/s/item/{postId}/vote?eventSource=PostDetailView"; }
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest(endPoint);
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); } 
        }

        /// <summary>
        /// Allows you to like a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public Task like_comment(string commentId, string targetId,Types.Comment_Types targetType)
        {
            try
            {
                string _targetType = null;
                string voteType = null;
                string targetValue = null;
                if(targetType == Types.Comment_Types.User) { _targetType = "UserProfileView"; targetValue = "user-profile"; } else { _targetType = "PostDetailView"; targetValue = "blog"; }
                if(targetType == Types.Comment_Types.Wiki) { voteType = "g-vote"; targetValue = "item"; } else { voteType = "vote";  }
                JObject data = new JObject();
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                data.Add("value", 1);
                data.Add("EventSource", _targetType);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/{targetValue}/{targetId}/comment/{commentId}/{voteType}?cv=1.2&value=1");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }catch(Exception e) { throw new Exception(e.Message); }
        }


        /// <summary>
        /// Allows you to unlike a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="targetId"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public Task unlike_comment(string commentId, string targetId, Amino.Types.Comment_Types targetType)
        {
            try
            {
                string _targetType = null;
                string _eventSource = "PostDetailView";
                if (targetType == Types.Comment_Types.User) { _targetType = "user-profile"; _eventSource = "UserProfileView"; } else if (targetType == Types.Comment_Types.Wiki) { _targetType = "item"; } else { _targetType = "blog"; }
                


                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/{_targetType}/{targetId}/comment/{commentId}/g-vote?eventSource={_eventSource}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        } 

        /// <summary>
        /// Allows you to upvote a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Task upvote_comment(string commentId, string postId)
        {
            try
            {
                JObject data = new JObject();
                data.Add("value", 1);
                data.Add("eventSource", "PostDetailView");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog/{postId}/comment/{commentId}/vote?cv=1.2&value=1");
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddHeaders(headers);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to downvote a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Task downvote_comment(string commentId, string postId)
        {
            try
            {
                JObject data = new JObject();
                data.Add("value", -1);
                data.Add("eventSource", "PostDetailView");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog/{postId}/comment/{commentId}/vote?cv=1.2&value=1");
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddHeaders(headers);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.Delete(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to remove your vote from a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Task unvote_comment(string commentId, string postId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog/{postId}/comment/{commentId}/vote?eventSource=PostDetailView");
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to reply to a wall comment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commentId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task reply_wall(string userId, string commentId, string message)
        {
            try
            {
                JObject data = new JObject();
                data.Add("content", message);
                data.Add("stackId", null);
                data.Add("respondTo", commentId);
                data.Add("type", 0);
                data.Add("eventSource", "UserProfileView");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/user-profile/{userId}/comment");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }

        }

        /// <summary>
        /// Allows you to set if youre online or offline on the community 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task set_activity_status(Types.Activity_Status_Types status)
        {
            try
            {
                JObject data = new JObject();
                if (status == Types.Activity_Status_Types.On) { data.Add("onlineStatus", "on"); } else { data.Add("onlineStatus", "off"); }
                data.Add("duration", 86400);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/user-profile/{this.client.userID}/online-status");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to check your notifications on the current Community
        /// </summary>
        /// <returns></returns>
        public Task check_notification()
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/notification/checked");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to delete a specific notification on the current Community
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public Task delete_notification(string notificationId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/notification/{notificationId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to clear all notifications on the current Community
        /// </summary>
        /// <returns></returns>
        public Task clear_notifications()
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/notification");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to send activity time (farm coins) on the current community (NO ARGUMENTS REQUIRED)
        /// </summary>
        /// <returns></returns>
        public Task send_activity_time()
        {
            try
            {
                JObject data = new JObject();
                JArray timeData = new JArray();

                foreach (Dictionary<string, long> timer in helpers.getTimeData())
                {
                    JObject subData = new JObject();

                    subData.Add("start", timer["start"]);
                    subData.Add("end", timer["end"]);

                    timeData.Add(subData);
                }
                int optInAdsFlags = 2147483647;
                int tzf = helpers.TzFilter();
                data.Add("userActiveTimeChunkList", timeData);
                data.Add("OptInAdsFlags", optInAdsFlags);
                data.Add("timestamp", (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);
                data.Add("timezone", tzf);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/community/stats/user-active-time");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch(Exception e) { throw new Exception(e.Message); }
        }


        /// <summary>
        /// Allows you to start a chat with multiple people on the current Community
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="isGlobal"></param>
        /// <param name="publishToGlobal"></param>
        /// <returns></returns>
        public Task start_chat(List<string> userIds, string message, string title = null, string content = null, bool isGlobal = false, bool publishToGlobal = false)
        {
            try
            {
                JObject data = new JObject();
                data.Add("title", title);
                data.Add("inviteeUids", new JArray(userIds));
                data.Add("initialMessageContent", message);
                data.Add("content", content);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                if(isGlobal) { data.Add("type", 2); data.Add("eventSource", "GlobalComposeMenu"); } else { data.Add("type", 0); }
                if(publishToGlobal) { data.Add("publishToGlobal", 1); } else { data.Add("publishToGlobal", 0); }

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/chat/thread");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to start a chat with a single person on the current Community
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="isGlobal"></param>
        /// <param name="publishToGlobal"></param>
        /// <returns></returns>
        public Task start_chat(string userId, string message, string title = null, string content = null, bool isGlobal = false, bool publishToGlobal = false)
        {
            start_chat(new List<string>() { userId }, message, title, content, isGlobal, publishToGlobal);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to invite multiple people to a chat in the current Community
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task invite_to_chat(List<string> userIds, string chatId)
        {
            try
            {
                JObject data = new JObject();
                data.Add("uids", new JArray(userIds));
                data.Add("timestamp", helpers.GetTimestamp() * 1000);

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/chat/thread/{chatId}/member/invite");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Allows you to invite a single person to a chat in the current Community
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public Task invite_to_chat(string userId, string chatId)
        {
            invite_to_chat(new List<string>() { userId }, chatId);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Allows you to add a user to your favorites
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task add_to_favorites(string userId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/user-group/quick-access/{userId}");
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            }
            catch(Exception e) { throw new Exception(e.Message); }
        }

        /// <summary>
        /// Not to be used in general use (THIS FUNCTION WILL DISPOSE THE SUBCLIENT)
        /// </summary>
        public void Dispose()
        {
            this.communityId = null;
            this.client = null;
            this.headers = null;
        }


    }


}
