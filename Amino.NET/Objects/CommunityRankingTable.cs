using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class CommunityRankingTable
    {
        [JsonPropertyName("level")] public int Level { get; set; }
        [JsonPropertyName("reputation")] public int Reputation { get; set; }
        [JsonPropertyName("id")] public string ID { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
    }
}
