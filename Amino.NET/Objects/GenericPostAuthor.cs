using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class GenericPostAuthor
    {
        [JsonPropertyName("status")] public int Status { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("isGlobal")] public bool IsGlobal { get; set; }
        [JsonPropertyName("role")] public int Role { get; set; }
        [JsonPropertyName("isStaff")] public bool IsStaff { get; set; }
        [JsonPropertyName("nickname")] public string Nickname { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
    }
}
