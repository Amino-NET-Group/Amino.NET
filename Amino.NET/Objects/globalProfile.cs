using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;


namespace Amino.Objects
{
    // ROOT JSON ELEMENT: userProfile
    public class GlobalProfile
    {
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("itemsCount")]public int ItemsCount { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("followingStatus")]public int FollowingStatus { get; set; }
        [JsonPropertyName("onlineStatus")]public int OnlineStatus { get; set; }
        [JsonPropertyName("accountMembershipStatus")]public int AccountMembershipStatus { get; set; }
        [JsonPropertyName("isGlobal")]public bool IsGlobal { get; set; }
        [JsonPropertyName("avatarFrameId")]public string AvatarFrameId { get; set; }
        [JsonPropertyName("reputation")]public int Reputation { get; set; }
        [JsonPropertyName("postsCount")]public int PostsCount { get; set; }
        [JsonPropertyName("membersCount")]public int MemberCount { get; set; }
        [JsonPropertyName("nickname")]public string Nickname { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("isNicknameVerified")]public bool IsNicknameVerified { get; set; }
        [JsonPropertyName("visitorsCount")]public int VisitorsCount { get; set; }
        [JsonPropertyName("level")]public int Level { get; set; }
        [JsonPropertyName("notificationSubscriptionStatus")]public int NotificationSubscriptionStatus { get; set; }
        [JsonPropertyName("pushEnabled")]public bool PushEnabled { get; set; }
        [JsonPropertyName("membershipStatus")]public int MembershipStatus { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("joinedCount")]public int JoinedCount { get; set; }
        [JsonPropertyName("role")]public int Role { get; set; }
        [JsonPropertyName("commentsCount")]public int CommentsCount { get; set; }
        [JsonPropertyName("aminoId")]public string AminoId { get; set; }
        [JsonPropertyName("ndcId")]public string CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("visitPrivacy")]public int VisitPrivacy { get; set; }
        [JsonPropertyName("storiesCount")]public int StoriesCount { get; set; }
        [JsonPropertyName("blogsCount")]public int BlogsCount { get; set; }
        [JsonPropertyName("avatarFrame")]public GenericAvatarFrame UserProfileFrame { get; set; }
    }


}
