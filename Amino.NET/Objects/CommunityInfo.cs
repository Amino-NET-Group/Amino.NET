using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CommunityInfo
    {
        [JsonPropertyName("objectType")]public int ObjectType { get; set; }
        [JsonPropertyName("aminoId")]public string AminoId { get; set; }
        [JsonPropertyName("objectId")]public string ObjectId { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("refObject")]public Community CommunityBase { get; set; }
    }
}
