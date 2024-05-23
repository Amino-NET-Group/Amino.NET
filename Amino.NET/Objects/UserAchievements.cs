using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class UserAchievements
    {
        [JsonPropertyName("numberOfMembersCount")]public int NumberOfMembersCount { get; set; }
        [JsonPropertyName("numberOfPostsCreated")]public int NumberOfPostsCreated { get; set; }
    }
}
