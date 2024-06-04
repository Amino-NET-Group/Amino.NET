using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class Sticker
    {
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("iconV2")] public string IconV2Url { get; set; }
        [JsonPropertyName("stickerId")] public string StickerId { get; set; }
        [JsonPropertyName("smallIconV2")] public string SmallIconV2Url { get; set; }
        [JsonPropertyName("smallIcon")] public string SmallIconUrl { get; set; }
        [JsonPropertyName("stickerCollectionId")] public string StickerCollectionId { get; set; }
        [JsonPropertyName("mediumIcon")] public string MediumIconUrl { get; set; }
        [JsonPropertyName("usedCount")] public int? UsedCount { get; set; }
        [JsonPropertyName("mediumIconV2")] public string MediumIconV2Url { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("stickerCollectionSummary")] StickerCollectionSummary StickerCollectionSummary { get; set; }

    }
}
