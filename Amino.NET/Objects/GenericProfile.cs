using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GenericProfile
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
        public int membersCount { get; private set; }
        public string nickname { get; private set; }
        public string iconUrl { get; private set; }
        public int communityId { get; private set; }
        public string json { get; private set; }
        [JsonPropertyName("avatarFrame")] public GenericAvatarFrame AvatarFrame { get; set; }
        [JsonPropertyName("influencerInfo")] public InfluencerPriceInfo InfluencerInfo { get; set; }


        public GenericProfile(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());

            status = (int)jsonObj["userProfile"]["status"];
            isNicknameVerified = (bool)jsonObj["userProfile"]["isNicknameVerified"];
            userId = (string)jsonObj["userProfile"]["uid"];
            level = (int)jsonObj["userProfile"]["level"];
            followingStatus = (int)jsonObj["userProfile"]["followingStatus"];
            accountMembershipStatus = (int)jsonObj["userProfile"]["accountMembershipStatus"];
            isGlobal = (bool)jsonObj["userProfile"]["isGlobal"];
            membershipStatus = (int)jsonObj["userProfile"]["membershipStatus"];
            avatarFrameId = (string)jsonObj["userProfile"]["avatarFrameId"];
            reputation = (int)jsonObj["userProfile"]["reputation"];
            membersCount = (int)jsonObj["userProfile"]["membersCount"];
            nickname = (string)jsonObj["userProfile"]["nickname"];
            iconUrl = (string)jsonObj["userProfile"]["icon"];
            communityId = (int)jsonObj["userProfile"]["ndcId"];
            json = _json.ToString();
        }
    }
}
