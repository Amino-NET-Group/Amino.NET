using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
