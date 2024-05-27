using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class Invitation
    {
        [JsonPropertyName("link")] public string Link { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("invitationId")] public string InvitationId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("duration")] public int Duration { get; set; }
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("ndcId")] public int CommunityId { get; set; }
        [JsonPropertyName("inviteCode")] public string InviteCode { get; set; }
        [JsonPropertyName("author")] public GenericProfile Author { get; set; }
    }
}
