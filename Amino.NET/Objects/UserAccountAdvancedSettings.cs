using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class UserAccountAdvancedSettings
    {
        [JsonPropertyName("analyticsEnabled")] public int? AnalyticsEnabled { get; set; }
    }
}
