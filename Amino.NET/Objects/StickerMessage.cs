using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class StickerMessage
    {
        public int _type { get; }
        public int communityId { get; }
        public int alertOption { get; }
        public int membershipStatus { get; }

        public string mediaValue { get; }
        public string chatId { get; }
        public int mediaType { get; }
        public int clientRefId { get; }
        public string messageId { get; }
        public string userId { get; }
        public string createdTime { get; }
        public int type { get; }
        public bool isHidden { get; }
        public bool includedInSummary { get; }
        public string chatBubbleId { get; }
        public int chatBubbleVersion { get; }
        public string json { get; }
        public _Author Author { get; }
        public _Extensions Extensions { get; }

        public StickerMessage(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            _type = (int)jsonObj["t"];
            communityId = (int)jsonObj["o"]["ndcId"];
            alertOption = (int)jsonObj["o"]["alertOption"];
            membershipStatus = (int)jsonObj["o"]["membershipStatus"];


            mediaValue = (string)jsonObj["o"]["chatMessage"]["mediaValue"];
            chatId = (string)jsonObj["o"]["chatMessage"]["threadId"];
            mediaType = (int)jsonObj["o"]["chatMessage"]["mediaType"];
            clientRefId = (int)jsonObj["o"]["chatMessage"]["clientRefId"];
            messageId = (string)jsonObj["o"]["chatMessage"]["messageId"];
            userId = (string)jsonObj["o"]["chatMessage"]["uid"];
            createdTime = (string)jsonObj["o"]["chatMessage"]["createdTime"];
            type = (int)jsonObj["o"]["chatMessage"]["type"];
            isHidden = (bool)jsonObj["o"]["chatMessage"]["isHidden"];
            includedInSummary = (bool)jsonObj["o"]["chatMessage"]["includedInSummary"];
            chatBubbleId = (string)jsonObj["o"]["chatMessage"]["chatBubbleId"];
            chatBubbleVersion = (int)jsonObj["o"]["chatMessage"]["chatBubbleVersion"];
            if (jsonObj["o"]["chatMessage"]["author"] != null) { Author = new _Author(_json); }
            if (jsonObj["o"]["chatMessage"]["extensions"] != null) { Extensions = new _Extensions(_json); }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Extensions
        {
            public string originalStickerId { get; }
            public _Sticker Sticker { get; }

            public _Extensions(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                originalStickerId = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["originalStickerId"];
                Sticker = new _Sticker(_json);
            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _Sticker
            {
                public int status { get; }
                public string iconV2Url { get; }
                public string stickerId { get; }
                public string smallIconV2Url { get; }
                public string smallIconUrl { get; }
                public string stickerCollectionId { get; }
                public string mediumIconUrl { get; }
                public int usedCount { get; }
                public string mediumIconV2Url { get; }
                public string createdTime { get; }
                public string iconUrl { get; }
                public _StickerCollectionSummary StickerCollectionSummary { get; }


                public _Sticker(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    status = (int)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["status"];
                    iconV2Url = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["iconV2"];
                    stickerId = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerId"];
                    smallIconV2Url = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["smallIconV2"];
                    smallIconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["smallIcon"];
                    stickerCollectionId = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionId"];
                    mediumIconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["mediumIcon"];
                    usedCount = (int)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["usedCount"];
                    mediumIconV2Url = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["mediumIconV2"];
                    createdTime = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["createdTime"];
                    iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["icon"];
                    StickerCollectionSummary = new _StickerCollectionSummary(_json);
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _StickerCollectionSummary
                {
                    public int status { get; }
                    public int collectionType { get; }
                    public string userId { get; }
                    public string modifiedTime { get; }
                    public string smallIconUrl { get; }
                    public int usedCount { get; }
                    public string iconUrl { get; }
                    public string name { get; }
                    public string collectionId { get; }
                    public string createdTime { get; }

                    public _StickerCollectionSummary(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        status = (int)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["status"];
                        collectionType = (int)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["collectionType"];
                        userId = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["uid"];
                        modifiedTime = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["modifiedTime"];
                        smallIconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["smallIcon"];
                        usedCount = (int)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["usedCount"];
                        iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["icon"];
                        name = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["name"];
                        collectionId = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["collectionId"];
                        createdTime = (string)jsonObj["o"]["chatMessage"]["author"]["extensions"]["sticker"]["stickerCollectionSummary"]["createdTime"];
                    }
                }
            }
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
                if(jsonObj["o"]["chatMessage"]["author"]["icon"] != null) { iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["icon"]; }
                reputation = (int)jsonObj["o"]["chatMessage"]["author"]["reputation"];
                role = (int)jsonObj["o"]["chatMessage"]["author"]["role"];
                nickname = (string)jsonObj["o"]["chatMessage"]["author"]["nickname"];
                level = (int)jsonObj["o"]["chatMessage"]["author"]["level"];
                accountMembershipStatus = (int)jsonObj["o"]["chatMessage"]["author"]["accountMembershipStatus"];
                if(jsonObj["o"]["chatMessage"]["author"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
                if(jsonObj["o"]["chatMessage"]["author"]["influencerInfo"] != null) { InfluencerInfo = new _InfluencerInfo(_json); }
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
