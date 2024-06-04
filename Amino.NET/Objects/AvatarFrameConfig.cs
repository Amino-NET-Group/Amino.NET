using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class AvatarFrameConfig
    {
        [JsonPropertyName("avattarFramePath")] public string AvatarFramePath { get; set; }
        [JsonPropertyName("id")] public string ObjectId { get; set; }
        [JsonPropertyName("moodColor")] public string MoodColor { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("version")] public int? Version { get; set; }
        [JsonPropertyName("userIconBorderColor")] public string UserIconBorderColor { get; set; }
    }
}
