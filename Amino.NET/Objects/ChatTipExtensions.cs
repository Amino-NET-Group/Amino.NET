using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class ChatTipExtensions
    {
        [JsonPropertyName("tippingCoins")] public int TippedCoins { get; set; }
    }
}
