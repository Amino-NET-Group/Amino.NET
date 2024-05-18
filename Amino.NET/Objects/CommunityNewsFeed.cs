using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CommunityNewsFeed
    {
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("type")] public int Type { get; set; }
    }
}
