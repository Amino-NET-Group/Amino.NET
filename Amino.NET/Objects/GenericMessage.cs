using System.Text.Json.Serialization;

namespace Amino.Objects
{

    public class GenericMessage
    {
        [JsonPropertyName("includedInSummary")] public bool? IncludedInSummary { get; set; }
        [JsonPropertyName("uid")] public string AuthorId { get; set; }
        [JsonPropertyName("isHidden")] public bool? IsHidden { get; set; }
        [JsonPropertyName("messageId")] public string MessageId { get; set; }
        [JsonPropertyName("mediaType")] public int? MediaType { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("chatBubbleId")] public string ChatBubbleId { get; set; }
        [JsonPropertyName("clientRefId")] public int? ClientRefId { get; set; }
        [JsonPropertyName("threadId")] public string ChatId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
        [JsonPropertyName("mediaUrl")] public string MediaUrl { get; set; }
        [JsonPropertyName("author")] public GenericProfile Author { get; set; }

    }
}
