using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class GenericChatMember
    {
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("membershipStatus")] public int MembershipStatus { get; set; }
        [JsonPropertyName("role")] public int Role { get; set; }
        [JsonPropertyName("nickname")] public string Nickname { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
    }
}
