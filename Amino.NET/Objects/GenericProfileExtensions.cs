using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class GenericProfileExtensions
    {
        [JsonPropertyName("defaultBubbleId")] public string DefaultBubbleId { get; set; }
    }
}
