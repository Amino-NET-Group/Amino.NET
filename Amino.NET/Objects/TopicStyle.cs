using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class TopicStyle
    {
        [JsonPropertyName("backgroundColor")] public string BackgroundColor { get; set; }
    }
}
