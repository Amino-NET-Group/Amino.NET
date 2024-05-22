using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{

    // ROOT JSON ELEMENT: o/chatMessage
    public class ChatEvent
    {
        public string Json { get; set; } // NEEDS TO BE SET AFTER
        public SocketBase SocketBase { get; set; } // NEEDS TO BE SET AFTER

        [JsonPropertyName("threadId")]public string ChatId { get; set; }
        [JsonPropertyName("mediaType")]public int MediaType { get; set; }
        [JsonPropertyName("clienttRefId")]public int ClientRefId { get; set; }
        [JsonPropertyName("messageId")]public string MessageId { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("type")]public int Type { get; set; }
        [JsonPropertyName("isHidden")]public bool IsHidden { get; set; }
        [JsonPropertyName("includedInSummary")]public bool IncludedInSummary { get; set; }
        
    }
}
