using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class ParticipatedExperiments
    {
        [JsonPropertyName("chatMembersCommonChannel")] public int ChatMembersCommonChannel { get; set; }
        [JsonPropertyName("couponPush")] public int CouponPush { get; set; }
        [JsonPropertyName("communityMembersCommonChannel")] public int CommunityMembersCommonChannel { get; set; }
        [JsonPropertyName("communityTabExp")] public int CommunityTabExp { get; set; }
        [JsonPropertyName("userVectorCommunitySimilarityChannel")] public int UserVectorCommunitySimilarityChannel { get; set; }
    }
}
