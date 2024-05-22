using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class Post
    {
        [JsonPropertyName("globalVotesCount")]public int GlobalVotesCount { get; set; }
        [JsonPropertyName("globalVotedValue")]public int GlobalVotedValue { get; set; }
        [JsonPropertyName("votedValue")]public int VotedValue { get; set; }
        [JsonPropertyName("keywords")]public string Keywords { get; set; }
        [JsonPropertyName("isGlobalAnnouncement")]public bool IsGlobalAnnouncement { get; set; }
        [JsonPropertyName("style")]public int Style { get; set; }
        [JsonPropertyName("totalQuizPlayCount")]public int TotalQuizPlayCount { get; set; }
        [JsonPropertyName("title")]public string Title { get; set; }
        [JsonPropertyName("contentRating")]public int ContentRating { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("needHidden")]public bool NeedHidden { get; set; }
        [JsonPropertyName("guestVotesCount")]public int GuestVotesCount { get; set; }
        [JsonPropertyName("type")]public int Type { get; set; }
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("globalCommentsCount")]public int GlobalCommentsCount { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("totalPollVotesCount")]public int TotalPollVoteCount { get; set; }
        [JsonPropertyName("blogId")]public string BlogId { get; set; }
        [JsonPropertyName("shareURLFullPath")]public string ShareURLFullPath { get; set; }
        [JsonPropertyName("viewCount")]public int ViewCount { get; set; }
        [JsonPropertyName("votesCount")]public int VotesCount { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("endTime")]public string EndTime { get; set; }
        [JsonPropertyName("commentsCount")]public int commentsCount { get; set; }
        [JsonPropertyName("tipInfo")]public TipInfo TipInfo { get; set; }
        [JsonPropertyName("author")]public GenericPostAuthor Author { get; set; }
        [JsonPropertyName("extensions")]public PostExtensions Extensions { get; set; }

    }
}
