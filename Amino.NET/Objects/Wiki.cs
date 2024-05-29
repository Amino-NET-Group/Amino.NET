using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class Wiki
    {
        [JsonPropertyName("itemId")]public string ItemId { get; set; }
        [JsonPropertyName("status")]public int? Status { get; set; }
        [JsonPropertyName("style")]public int? Style { get; set; }
        [JsonPropertyName("globalCommentsCount")]public int? GlobalCommentsCount { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("votedValue")]public int? VotedValue { get; set; }
        [JsonPropertyName("globalVotesCount")]public int? GlobalVotesCount { get; set; }
        [JsonPropertyName("globalVotedValue")]public int? GlobalVotedValue { get; set; }
        [JsonPropertyName("contentRating")]public int? ContentRating { get; set; }
        [JsonPropertyName("label")]public string Label { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("keywords")]public string Keywords { get; set; }
        [JsonPropertyName("needHidden")]public bool? NeedHidden { get; set; }
        [JsonPropertyName("guestVotesCount")]public int? GuestVotesCount { get; set; }
        [JsonPropertyName("votesCount")]public int? VotesCount { get; set; }
        [JsonPropertyName("ndcId")]public int? CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("commentsCount")]public int? CommentsCount { get; set; }
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
    }
}
