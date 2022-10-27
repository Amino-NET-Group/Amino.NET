using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserVisitor
    {
        public int visitorsCount { get; private set; }
        public int capacity { get; private set; }
        public int visitorPrivacyMode { get; private set; }
        public int ownerPrivacyMode { get; private set; }
        public string visitTime { get; private set; }
        public _Profile Profile { get; private set; }


        public UserVisitor(JObject _profile, JObject _baseJson)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_baseJson.ToString());
            dynamic profileObj = (JObject)JsonConvert.DeserializeObject(_profile.ToString());
            visitorsCount = (int)jsonObj["visitorsCount"];
            capacity = (int)jsonObj["capacity"];
            visitorPrivacyMode = (int)profileObj["visitorPrivacyMode"];
            ownerPrivacyMode = (int)profileObj["ownerPrivacyMode"];
            visitTime = (string)profileObj["visitTime"];
            Profile = new _Profile(_profile);
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Profile
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

            public _Profile(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["profile"]["status"];
                isNicknameVerified = (bool)jsonObj["profile"]["isNicknameVerified"];
                userId = (string)jsonObj["profile"]["uid"];
                level = (int)jsonObj["profile"]["level"];
                followingStatus = (int)jsonObj["profile"]["followingStatus"];
                accountMembershipStatus = (int)jsonObj["profile"]["accountMembershipStatus"];
                isGlobal = (bool)jsonObj["profile"]["isGlobal"];
                membershipStatus = (int)jsonObj["profile"]["membershipStatus"];
                avatarFrameId = (string)jsonObj["profile"]["avatarFrameId"];
                reputation = (int)jsonObj["profile"]["reputation"];
                role = (int)jsonObj["profile"]["role"];
                aminoId = (string)jsonObj["profile"]["aminoId"];
                communityId = (int)jsonObj["profile"]["ndcId"];
                membersCount = (int)jsonObj["profile"]["membersCount"];
                nickname = (string)jsonObj["profile"]["nickname"];
                iconUrl = (string)jsonObj["profile"]["icon"];
            }
        }
    }
}
