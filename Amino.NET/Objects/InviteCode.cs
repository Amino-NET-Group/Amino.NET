using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class InviteCode
    {
        public int status { get; }
        public int duration { get; }
        public string invitationId { get; }
        public string inviteUrl { get; }
        public string modifiedTime { get; }
        public int communityId { get; }
        public string createdTime { get; }
        public string json { get; }
        public _Author Author { get; }


        public InviteCode(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["status"];
            duration = (int)jsonObj["duration"];
            invitationId = (string)jsonObj["invitationId"];
            inviteUrl = (string)jsonObj["link"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            communityId = (int)jsonObj["ndcId"];
            json = _json.ToString();
            Author = new _Author(_json);
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public int status { get; }
            public bool isNicknameVerified { get; }
            public string userId { get; }
            public int level { get; }
            public int followingStatus { get; }
            public int accountMembershipStatus { get; }
            public bool isGlobal { get; }
            public int membershipStatus { get; }
            public string avatarFrameId { get; }
            public int reputation { get; }
            public int role { get; }
            public int communityId { get; }
            public int membersCount { get; }
            public string nickname { get; }
            public string iconUrl { get; }
            public _InfluencerInfo InfluencerInfo { get; }
            public _AvatarFrame AvatarFrame { get; }


            public _Author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["author"]["status"];
                isNicknameVerified = (bool)jsonObj["author"]["isNicknameVerified"];
                userId = (string)jsonObj["author"]["uid"];
                level = (int)jsonObj["author"]["level"];
                followingStatus = (int)jsonObj["author"]["followingStatus"];
                accountMembershipStatus = (int)jsonObj["author"]["accountMembershipStatus"];
                isGlobal = (bool)jsonObj["author"]["isGlobal"];
                membershipStatus = (int)jsonObj["author"]["membershipStatus"];
                avatarFrameId = (string)jsonObj["author"]["avatarFrameId"];
                reputation = (int)jsonObj["author"]["reputation"];
                role = (int)jsonObj["author"]["role"];
                communityId = (int)jsonObj["author"]["ndcId"];
                membersCount = (int)jsonObj["author"]["membersCount"];
                nickname = (string)jsonObj["author"]["nickname"];
                if (jsonObj["author"]["icon"] != null) { iconUrl = (string)jsonObj["author"]["icon"]; }
                if(jsonObj["author"]["influencerInfo"] != null) { InfluencerInfo = new _InfluencerInfo(_json); }
                if(jsonObj["author"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }

            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _InfluencerInfo
            {
                public bool pinned { get; }
                public string createdTime { get; }
                public int fansCount { get; }
                public int monthlyFee { get; }

                public _InfluencerInfo(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    pinned = (bool)jsonObj["author"]["influencerInfo"]["pinned"];
                    createdTime = (string)jsonObj["author"]["influencerInfo"]["createdTime"];
                    fansCount = (int)jsonObj["author"]["influencerInfo"]["fansCount"];
                    monthlyFee = (int)jsonObj["author"]["influencerInfo"]["monthlyFee"];
                }
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _AvatarFrame
            {
                public int status { get; }
                public string ownershipStatus { get; }
                public int version { get; }
                public string resourceUrl { get; }
                public string name { get; }
                public string iconUrl { get; }
                public string frameId { get; }

                public _AvatarFrame(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    status = (int)jsonObj["author"]["avatarFrame"]["status"];
                    ownershipStatus = (string)jsonObj["author"]["avatarFrame"]["ownershipStatus"];
                    version = (int)jsonObj["author"]["avatarFrame"]["version"];
                    resourceUrl = (string)jsonObj["author"]["avatarFrame"]["resourceUrl"];
                    name = (string)jsonObj["author"]["avatarFrame"]["name"];
                    iconUrl = (string)jsonObj["author"]["avatarFrame"]["icon"];
                    frameId = (string)jsonObj["author"]["avatarFrame"]["frameId"];
                }


            }
        }
    }
}
