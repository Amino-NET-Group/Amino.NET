using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class DeviceInfo
    {
        [JsonPropertyName("lastClientType")] public int? LastClientType { get; set; }
    }
}
