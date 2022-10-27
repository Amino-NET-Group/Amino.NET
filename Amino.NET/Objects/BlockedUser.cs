using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BlockedUser
    {
        public int status { get; private set; }
        public bool isNicknameVerified { get; private set; }
        public string userId { get; private set; }
        public int level { get; private set; }
        public int followingStatus { get; private set; }
        public int accountMembershipStatus { get; private set; }
        public bool isGlobal { get; private set; }
        public int membershipStatus { get; private set; }
        public int reputation { get; private set; }
        public int role { get; private set; }
        public int communityId { get; private set; }
        public int membersCount { get; private set; }
        public string nickname { get; private set; }
        public string iconUrl { get; private set; }
        public string json { get; private set; }

        public BlockedUser(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["status"];
            isNicknameVerified = (bool)jsonObj["isNicknameVerified"];
            userId = (string)jsonObj["uid"];
            level = (int)jsonObj["level"];
            followingStatus = (int)jsonObj["followingStatus"];
            accountMembershipStatus = (int)jsonObj["accountMembershipStatus"];
            isGlobal = (bool)jsonObj["isGlobal"];
            membershipStatus = (int)jsonObj["membershipStatus"];
            reputation = (int)jsonObj["reputation"];
            role = (int)jsonObj["role"];
            communityId = (int)jsonObj["ndcId"];
            membersCount = (int)jsonObj["membersCount"];
            nickname = (string)jsonObj["nickname"];
            iconUrl = (string)jsonObj["icon"];
            json = _json.ToString();
        }
    }
}
