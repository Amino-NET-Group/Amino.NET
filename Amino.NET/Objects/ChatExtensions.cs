using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class ChatExtensions
    {
        [JsonPropertyName("viewOnly")] public bool ViewOnly { get; set; }
        [JsonPropertyName("lastMembersSummaryUpdatedTime")] public string LastMemberSummaryUpdatedTime { get; set; }
        [JsonPropertyName("channelType")] public string ChannelType { get; set; }
    }
}
