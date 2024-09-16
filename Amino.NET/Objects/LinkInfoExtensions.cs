using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class LinkInfoExtensions
    {
        [JsonPropertyName("community")] public Community Community { get; set; }
        [JsonPropertyName("linkInfo")] public LinkInfo LinkInfo { get; set; }
    }
}
