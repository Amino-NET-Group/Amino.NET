using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class UserAchievements
    {
        [JsonPropertyName("numberOfMembersCount")]public int NumberOfMembersCount { get; set; }
        [JsonPropertyName("numberOfPostsCreated")]public int NumberOfPostsCreated { get; set; }
    }
}
