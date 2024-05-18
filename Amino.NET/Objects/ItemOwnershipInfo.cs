using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class ItemOwnershipInfo
    {
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("ownershipStatus")] public int OwnershipStatus { get; set; }
        [JsonPropertyName("isAutoRenew")] public bool IsAutoRenew { get; set; }
        [JsonPropertyName("expiredTime")] public string ExpiredTime { get; set; }
    }
}
