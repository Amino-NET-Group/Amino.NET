using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class UserVisitor
    {
        [JsonPropertyName("visitorsCount")] public int? VisitorsCount { get; set; }
        [JsonPropertyName("capacity")] public int? Capacity { get; set; }
        [JsonPropertyName("visitorPrivacyMode")] public int? VisitorPrivacyMode { get; set; }
        [JsonPropertyName("ownerPrivacyMode")]public int? OwnerPrivacyMode { get; set; }
        [JsonPropertyName("visitTime")]public string VisitTime { get; set; }
        [JsonPropertyName("profile")]public UserProfile Profile { get; private set; }
    }
}
