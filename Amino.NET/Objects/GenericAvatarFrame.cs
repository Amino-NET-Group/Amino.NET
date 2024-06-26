﻿using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class GenericAvatarFrame
    {
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("version")] public int? Version { get; set; }
        [JsonPropertyName("resourceUrl")] public string ResourceUrl { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("frameType")] public int? FrameType { get; set; }
        [JsonPropertyName("frameId")] public string FrameId { get; set; }
    }
}
