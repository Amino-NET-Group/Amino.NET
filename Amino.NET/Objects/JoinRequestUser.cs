using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class JoinRequestUser
    {
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("requestId")] public string RequestId { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("ndcId")] public int CommunityId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("applicant")] public JoinRequestApplicant Applicant { get; set; }
    }
}
