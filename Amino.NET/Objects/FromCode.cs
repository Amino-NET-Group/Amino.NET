using System.Text.Json.Serialization;

namespace Amino.Objects
{
    // ROOT JSON ELEMENT: linkInfoV2
    public class FromCode
    {
        public string Json { get; set; } // NEEDS TO BE SET AFTER
        [JsonPropertyName("path")]public string Path { get; set; }
        [JsonPropertyName("extensions")]public LinkInfoExtensions Extensions { get; set; }

    }
}
