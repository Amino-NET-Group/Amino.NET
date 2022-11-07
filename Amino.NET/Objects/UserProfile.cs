using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserProfile
    {
        public int status { get; }
        public string moodSticker { get; }
        public int itemsCount { get; }
        public int consecutiveCheckInDays { get; }
        public string userId { get; }
        public string modifiedTime { get; }
        public int followingStatus { get; }
        public int onlineStatus { get; }
        public int accountMembershipStatus { get; }
        public bool isGlobal { get; }
        public int reputation { get; }
        public int postsCount { get; }
        public int membersCount { get; }
        public string nickname { get; }
        public string iconUrl { get; }
        public bool isNicknameVerified { get; }
        public int level { get; }
        public int notificationSubscriptionStatus { get; }
        public bool pushEnabled { get; }
        public int membershipStatus { get; }
        public string content { get; }
        public int joinedCount { get; }
        public int role { get; }
        public int commentsCount { get; }
        public string aminoId { get; }
        public int communityId { get; }
        public string createdTime { get; }
        public int storiesCount { get; }
        public int blogsCount { get; }
        public string json { get; }
        public _Extensions Extensions { get; }

        public UserProfile(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["status"];
            moodSticker = (string)jsonObj["moodSticker"];
            itemsCount = (int)jsonObj["itemsCount"];
            if(jsonObj["consecutiveCheckInDays"] != null) { consecutiveCheckInDays = (int)jsonObj["consecutiveCheckInDays"]; }
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
            level = (int)jsonObj["level"];
            notificationSubscriptionStatus = (int)jsonObj["notificationSubscriptionStatus"];
            pushEnabled = (bool)jsonObj["pushEnabled"];
            membershipStatus = (int)jsonObj["membershipStatus"];
            if(jsonObj["content"] != null) { content = (string)jsonObj["content"]; }
            joinedCount = (int)jsonObj["joinedCount"];
            role = (int)jsonObj["role"];
            commentsCount = (int)jsonObj["commentsCount"];
            aminoId = (string)jsonObj["aminoId"];
            communityId = (int)jsonObj["ndcId"];
            createdTime = (string)jsonObj["createdTime"];
            json = _json.ToString();
            
            if(jsonObj["extensions"] != null) { Extensions = new _Extensions(_json); }

        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Extensions
        {
            public string defaultBubbleId { get; }

            public _Extensions(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                defaultBubbleId = (string)jsonObj["extensions"]["defaultBubbleId"];
            }
        }


    }
}