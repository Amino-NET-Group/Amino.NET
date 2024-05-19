using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CommunityAdvancedSettings
    {
        [JsonPropertyName("defaultRankingTypeInLeaderboard")] public int DefaultRankingTypeInLeaderboard { get; set; }
        [JsonPropertyName("frontPageLayout")] public int FrontPageLayout { get; set; }
        [JsonPropertyName("hasPendingReviewRequest")] public bool HasPendingReviewRequest { get; set; }
        [JsonPropertyName("welcomeMessageEnabled")] public bool WelcomeMessageEnabled { get; set; }
        [JsonPropertyName("pollMinFullBarCount")] public int PollMinFullBarCount { get; set; }
        [JsonPropertyName("catalogEnabled")] public bool CatalogEnabled { get; set; }
        [JsonPropertyName("newsFeedPages")] public List<CommunityNewsFeed> NewsFeed { get; set; }
        [JsonPropertyName("rankingTable")] public List<CommunityRankingTable> Ranks { get; set; }
        [JsonPropertyName("welcomeMessageText")] public string WelcomeMessageText { get; set; }
    }
}
