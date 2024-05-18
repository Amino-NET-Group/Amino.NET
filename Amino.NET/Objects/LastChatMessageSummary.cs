using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class LastChatMessageSummary
    {
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("isHidden")] public bool IsHidden { get; set; }
        [JsonPropertyName("mediaType")] public string MediaType { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("messageId")] public string MessageId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("type")] public int Type { get; set; }
        [JsonPropertyName("mediaValue")] public string MediaValue { get; set; }
    }
}
