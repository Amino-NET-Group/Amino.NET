using System.Text.Json.Serialization;

namespace Amino.Objects
{
    // ROOT JSON ELEMENT: o/chatMessage
    public class DeletedMessage
    {
        public SocketBase SocketBase { get; set; } // NEEDS TO BE ADDED AFTER
        public string Json { get; set; } // NEEDS TO BE ADDED AFTER
        [JsonPropertyName("threadId")]public string ChatId { get; set; }
        [JsonPropertyName("mediaType")]public int MediaType { get; set; }
        [JsonPropertyName("clientRefId")]public int ClientRefId { get; set; }
        [JsonPropertyName("messageId")]public string MessageId { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("type")]public int Type { get; set; }
        [JsonPropertyName("isHidden")]public bool IsHidden { get; set; }
        [JsonPropertyName("includedInSummary")]public bool IncludedInSummary { get; set; }
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
    }
}
