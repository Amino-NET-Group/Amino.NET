using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;


namespace Amino.Objects
{
    public class Community
    {
        [JsonPropertyName("listedStatus")] public int ListedStatus { get; set; }
        [JsonPropertyName("probationStatus")] public int ProbationStatus { get; set; }
        [JsonPropertyName("membersCount")] public int MemberCount { get; set; }
        [JsonPropertyName("primaryLanguage")] public string PrimaryLanguage { get; set; }
        [JsonPropertyName("communityHeat")] public float CommunityHeat { get; set; }
        [JsonPropertyName("strategyInfo")] public string StrategyInfo { get; set; }
        [JsonPropertyName("tagLine")] public string Tagline { get; set; }
        [JsonPropertyName("joinType")]public int JoinType { get; set; }
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("link")]public string CommunityLink { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("updatedTime")]public string UpdatedTime { get; set; }
        [JsonPropertyName("endpoint")]public string Endpoint { get; set; }
        [JsonPropertyName("name")]public string CommunityName { get; set; }
        [JsonPropertyName("templateId")]public int templateId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("userAddedTopicList")] public string UserAddedTopicList { get; set; }
        [JsonPropertyName("agent")]public GenericProfile Agent { get; set; }
        [JsonPropertyName("themePack")] public CommunityThemePack ThemePack { get; set; }
    }
}
