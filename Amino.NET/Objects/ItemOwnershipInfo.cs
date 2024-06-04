using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class ItemOwnershipInfo
    {
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("ownershipStatus")] public int? OwnershipStatus { get; set; }
        [JsonPropertyName("isAutoRenew")] public bool? IsAutoRenew { get; set; }
        [JsonPropertyName("expiredTime")] public string ExpiredTime { get; set; }

    }
}
