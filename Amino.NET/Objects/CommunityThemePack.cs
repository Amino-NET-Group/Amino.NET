using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class CommunityThemePack
    {
        [JsonPropertyName("themeColor")] public string ThemeColor { get; set; }
        [JsonPropertyName("themePackHash")] public string ThemePackHash { get; set; }
        [JsonPropertyName("themePackRevision")] public int ThemePackRevision { get; set; }
        [JsonPropertyName("themePackUrl")] public string ThemePackUrl { get; set; }
    }
}
