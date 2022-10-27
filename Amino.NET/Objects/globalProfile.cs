using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GlobalProfile
    {
        public int? status { get; private set; } = new int?();
        public int? itemsCount { get; private set; } = new int?();
        public string? userId { get; private set; }
        public string? modifiedTime { get; private set; }
        public int? followingStatus { get; private set; } = new int?();
        public int? onlineStatus { get; private set; } = new int?();
        public int? accountMembershipStatus { get; private set; } = new int?();
        public bool? isGlobal { get; private set; }
        public string? avatarFrameId { get; private set; }
        public int? reputation { get; private set; } = new int?();
        public int? postsCount { get; private set; } = new int?();
        public int? memberCount { get; private set; } = new int?();
        public string? nickname { get; private set; }
        public string? iconUrl { get; private set; }
        public bool? isNicknameVerified { get; private set; }
        public int? visitorsCount { get; private set; } = new int?();
        public int? level { get; private set; } = new int?();
        public int? notificationSubscriptionStatus { get; private set; } = new int?();
        public bool? pushEnabled { get; private set; }
        public int? membershipStatus { get; private set; } = new int?();
        public string? content { get; private set; }
        public int? joinedCount { get; private set; } = new int?();
        public int? role { get; private set; } = new int?();
        public int? commentsCount { get; private set; } = new int?();
        public string? aminoId { get; private set; }
        public string? communityId { get; private set; }
        public string? createdTime { get; private set; }
        public int? visitPrivacy { get; private set; } = new int?();
        public int? storiesCount { get; private set; } = new int?();
        public int? blogsCount { get; private set; } = new int?();
        public string? json { get; private set; }
        public UserFrame? UserProfileFrame { get; }



        public GlobalProfile(JObject _json)
        {

            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["userProfile"]["status"];
            itemsCount = (int)jsonObj["userProfile"]["itemsCount"];
            userId = (string)jsonObj["userProfile"]["uid"];
            modifiedTime = (string)jsonObj["userProfile"]["modifiedTime"];
            followingStatus = (int)jsonObj["userProfile"]["followingStatus"];
            onlineStatus = (int)jsonObj["userProfile"]["onlineStatus"];
            accountMembershipStatus = (int)jsonObj["userProfile"]["accountMembershipStatus"];
            isGlobal = (bool)jsonObj["userProfile"]["isGlobal"];
            avatarFrameId = (string)jsonObj["userProfile"]["avatarFrameId"];
            reputation = (int)jsonObj["userProfile"]["reputation"];
            postsCount = (int)jsonObj["userProfile"]["postsCount"];
            memberCount = (int)jsonObj["userProfile"]["membersCount"];
            nickname = (string)jsonObj["userProfile"]["nickname"];
            iconUrl = (string)jsonObj["userProfile"]["icon"];
            isNicknameVerified = (bool)jsonObj["userProfile"]["isNicknameVerified"];
            visitorsCount = (int)jsonObj["userProfile"]["visitorsCount"];
            level = (int)jsonObj["userProfile"]["level"];
            notificationSubscriptionStatus = (int)jsonObj["userProfile"]["notificationSubscriptionStatus"];
            pushEnabled = (bool)jsonObj["userProfile"]["pushEnabled"];
            membershipStatus = (int)jsonObj["userProfile"]["membershipStatus"];
            content = (string)jsonObj["userProfile"]["content"];
            joinedCount = (int)jsonObj["userProfile"]["joinedCount"];
            role = (int)jsonObj["userProfile"]["role"];
            commentsCount = (int)jsonObj["userProfile"]["commentsCount"];
            aminoId = (string)jsonObj["userProfile"]["aminoId"];
            communityId = (string)jsonObj["userProfile"]["ndcId"];
            createdTime = (string)jsonObj["userProfile"]["createdTime"];
            visitPrivacy = (int)jsonObj["userProfile"]["visitPrivacy"];
            storiesCount = (int)jsonObj["userProfile"]["storiesCount"];
            blogsCount = (int)jsonObj["userProfile"]["blogsCount"];
            json = _json.ToString();
            UserProfileFrame = new UserFrame(_json);

        }
        public class UserFrame
        {
            public int? status { get; private set; } = new int?();
            public int? ownershipStatus { get; private set; } = new int?();
            public int? version { get; private set; } = new int?();
            public string? resourceUrl { get; private set; }
            public string? name { get; private set; }
            public string? iconUrl { get; private set; }
            public int? frameType { get; private set; } = new int?();
            public string? frameId { get; private set; }

            public UserFrame(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["userProfile"]["avatarFrame"]["status"];
                ownershipStatus = (int)jsonObj["userProfile"]["avatarFrame"]["ownershipStatus"];
                version = (int)jsonObj["userProfile"]["avatarFrame"]["version"];
                resourceUrl = (string)jsonObj["userProfile"]["avatarFrame"]["resourceUrl"];
                name = (string)jsonObj["userProfile"]["avatarFrame"]["name"];
                iconUrl = (string)jsonObj["userProfile"]["avatarFrame"]["icon"];
                frameType = (int)jsonObj["userProfile"]["avatarFrame"]["frameType"];
                frameId = (string)jsonObj["userProfile"]["avatarFrame"]["frameId"];
            }
        }
    }


}
