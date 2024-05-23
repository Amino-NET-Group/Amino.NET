using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class AvatarFrame
    {
        [JsonPropertyName("isGloballyAvailable")]public bool IsGloballyAvailable { get; set; }
        [JsonPropertyName("isNew")]public bool IsNew { get; set; }
        [JsonPropertyName("version")]public int Version { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("frameType")]public int FrameType { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("frameUrl")]public string FrameUrl { get; set; }
        [JsonPropertyName("md5")]public string MD5 { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("frameId")]public string FrameId { get; set; }
        [JsonPropertyName("uid")]public string ObjectId { get; set; }
        [JsonPropertyName("ownershipStatus")]public int OwnershipStatus { get; set; }
        [JsonPropertyName("name")]public string Name { get; set; }
        [JsonPropertyName("resourceUrl")]public string ResourceUrl { get; set; }
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("additionalBenefits")]public AdditionalItemBenefits AdditionalBenefits { get; set; }
        [JsonPropertyName("restrictionInfo")]public ItemRestrictionInfo RestrictionInfo { get; set; }
        [JsonPropertyName("ownershipInfo")]public ItemOwnershipInfo OwnershipInfo { get; set; }
        [JsonPropertyName("config")]public AvatarFrameConfig Config { get; set; }
    }
}
