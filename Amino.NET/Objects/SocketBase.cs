using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class SocketBase
    {
        [JsonPropertyName("alertOption")] public int AlertOption { get; set; }
        [JsonPropertyName("membershipStatus")] public int MembershipStatus { get; set; }
        [JsonPropertyName("ndcId")] public int CommunityId { get; set; }
        [JsonPropertyName("chatBubbleId")] public string ChatBubbleId { get; set; }
    }
}
