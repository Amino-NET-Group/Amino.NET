using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class JoinRequest
    {
        [JsonPropertyName("communityMembershipRequestCount")] public int CommunityMembershipRequestCount { get; set; }
        [JsonPropertyName("communityMembershipRequestList")] public List<JoinRequestUser> CommunityMembershipRequestList { get; set; }
    }
}
