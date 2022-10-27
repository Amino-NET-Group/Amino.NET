using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MessageCollection
    {
        public bool includedInSummary { get; private set; }
        public string authorId { get; private set; }
        public bool isHidden { get; private set; }
        public string messageId { get; private set; }
        public int mediaType { get; private set; }
        public string content { get; private set; }
        public string chatBubbleId { get; private set; }
        public int clientRefId { get; private set; }
        public string chatId { get; private set; }
        public string createdTime { get; private set; }
        public int type { get; private set; }
        public string mediaUrl { get; private set; }
        public string json { get; private set; }
        public _Author? Author { get; }
        public _Paging? Paging { get; }

        public MessageCollection(JObject _json, JObject _fullJson)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            includedInSummary = (bool)jsonObj["includedInSummary"];
            authorId = (string)jsonObj["uid"];
            isHidden = (bool)jsonObj["isHidden"];
            messageId = (string)jsonObj["messageId"];
            mediaType = (int)jsonObj["mediaType"];
            content = (string)jsonObj["content"];
            chatBubbleId = (string)jsonObj["chatBubbleId"];
            clientRefId = (int)jsonObj["clientRefId"];
            chatId = (string)jsonObj["threadId"];
            createdTime = (string)jsonObj["createdTime"];
            type = (int)jsonObj["type"];
            mediaUrl = (string)jsonObj["mediaValue"];
            json = _json.ToString();
            Paging = new _Paging(_fullJson);
            Author = new _Author(_json);
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Paging
        {
            public string nextPageToken { get; private set; }
            public string prevPageToken { get; private set; }

            public _Paging(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                
                nextPageToken = (string)jsonObj["paging"]["nextPageToken"];
                prevPageToken = (string)jsonObj["paging"]["prevPageToken"];
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public int status { get; private set; }
            public bool isNicknameVerified { get; private set; }
            public string userId { get; private set; }
            public int level { get; private set; }
            public int followingStatus { get; private set; }
            public int accountMembershipStatus { get; private set; }
            public bool isGlobal { get; private set; }
            public int membershipStatus { get; private set; }
            public string avatarFrameId { get; private set; }
            public int reputation { get; private set; }
            public int role { get; private set; }
            public string aminoId { get; private set; }
            public int communityId { get; private set; }
            public int membersCount { get; private set; }
            public string nickname { get; private set; }
            public string iconUrl { get; private set; }
            public _avatarFrame AvatarFrame { get; }

            public _Author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["author"]["status"];
                isNicknameVerified = (bool)jsonObj["author"]["isNicknameVerified"];
                userId = (string)jsonObj["author"]["uid"];
                level = (int)jsonObj["author"]["level"];
                followingStatus = (int)jsonObj["author"]["followingStatus"];
                accountMembershipStatus = (int)jsonObj["author"]["accountMembershipStatus"];
                avatarFrameId = (string)jsonObj["author"]["avatarFrameId"];
                reputation = (int)jsonObj["author"]["reputation"];
                role = (int)jsonObj["author"]["role"];
                aminoId = (string)jsonObj["author"]["aminoId"];
                communityId = (int)jsonObj["author"]["ndcId"];
                membersCount = (int)jsonObj["author"]["membersCount"];
                nickname = (string)jsonObj["author"]["nickname"];
                iconUrl = (string)jsonObj["author"]["icon"];
                if(jsonObj["author"]["avatarFrame"] != null) { AvatarFrame = new _avatarFrame(_json); }
            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _avatarFrame
            {
                public int status { get; private set; }
                public int version { get; private set; }
                public string resourceUrl { get; private set; }
                public string name { get; private set; }
                public string iconUrl { get; private set; }
                public int frameType { get; private set; }
                public string frameId { get; private set; }

                public _avatarFrame(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    status = (int)jsonObj["author"]["avatarFrame"]["status"];
                    version = (int)jsonObj["author"]["avatarFrame"]["version"];
                    resourceUrl = (string)jsonObj["author"]["avatarFrame"]["resourceUrl"];
                    name = (string)jsonObj["author"]["avatarFrame"]["name"];
                    iconUrl = (string)jsonObj["author"]["avatarFrame"]["icon"];
                    frameType = (int)jsonObj["author"]["avatarFrame"]["frameType"];
                    frameId = (string)jsonObj["author"]["avatarFrame"]["frameId"];
                }
            }
        }
    }
}
