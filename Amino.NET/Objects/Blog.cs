using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class Blog
    {
        public string json { get; set; } // NEEDS TO BE ADDED AFTER
        [JsonPropertyName("globalVotesCount")]public int GlobalVotesCount { get; set; } = 0;
        [JsonPropertyName("globalVotedValue")]public int GlobalVotedValue { get; set; } = 0;
        [JsonPropertyName("votedValue")]public int VotedValue { get; set; } = 0;
        [JsonPropertyName("keywords")]public string Keywords { get; set; }
        [JsonPropertyName("strategyInfo")]public string StrategyInfo { get; set; }
        [JsonPropertyName("style")]public int Style { get; set; } = 0;
        [JsonPropertyName("totalQuizPlayCount")]public int TotalQuizPlayCount { get; set; } = 0;
        [JsonPropertyName("title")]public string Title { get; set; }
        [JsonPropertyName("contentRating")]public int ContentRating { get; set; } = 0;
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("needHidden")]public bool NeedHidden { get; set; } = false;
        [JsonPropertyName("guestVotesCount")]public int GuestVotesCount { get; set; } = 0;
        [JsonPropertyName("type")]public int Type { get; set; } = 0;
        [JsonPropertyName("status")]public int Status { get; set; } = 0;
        [JsonPropertyName("globalCommentsCount")]public int GlobalCommentsCount { get; set; } = 0;
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("totalPollVoteCount")]public int TotalPollVoteCount { get; set; } = 0;
        [JsonPropertyName("blogId")]public string BlogId { get; set; }
        [JsonPropertyName("viewCount")]public int ViewCount { get; set; } = 0;
        [JsonPropertyName("votesCount")]public int VotesCount { get; set; } = 0;
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; } = 0;
        [JsonPropertyName("endTime")]public string EndTime { get; set; }
        [JsonPropertyName("commentsCount")]public int commentsCount { get; set; } = 0;
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
    }
}
