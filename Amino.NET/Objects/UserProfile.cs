using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amino.Objects
{

    public class UserProfile
    {
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("moodSticker")]public string MoodSticker { get; set; }
        [JsonPropertyName("itemsCount")]public int ItemsCount { get; set; }
        [JsonPropertyName("consecutiveCheckInDays")]public int ConsecutiveCheckInDays { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("followingStatus")]public int FollowingStatus { get; set; }
        [JsonPropertyName("onlineStatus")]public int OnlineStatus { get; set; }
        [JsonPropertyName("accountMembership")]public int AccountMembershipStatus { get; set; }
        [JsonPropertyName("isGlobal")]public bool IsGlobal { get; set; }
        [JsonPropertyName("reputation")]public int Reputation { get; set; }
        [JsonPropertyName("postsCount")]public int PostsCount { get; set; }
        [JsonPropertyName("membersCount")]public int MembersCount { get; set; }
        [JsonPropertyName("nickname")]public string Nickname { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("isNicknameVerified")]public bool IsNicknameVerified { get; set; }
        [JsonPropertyName("level")]public int Level { get; set; }
        [JsonPropertyName("notificationSubscriptionStatus")]public int notificationSubscriptionStatus { get; set; }
        [JsonPropertyName("pushEnabled")]public bool PushEnabled { get; set; }
        [JsonPropertyName("membershipStatus")]public int MembershipStatus { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("joinedCount")]public int JoinedCount { get; set; }
        [JsonPropertyName("role")]public int Role { get; set; }
        [JsonPropertyName("commentsCount")]public int CommentsCount { get; set; }
        [JsonPropertyName("aminoId")]public string AminoId { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("storiesCount")]public int StoriesCount { get; set; }
        [JsonPropertyName("blogsCount")]public int BlogsCount { get; set; }
        [JsonPropertyName("extensions")]public GenericProfileExtensions Extensions { get; set; }
        [JsonPropertyName("influencerInfo")]public InfluencerInfo InfluencerInfo { get; set; }
        [JsonPropertyName("fanClubList")] public List<InfluencerFanClubMember> FanClubList { get; set; }
    }
}