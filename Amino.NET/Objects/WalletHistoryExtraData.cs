using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class WalletHistoryExtraData
    {
        [JsonPropertyName("objectDeeplinkUrl")] public string ObjectDeeplinkUrl { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("subtitle")] public string Subtitle { get; set; }
    }
}
