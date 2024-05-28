using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class BlogCategory
    {
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("style")] public int Style { get; set; }
        [JsonPropertyName("label")] public string Label { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("position")] public int Position { get; set; }
        [JsonPropertyName("type")] public int Type { get; set; }
        [JsonPropertyName("categoryId")] public string CategoryId { get; set; }
        [JsonPropertyName("blogsCount")] public int BlogsCount { get; set; }
    }
}
