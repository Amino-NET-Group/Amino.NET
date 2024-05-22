﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class GenericMessageAuthor
    {
        [JsonPropertyName("nickname")] public string Nickname { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("level")] public int Level { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("reputation")] public int Reputation { get; set; }
    }
}
