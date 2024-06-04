using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class CommunityNewsFeed
    {
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
    }
}
