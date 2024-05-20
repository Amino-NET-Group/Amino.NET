using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
namespace Amino.Objects
{
    public class InviteCode
    {
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("duration")]public int Duration { get; set; }
        [JsonPropertyName("invitationId")]public string InvitationId { get; set; }
        [JsonPropertyName("link")]public string InviteUrl { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
    }
}
