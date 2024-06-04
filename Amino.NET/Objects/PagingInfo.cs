using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class PagingInfo
    {
        [JsonPropertyName("nextPageToken")] public string NextPageToken { get; set; }
        [JsonPropertyName("prevPageToken")] public string PrevPageToken { get; set; }
    }
}
