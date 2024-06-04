using System.Text.Json.Serialization;

namespace Amino.Objects
{
    //MediaType: 100
    // ROOT JSON ELEMENT: o/chatMessage
    public class ImageMessage
    {
        public SocketBase SocketBase { get; set; } // NEEDS TO BE SET AFTER
        public string Json { get; set; } // NEEDS TO BE SET AFTER
        [JsonPropertyName("mediaValue")] public string MediaUrl { get; set; }
        [JsonPropertyName("threadId")] public string ChatId { get; set; }
        [JsonPropertyName("mediaType")] public int? MediaType { get; set; }
        [JsonPropertyName("clientRefId")] public int? ClientRefId { get; set; }
        [JsonPropertyName("messageId")] public string MessageId { get; set; }
        [JsonPropertyName("uid")] public string ObjectId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
        [JsonPropertyName("isHidden")] public bool? IsHidden { get; set; }
        [JsonPropertyName("includedInSummary")] public bool? IncludedInSummary { get; set; }
        [JsonPropertyName("chatBubbleId")] public string ChatBubbleId { get; set; }
        [JsonPropertyName("chatBubbleVersion")] public int? ChatBubbleVersion { get; set; }
        [JsonPropertyName("alertOption")] public int? AlertOption { get; set; }
        [JsonPropertyName("membershipStatus")] public int? MembershipStatus { get; set; }
        [JsonPropertyName("author")] public GenericProfile Author { get; set; }



    }
}
