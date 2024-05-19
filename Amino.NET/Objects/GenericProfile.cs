using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class GenericProfile // ROOT JSON ELEMENT: userProfile
    {
        public string json { get; set; } // NEEDS TO BE SET AFTER
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("isNicknameVerified")]public bool IsNicknameVerified { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("level")]public int Level { get; set; }
        [JsonPropertyName("followingStatus")]public int FollowingStatus { get; set; }
        [JsonPropertyName("accountMembershipStatus")]public int AccountMembershipStatus { get; set; }
        [JsonPropertyName("isGlobal")]public bool IsGlobal { get; set; }
        [JsonPropertyName("membershipStatus")]public int MembershipStatus { get; set; }
        [JsonPropertyName("avatarFrameId")]public string AvatarFrameId { get; set; }
        [JsonPropertyName("reputation")]public int Reputation { get; set; }
        [JsonPropertyName("membersCount")]public int MembersCount { get; set; }
        [JsonPropertyName("nickname")]public string Nickname { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }

        [JsonPropertyName("avatarFrame")] public GenericAvatarFrame AvatarFrame { get; set; }
        [JsonPropertyName("influencerInfo")] public InfluencerPriceInfo InfluencerInfo { get; set; }

    }
}
