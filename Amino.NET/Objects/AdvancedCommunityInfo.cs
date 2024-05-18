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
    public class AdvancedCommunityInfo
    {
        // Base JSON key: community

        public bool IsCurrentUserJoined { get; set; } // MUST BE SET AFTER
        public string json { get; set; } // MUST BE SET AFTER
        [JsonPropertyName("searchable")] public bool Searchable { get; set; }
        [JsonPropertyName("isStandaloneAppDepricated")] public bool IsStandaloneAppDeprecated { get; set; }
        [JsonPropertyName("listedStatus")] public int ListedStatus { get; set; }
        [JsonPropertyName("probationStatus")] public int ProbationStatus { get; set; }
        [JsonPropertyName("keywords")] public string Keywords { get; set; }
        [JsonPropertyName("membersCount")] public int MemberCount { get; set; }
        [JsonPropertyName("primaryLanguage")] public string PrimaryLanguage { get; set; }
        [JsonPropertyName("communityHeat")] public float CommunityHeat { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("isStandaloneAppMonetizationEnabled")] public bool IsStandaloneAppMonetizationEnabled { get; set; }
        [JsonPropertyName("tagline")] public string Tagline { get; set; }
        [JsonPropertyName("joinType")] public int JoinType { get; set; }
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("ndcId")] public int CommunityId { get; set; }
        [JsonPropertyName("link")] public string Link { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("updatedTime")] public string UpdatedTime { get; set; }
        [JsonPropertyName("endpoint")] public string Endpoint { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("templateId")] public int TemplateId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }

        [JsonPropertyName("communityHeadList")] public List<GenericProfile> CommunityHeadList { get; set; }
        [JsonPropertyName("agent")] public GenericProfile Agent { get; set; }
        [JsonPropertyName("themePack")] public CommunityThemePack ThemePack { get; set; }
        [JsonPropertyName("advancedSettings")] public CommunityAdvancedSettings AdvancedSettings { get; }
    }
}