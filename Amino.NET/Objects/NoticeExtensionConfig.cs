using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class NoticeExtensionConfig
    {
        [JsonPropertyName("showCommunity")] public bool? ShowCommunity { get; set; }
        [JsonPropertyName("showOperator")] public bool? ShowOperator { get; set; }
        [JsonPropertyName("allowQuickOperation")] public bool? AllowQuickOperation { get; set; }
        [JsonPropertyName("operationList")] public List<NoticeExtensionOperation> OperationList { get; set; }
    }
}
