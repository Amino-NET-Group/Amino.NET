using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class InfluencerPriceInfo
    {
        [JsonPropertyName("isPinned")] public bool? IsPinned { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("fansCount")] public int? FanCount { get; set; }
        [JsonPropertyName("monthlyFee")] public int? MonthlyFee { get; set; }
    }
}
