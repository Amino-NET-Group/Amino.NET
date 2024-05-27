using System.Text.Json.Serialization;

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
