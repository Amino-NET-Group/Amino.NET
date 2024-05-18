using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CommunityRankingTable
    {
        [JsonPropertyName("level")] public int Level { get; set; }
        [JsonPropertyName("reputation")] public int Reputation { get; set; }
        [JsonPropertyName("id")] public string ID { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
    }
}
