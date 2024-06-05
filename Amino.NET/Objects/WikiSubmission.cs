using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class WikiSubmission
    {
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("itemId")] public string WikiId { get; set; }
        [JsonPropertyName("originalItemId")] public string OriginalWikiId { get; set; }
        [JsonPropertyName("requestId")] public string RequestId { get; set; }
        [JsonPropertyName("destinationItemId")] public string DestinationItemId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("responseMessage")] public string ResponseMessage { get; set; }

        [JsonPropertyName("operator")] public GenericProfile Operator { get; set; }
        [JsonPropertyName("item")] public Wiki Wiki { get; set; }

    }
}
