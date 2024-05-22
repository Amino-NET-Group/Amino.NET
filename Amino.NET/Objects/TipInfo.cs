using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class TipInfo
    {
        [JsonPropertyName("tipMaxCoin")] public int TipMaxCoin { get; set; }
        [JsonPropertyName("tippersCount")] public int TipperCount { get; set; }
        [JsonPropertyName("tippable")] public bool Tippable { get; set; }
        [JsonPropertyName("tipMinCoin")] public int TipMinCoin { get; set; }
        [JsonPropertyName("tippedCoins")] public int TippedCoins { get; set; }
    }
}
