using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class Topic
    {
        [JsonPropertyName("topicId")] public int TopicId { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("style")] public TopicStyle Style { get; set; }
    }
}
