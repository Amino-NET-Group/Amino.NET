﻿using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class LastChatMessageSummary
    {
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("isHidden")] public bool? IsHidden { get; set; }
        [JsonPropertyName("mediaType")] public int? MediaType { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("messageId")] public string MessageId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
        [JsonPropertyName("mediaValue")] public string MediaValue { get; set; }

    }
}
