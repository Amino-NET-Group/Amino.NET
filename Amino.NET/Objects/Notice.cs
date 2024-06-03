using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class Notice
    {
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        //[JsonPropertyName("community")] No Data
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("ndcId")] public int? CommunityId { get; set; }
        [JsonPropertyName("noticeId")] public string NoticeId { get; set; }
        [JsonPropertyName("notificationId")] public string NotificationId { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("type")] public int? Type { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        //[JsonPropertyName("content")] No Data

        [JsonPropertyName("operator")] public GenericProfile Operator { get; set; }
        [JsonPropertyName("targetUser")] public GenericProfile TargetUser { get; set; }
        [JsonPropertyName("extensions")] public NoticeExtensions Extensions { get; set; }
    }
}
