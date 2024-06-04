using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class PostExtensions
    {
        [JsonPropertyName("commentEnabled")] public bool? CommentsEnabled { get; set; }
        [JsonPropertyName("author")] public string Author { get; set; }
    }
}
