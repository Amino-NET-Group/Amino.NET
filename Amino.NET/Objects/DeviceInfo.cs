using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class DeviceInfo
    {
        [JsonPropertyName("lastClientType")] public int LastClientType { get; set; }
    }
}
