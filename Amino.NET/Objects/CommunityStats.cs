using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CommunityStats
    {
        [JsonPropertyName("dailyActiveMembers")] public int DailyActiveMembers { get; set; }
        [JsonPropertyName("monthlyActiveMembers")] public int MonthlyActiveMembers { get; set; }
        [JsonPropertyName("totalTimeSpent")] public int TotalTimeSpent { get; set; }
        [JsonPropertyName("totalPostsCreated")] public int TotalPostsCreated { get; set; }
        [JsonPropertyName("newMembersToday")] public int NewMembersToday { get; set; }
        [JsonPropertyName("totalMembers")] public int TotalMembers { get; set; }
    }
}
