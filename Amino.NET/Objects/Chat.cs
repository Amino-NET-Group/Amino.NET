using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class Chat
    {
        public string Json { get; set; } // NEEDS TO BE ADDED LATER
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("membersQuota")]public int MembersQuota { get; set; }
        [JsonPropertyName("threadId")]public string ThreadId { get; set; }
        [JsonPropertyName("keywords")]public string Keywords { get; set; }
        [JsonPropertyName("membersCount")]public int MemberCount { get; set; }
        [JsonPropertyName("strategyInfo")]public string StrategyInfo { get; set; }
        [JsonPropertyName("isPinned")]public bool IsPinned { get; set; }
        [JsonPropertyName("title")]public string Title { get; set; }
        [JsonPropertyName("membershipStatus")]public int MembershipStatus { get; set; }
        [JsonPropertyName("content")]public string Content { get; set; }
        [JsonPropertyName("needHidden")]public bool NeedHidden { get; set; }
        [JsonPropertyName("alrtOption")]public int alertOption { get; set; }
        [JsonPropertyName("lastReadTime")]public string LastReadTime { get; set; }
        [JsonPropertyName("type")]public int Type { get; set; }
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("mentionMe")]public bool MentionMe { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("condition")]public int Condition { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("latestActivityTime")]public string LatestActivityTime { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("author")]public GenericProfile Author { get; set; }
        [JsonPropertyName("membersSummary")] public List<GenericChatMember> ChatMemberSummary { get; set; }
        [JsonPropertyName("lastMessageSummary")] public LastChatMessageSummary LastMessageSummary { get; set; }
        [JsonPropertyName("extensions")] public ChatExtensions Extensions { get; set; }
    }
}
