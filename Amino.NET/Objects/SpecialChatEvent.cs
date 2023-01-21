using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SpecialChatEvent
    {

        public int _type { get; }
        public int communityId { get; }
        public int alertOption { get; }
        public int membershipStatus { get; }

        public string chatId { get; }
        public int mediaType { get; }
        public int clientRefId { get; }
        public string messageId { get; }
        public string userId { get; }
        public string createdTime { get; }
        public int type { get; }
        public bool isHidden { get; }
        public bool includedInSummary { get; }
        public string json { get; }
        public _Author Author { get; }

        public SpecialChatEvent(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            _type = (int)jsonObj["t"];
            communityId = (int)jsonObj["o"]["ndcId"];
            alertOption = (int)jsonObj["o"]["alertOption"];
            membershipStatus = (int)jsonObj["o"]["membershipStatus"];

            chatId = (string)jsonObj["o"]["chatMessage"]["threadId"];
            mediaType = (int)jsonObj["o"]["chatMessage"]["mediaType"];
            clientRefId = (int)jsonObj["o"]["chatMessage"]["clientRefId"];
            messageId = (string)jsonObj["o"]["chatMessage"]["messageId"];
            userId = (string)jsonObj["o"]["chatMessage"]["uid"];
            createdTime = (string)jsonObj["o"]["chatMessage"]["createdTime"];
            type = (int)jsonObj["o"]["chatMessage"]["type"];
            isHidden = (bool)jsonObj["o"]["chatMessage"]["isHidden"];
            includedInSummary = (bool)jsonObj["o"]["chatMessage"]["includedInSummary"];
            json = _json.ToString();
            Author = new _Author(_json);
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public string userId { get; }
            public int status { get; }
            public string iconUrl { get; }
            public int reputation { get; }
            public int role { get; }
            public string nickname { get; }
            public int level { get; }
            public int accountMembershipStatus { get; }
            public _AvatarFrame AvatarFrame { get; }
            public _InfluencerInfo InfluencerInfo { get; }

            public _Author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                userId = (string)jsonObj["o"]["chatMessage"]["author"]["uid"];
                status = (int)jsonObj["o"]["chatMessage"]["author"]["status"];
                if (jsonObj["o"]["chatMessage"]["author"]["icon"] != null) { iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["icon"]; }
                reputation = (int)jsonObj["o"]["chatMessage"]["author"]["reputation"];
                role = (int)jsonObj["o"]["chatMessage"]["author"]["role"];
                nickname = (string)jsonObj["o"]["chatMessage"]["author"]["nickname"];
                level = (int)jsonObj["o"]["chatMessage"]["author"]["level"];
                accountMembershipStatus = (int)jsonObj["o"]["chatMessage"]["author"]["accountMembershipStatus"];
                if (jsonObj["o"]["chatMessage"]["author"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
                if (jsonObj["o"]["chatMessage"]["author"]["influencerInfo"] != null) { InfluencerInfo = new _InfluencerInfo(_json); }


            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _AvatarFrame
            {
                public int status { get; }
                public int version { get; }
                public string resourceUrl { get; }
                public string name { get; }
                public string iconUrl { get; }
                public int frameType { get; }
                public string frameId { get; }

                public _AvatarFrame(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    status = (int)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["status"];
                    version = (int)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["version"];
                    resourceUrl = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["resourceUrl"];
                    name = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["name"];
                    iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["icon"];
                    frameType = (int)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["frameType"];
                    frameId = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["frameId"];
                }
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _InfluencerInfo
            {
                public int fansCount { get; }
                public int monthlyFee { get; }

                public _InfluencerInfo(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    fansCount = (int)jsonObj["o"]["chatMessage"]["author"]["influencerInfo"]["fansCount"];
                    monthlyFee = (int)jsonObj["o"]["chatMessage"]["author"]["influencerInfo"]["monthlyFee"];
                }
            }
        }



    }
}
