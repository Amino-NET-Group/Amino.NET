using System.Text.Json.Serialization;


namespace Amino.Objects
{
    public class Comment
    {
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("ndcId")] public int? CommunityId { get; set; }
        [JsonPropertyName("votedValue")] public int? VotedValue { get; set; }
        [JsonPropertyName("parentType")] public int? ParentType { get; set; }
        [JsonPropertyName("commentId")] public string CommentId { get; set; }
        [JsonPropertyName("parentNdcId")] public int? ParentCommunityId { get; set; }
        [JsonPropertyName("votesSum")] public int? VotesSum { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("parentId")] public string ParentId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("subcommentsCount")] public int? SubcommentsCount { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
        [JsonPropertyName("author")] public GenericProfile Author { get; set; }

    }
}
