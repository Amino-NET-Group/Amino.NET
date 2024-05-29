using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class StickerCollectionSummary
    {
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("collectionType")] public int? CollectionType { get; set; }
        [JsonPropertyName("uid")] public string ObjectId { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("smallIcon")] public string SmallIconUrl { get; set; }
        [JsonPropertyName("usedCount")] public int? UsedCount { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("collectionId")] public string CollectionId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }

    }
}
