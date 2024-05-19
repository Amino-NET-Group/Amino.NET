using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CurrentUserInfo
    {
        [JsonPropertyName("notificationsCount")] public int NotificationsCount { get; set; }
        [JsonPropertyName("userProfile")] public UserProfile UserProfile { get; set; }
    }
}
