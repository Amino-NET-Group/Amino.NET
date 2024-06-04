using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class WalletHistoryExtraData
    {
        [JsonPropertyName("objectDeeplinkUrl")] public string ObjectDeeplinkUrl { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("subtitle")] public string Subtitle { get; set; }
    }
}
