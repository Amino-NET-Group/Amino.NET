using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class VoiceMessageExtensions
    {
        [JsonPropertyName("duration")] public float? Duration { get; set; }
    }
}
