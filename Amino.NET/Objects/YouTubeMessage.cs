﻿using System.Text.Json.Serialization;

namespace Amino.Objects
{
    // ROOT JSON ELEMENT: o/chatMessage
    public class YouTubeMessage
    {
        public SocketBase SocketBase { get; set; }
        public string Json { get; set; }

        [JsonPropertyName("mediaValue")]public string MediaValue { get; set; }
        [JsonPropertyName("threadId")]public string ChatId { get; set; }
        [JsonPropertyName("mediaType")]public int? MediaType { get; set; }
        [JsonPropertyName("content")]public string VideoTitle { get; }
        [JsonPropertyName("clientRefId")]public int? ClientRefId { get; set; }
        [JsonPropertyName("messageId")]public string MessageId { get; set; }
        [JsonPropertyName("uid")]public string ObjectId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("type")]public int? Type { get; set; }
        [JsonPropertyName("isHidden")]public bool? IsHidden { get; set; }
        [JsonPropertyName("includedInSummary")]public bool? IncludedInSummary { get; set; }
        [JsonPropertyName("chatBubbleId")]public string ChatBubbleId { get; set; }
        [JsonPropertyName("chatBubbleVersion")]public int? ChatBubbleVersion { get; set; }
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
    }
}
