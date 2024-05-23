using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class LinkInfoExtensions
    {
        [JsonPropertyName("community")] Community Community { get; set; }
        [JsonPropertyName("linkInfo")] LinkInfo LinkInfo { get; set; }
    }
}
