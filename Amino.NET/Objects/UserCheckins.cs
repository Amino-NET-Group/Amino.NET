using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class UserCheckins
    {

        [JsonPropertyName("hasAnyCheckIn")]public bool? HasAnyCheckIn { get; set; }
        [JsonPropertyName("consecutiveCheckInDays")]public int? ConsecutiveCheckInDays { get; set; }
        [JsonPropertyName("brokenStreaks")]public int? BrokenStreaks { get; set; }

    }
}
