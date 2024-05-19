using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;


namespace Amino.Objects
{
    public class EventLog
    {
        
        [JsonPropertyName("globalStrategyInfo")]public string GlobalStrategyInfo { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("contentLanguage")]public string ContentLanguage { get; set; }
        [JsonPropertyName("signUpStrategy")]public int SignUpStrategy { get; set; }
        [JsonPropertyName("landingOption")]public int LandingOption { get; set; }
        [JsonPropertyName("needsBirthDateUpdate")]public bool NeedsBirthDateUpdate { get; set; }
        [JsonPropertyName("interestPickerStyle")]public int InterestPickerStyle { get; set; }
        [JsonPropertyName("showStoreBadge")]public bool ShowStoreBadge { get; set; }
        [JsonPropertyName("auid")]public string AUID { get; set; }
        [JsonPropertyName("needTriggerInterestPicker")]public bool NeedTriggerInterestPicker { get; set; }
        [JsonPropertyName("participatedExperiments")]public ParticipatedExperiments ParticipatedExperiments { get; set; }
        
    }
}
