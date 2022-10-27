using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserFollowings
    {
        public int status { get; private set; }
        public string moodSticker { get; private set; }
        public int itemsCount { get; private set; }
        public string consecutiveCheckInDays { get; private set; }
        public string userId { get; private set; }
        public string modifiedTime { get; private set; }
        public int followingStatus { get; private set; }
        public int onlineStatus { get; private set; }
        public int accountMembershipStatus { get; private set; }
        public bool isGlobal { get; private set; }
        public int reputation { get; private set; }
        public int postsCount { get; private set; }
        public int membersCount { get; private set; }
        public string nickname { get; private set; }
        public string iconUrl { get; private set; }
        public bool isNicknameVerified { get; private set; }
        public string mood { get; private set; }
        public int level { get; private set; }
        public int notificationSubscriptionStatus { get; private set; }
        public bool pushEnabled { get; private set; }
        public int membershipStatus { get; private set; }
        public int joinedCount { get; private set; }
        public string content { get; private set; }
        public int role { get; private set; }
        public int commentsCount { get; private set; }
        public string aminoId { get; private set; }
        public int communityId { get; private set; }
        public string createdTime { get; private set; }
        public int storiesCount { get; private set; }
        public int blogsCount { get; private set; }
        public string json { get; private set; }


        public UserFollowings(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["status"];
            moodSticker = (string)jsonObj["moodSticker"];
            itemsCount = (int)jsonObj["itemsCount"];
            consecutiveCheckInDays = (string)jsonObj["consecutiveCheckInDays"];
            userId = (string)jsonObj["uid"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            followingStatus = (int)jsonObj["followingStatus"];
            onlineStatus = (int)jsonObj["onlineStatus"];
            accountMembershipStatus = (int)jsonObj["accountMembershipStatus"];
            isGlobal = (bool)jsonObj["isGlobal"];
            reputation = (int)jsonObj["reputation"];
            postsCount = (int)jsonObj["postsCount"];
            membersCount = (int)jsonObj["membersCount"];
            nickname = (string)jsonObj["nickname"];
            iconUrl = (string)jsonObj["icon"];
            isNicknameVerified = (bool)jsonObj["isNicknameVerified"];
            mood = (string)jsonObj["mood"];
            level = (int)jsonObj["level"];
            notificationSubscriptionStatus = (int)jsonObj["notificationSubscriptionStatus"];
            if (jsonObj["pushEnabled"] != null) { pushEnabled = (bool)jsonObj["pushEnabled"]; }
            membershipStatus = (int)jsonObj["membershipStatus"];
            content = (string)jsonObj["content"];
            joinedCount = (int)jsonObj["joinedCount"];
            role = (int)jsonObj["role"];
            commentsCount = (int)jsonObj["commentsCount"];
            aminoId = (string)jsonObj["aminoId"];
            communityId = (int)jsonObj["ndcId"];
            createdTime = (string)jsonObj["createdTime"];
            storiesCount = (int)jsonObj["storiesCount"];
            blogsCount = (int)jsonObj["blogsCount"];
            json = _json.ToString();
        }
    }
}
