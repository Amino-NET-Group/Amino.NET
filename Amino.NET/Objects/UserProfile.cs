using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("influencerInfo")]public InfluencerInfo InfluencerInfo { get; set; }
        [JsonPropertyName("fanClubList")] public List<InfluencerFanClubMember> FanClubList { get; set; }

        public UserProfile(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            try { status = (int)jsonObj["status"]; } catch { }
            try { moodSticker = (string)jsonObj["moodSticker"]; } catch { }
            try { itemsCount = (int)jsonObj["itemsCount"]; } catch { }
            try { if (jsonObj["consecutiveCheckInDays"] != null) { consecutiveCheckInDays = (int)jsonObj["consecutiveCheckInDays"]; } } catch { }
            try { userId = (string)jsonObj["uid"]; } catch{ }
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
            try { level = (int)jsonObj["level"]; } catch { }
            try { notificationSubscriptionStatus = (int)jsonObj["notificationSubscriptionStatus"]; } catch { }
            try { pushEnabled = (bool)jsonObj["pushEnabled"]; } catch { }
            try { membershipStatus = (int)jsonObj["membershipStatus"]; } catch { }
            try { if (jsonObj["content"] != null) { content = (string)jsonObj["content"]; } } catch { }
            try { joinedCount = (int)jsonObj["joinedCount"]; } catch { }
            try { role = (int)jsonObj["role"]; } catch { }
            try { commentsCount = (int)jsonObj["commentsCount"]; } catch { }
            try { aminoId = (string)jsonObj["aminoId"]; } catch { }
            try { communityId = (int)jsonObj["ndcId"]; } catch { }
            try { createdTime = (string)jsonObj["createdTime"]; } catch { }
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
                try { defaultBubbleId = (string)jsonObj["extensions"]["defaultBubbleId"]; } catch { }
            }
        }


    }
}