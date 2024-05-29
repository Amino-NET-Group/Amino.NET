using System.Text.Json.Serialization;

namespace Amino.Objects
{
    // ROOT JSON ELEMENT: account
    public class UserAccount
    {
        [JsonPropertyName("username")] public string Username { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("uid")] public string UserId { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("twitterID")] public string TwitterId { get; set; }
        [JsonPropertyName("activation")] public int? Activation { get; set; }
        [JsonPropertyName("phoneNumberActivation")] public int? PhoneNumberActivation { get; set; }
        [JsonPropertyName("emailActivation")] public int? EmailActivation { get; set; }
        [JsonPropertyName("appleID")] public string AppleId { get; set; }
        [JsonPropertyName("facebookID")] public string FacebookId { get; set; }
        [JsonPropertyName("nickname")] public string Nickname { get; set; }
        [JsonPropertyName("googleID")] public string GoogleId { get; set; }
        [JsonPropertyName("icon")] public string IconUrl { get; set; }
        [JsonPropertyName("securityLevel")] public int? SecurityLevel { get; set; }
        [JsonPropertyName("phoneNumber")] public string PhoneNumber { get; set; }
        [JsonPropertyName("role")] public int? Role { get; set; }
        [JsonPropertyName("aminoIdEditable")] public bool? AminoIdEditable { get; set; }
        [JsonPropertyName("aminoId")] public string AminoId { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonPropertyName("advancedSettings")] public UserAccountAdvancedSettings AdvancedSettings { get; set; }
        [JsonPropertyName("extensions")] public UserAccountExtensions Extensions { get; set; }

    }
}
