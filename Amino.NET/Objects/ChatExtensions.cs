﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class ChatExtensions
    {
        [JsonPropertyName("viewOnly")] public bool ViewOnly { get; set; }
        [JsonPropertyName("lastMembersSummaryUpdatedTime")] public string LastMemberSummaryUpdatedTime { get; set; }
        [JsonPropertyName("channelType")] public string ChannelType { get; set; }
    }
}
