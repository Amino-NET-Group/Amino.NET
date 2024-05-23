using System.Text.Json.Serialization;


namespace Amino.Objects
{
    //MediaType: 0
    // ROOT JSON ELEMENT: o/chatMessage
    public class Message
    {
        public SocketBase SocketBase { get; set; } // NEEDS TO BE SET AFTER
        public string Json { get; set; } // NEEDS TO BE SET AFTER
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("messageId")]public string MessageId { get; set; }
        [JsonPropertyName("threadId")]public string ChatId { get; set; }
        [JsonPropertyName("uid")]public string ObjectId { get; set; }
        [JsonPropertyName("author")]public GenericMessageAuthor Author { get; set; }
    }

}
