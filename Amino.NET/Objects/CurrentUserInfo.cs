using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class CurrentUserInfo
    {
        [JsonPropertyName("notificationsCount")] public int NotificationsCount { get; set; }
        [JsonPropertyName("userProfile")] public UserProfile UserProfile { get; set; }
    }
}
