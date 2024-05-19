using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class LinkInfoExtensions
    {
        [JsonPropertyName("community")] Community Community { get; set; }
        [JsonPropertyName("linkInfo")] LinkInfo LinkInfo { get; set; }
    }
}
