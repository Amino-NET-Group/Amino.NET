using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class ItemRestrictionInfo
    {
        [JsonPropertyName("restrictValue")] public int RestrictValue { get; set; }
        [JsonPropertyName("availableDuration")] public string AvailableDuration { get; set; }
        [JsonPropertyName("discountValue")] public int DiscountValue { get; set; }
        [JsonPropertyName("discountStatus")] public int DiscountStatus { get; set; }
        [JsonPropertyName("ownerUserId")] public string OwnerUserId { get; set; }
        [JsonPropertyName("ownerType")] public int OwnerType { get; set; }
        [JsonPropertyName("restrictType")] public int RestrictType { get; set; }
    }
}
