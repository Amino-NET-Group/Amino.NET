using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    public class FromInvite
    {
        [JsonPropertyName("isCurrentlyJoined")]public bool IsCurrentUserJoined { get; set; }
        [JsonPropertyName("path")]public string Path { get; set; }
        [JsonPropertyName("invitationId")]public string InvitationId { get; set; }
        [JsonPropertyName("community")]public AdvancedCommunityInfo Community { get; set; }
        [JsonPropertyName("currentUserInfo")]public CurrentUserInfo CurrentUserInfo { get; set; }
        [JsonPropertyName("invitation")]public Invitation Invitation { get; set; }
    }
}
