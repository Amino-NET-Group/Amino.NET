﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class NoticeExtensionOperation
    {
        [JsonPropertyName("text")] public string Text { get; set; }
        [JsonPropertyName("operationType")] public int? OperationType { get; set; }
    }
}
