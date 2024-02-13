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
            try { status = (int)jsonObj["status"]; } catch { }
            try { moodSticker = (string)jsonObj["moodSticker"]; } catch { }
            try { itemsCount = (int)jsonObj["itemsCount"]; } catch { }
            try { consecutiveCheckInDays = (string)jsonObj["consecutiveCheckInDays"]; } catch { }
            try { userId = (string)jsonObj["uid"]; } catch { }
            try { modifiedTime = (string)jsonObj["modifiedTime"]; } catch { }
            try { followingStatus = (int)jsonObj["followingStatus"]; } catch { }
            try { onlineStatus = (int)jsonObj["onlineStatus"]; } catch { }
            try { accountMembershipStatus = (int)jsonObj["accountMembershipStatus"]; } catch { }
            try { isGlobal = (bool)jsonObj["isGlobal"]; } catch { }
            try { reputation = (int)jsonObj["reputation"]; } catch { }
            try { postsCount = (int)jsonObj["postsCount"]; } catch { }
            try { membersCount = (int)jsonObj["membersCount"]; } catch { }
            try { nickname = (string)jsonObj["nickname"]; } catch { }
            try { iconUrl = (string)jsonObj["icon"]; } catch { }
            try { isNicknameVerified = (bool)jsonObj["isNicknameVerified"]; } catch { }
            try { mood = (string)jsonObj["mood"]; } catch { }
            try { level = (int)jsonObj["level"]; } catch { }
            try { notificationSubscriptionStatus = (int)jsonObj["notificationSubscriptionStatus"]; } catch { }
            try { if (jsonObj["pushEnabled"] != null) { pushEnabled = (bool)jsonObj["pushEnabled"]; } } catch { }
            try { membershipStatus = (int)jsonObj["membershipStatus"]; } catch { }
            try { content = (string)jsonObj["content"]; } catch { }
            try { joinedCount = (int)jsonObj["joinedCount"]; } catch { }
            try { role = (int)jsonObj["role"]; } catch { }
            try { commentsCount = (int)jsonObj["commentsCount"]; } catch { }
            try { aminoId = (string)jsonObj["aminoId"]; } catch { }
            try { communityId = (int)jsonObj["ndcId"]; } catch { }
            try { createdTime = (string)jsonObj["createdTime"]; } catch { }
            try { storiesCount = (int)jsonObj["storiesCount"]; } catch { }
            try { blogsCount = (int)jsonObj["blogsCount"]; } catch { }
            json = _json.ToString();
        }
    }
}
