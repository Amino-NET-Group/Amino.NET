using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{

    public class userProfile
    {
        public int? status { get; private set; }
        public string? moodSticker { get; private set; }
        public int? itemsCount { get; private set; }
        public int? checkInStreak { get; private set; }
        public string? userId { get; private set; }
        public string? modifiedTime { get; private set; }
        public int? followingStatus { get; private set; }
        public int? onlineStatus { get; private set; }
        public int? accountMembershipStatus { get; private set; }
        public bool? isGlobal { get; private set; }
        public string? avatarFrameId { get; private set; }
        public int? reputation { get; private set; }
        public int? postsCount { get; private set; }
        public int? memberCount { get; private set; }
        public string? nickname { get; private set; }
        public string? iconUrl { get; private set; }
        public bool? isNicknameVerified { get; private set; }
        public int? visitorsCount { get; private set; }
        public int? level { get; private set; }
        public int? notificationSubscriptionStatus { get; private set; }
        public bool? pushEnabled { get; private set; }
        public int? membershipStatus { get; private set; }
        public string? content { get; private set; }
        public int? joinedCount { get; private set; }
        public int? role { get; private set; }
        public int? commentsCount { get; private set; }
        public string? aminoId { get; private set; }
        public string? communityId { get; private set; }
        public string? createdTime { get; private set; }
        public int? visitPrivacy { get; private set; }
        public int? storiesCount { get; private set; }
        public int? blogsCount { get; private set; }
        public string? json { get; private set; }
        public userFrame? userProfileFrame { get; }



        public userProfile(JObject _json)
        {

            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["userProfile"]["status"];
            itemsCount = (int)jsonObj["userProfile"]["itemsCount"];
            checkInStreak = (int)jsonObj["userProfile"]["consecutiveCheckInDays"];
            userId = (string)jsonObj["userProfile"]["uid"];
            modifiedTime = (string)jsonObj["userProfile"]["modifiedTime"];
            followingStatus = (int)jsonObj["userProfile"]["followingStatus"];
            onlineStatus = (int)jsonObj["userProfile"]["onlineStatus"];
            accountMembershipStatus = (int)jsonObj["userProfile"]["accountMembershipStatus"];
            isGlobal = (bool)jsonObj["userProfile"]["isGlobal"];
            avatarFrameId = (string)jsonObj["userProfile"]["avatarFrameId"];
            reputation = (int)jsonObj["userProfile"]["reputation"];
            postsCount = (int)jsonObj["userProfile"]["postsCount"];
            memberCount = (int)jsonObj["userProfile"]["usersCount"];
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
            userProfileFrame = new userFrame(_json);

        }
        public class userFrame
        {
            public int? status { get; private set; }
            public int? ownershipStatus { get; private set; }
            public int? version { get; private set; }
            public string? resourceUrl { get; private set; }
            public string? name { get; private set; }
            public string? iconUrl { get; private set; }
            public int? frameType { get; private set; }
            public string? frameId { get; private set; }

            public userFrame(JObject _json)
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
