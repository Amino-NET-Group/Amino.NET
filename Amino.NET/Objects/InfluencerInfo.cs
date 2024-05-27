using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class InfluencerInfo
    {

        [JsonPropertyName("myFanClub")]public InfluencerFanClubMember MyFanClub { get; set; }
        [JsonPropertyName("influencerUserProfile")]public UserProfile InfluencerUserProfile { get; set; }
        [JsonPropertyName("fanClubList")]public List<InfluencerFanClubMember> FanClubMembers { get; set; }
    }
}
