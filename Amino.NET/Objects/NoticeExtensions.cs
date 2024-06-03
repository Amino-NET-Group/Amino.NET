using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class NoticeExtensions
    {
        [JsonPropertyName("operatorUid")] public string OperatorUid { get; set; }
        [JsonPropertyName("style")] public NoticeExtensionStyle Style { get; set; }
        [JsonPropertyName("config")] public NoticeExtensionConfig Config { get; set; }
    }
}
