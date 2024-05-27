using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class UserAccountExtensions
    {
        [JsonPropertyName("contentLanguage")] public string ContentLanguage { get; set; }
        [JsonPropertyName("adsFlags")] public int AdsFlags { get; set; }
        [JsonPropertyName("adsLevel")] public int AdsLevel { get; set; }
        [JsonPropertyName("mediaLabAdsMigrationJuly2018")] public bool MediaLabAdsMigrationJuly2018 { get; set; }
        [JsonPropertyName("avatarFrameId")] public string AvatarFrameId { get; set; }
        [JsonPropertyName("mediaLabAdsMigrationAugust2020")] public bool MediaLabAdsMigrationAugust2020 { get; set; }
        [JsonPropertyName("adsEnabled")] public bool AdsEnabled { get; set; }
        [JsonPropertyName("deviceInfo")] DeviceInfo DeviceInfo { get; set; }
    }
}
