using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class MembershipInfo
    {
        [JsonPropertyName("accountMembershipEnabled")] public bool AccountMembershipEnabled { get; set; }
        [JsonPropertyName("hasAnyAppleSubscription")] public bool HasAnyAppleSubscription { get; set; }
        [JsonPropertyName("hasAnyAndroidSubscription")]public bool HasAnyAndroidSubscription { get; set; }
        [JsonPropertyName("membership")]public Membership Membership { get; set; }

    }
}
