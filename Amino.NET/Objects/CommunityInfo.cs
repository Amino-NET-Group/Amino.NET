using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class CommunityInfo
    {
        [JsonPropertyName("objectType")]public int ObjectType { get; set; }
        [JsonPropertyName("aminoId")]public string AminoId { get; set; }
        [JsonPropertyName("objectId")]public string ObjectId { get; set; }
        [JsonPropertyName("ndcId")]public int CommunityId { get; set; }
        [JsonPropertyName("refObject")]public Community CommunityBase { get; set; }
    }
}
