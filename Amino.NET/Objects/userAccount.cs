using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    // ROOT JSON ELEMENT: account
    public class UserAccount
    {
        [JsonPropertyName("username")]public string Username { get; set; }
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("modifiedTime")]public string ModifiedTime { get; set; }
        [JsonPropertyName("twitterID")]public string TwitterId { get; set; }
        [JsonPropertyName("activation")]public int Activation { get; set; }
        [JsonPropertyName("phoneNumberActivation")]public int PhoneNumberActivation { get; set; }
        [JsonPropertyName("emailActivation")]public int EmailActivation { get; set; }
        [JsonPropertyName("appleID")]public string AppleId { get; set; }
        [JsonPropertyName("facebookID")]public string FacebookId { get; set; }
        [JsonPropertyName("nickname")]public string Nickname { get; set; }
        [JsonPropertyName("googleID")]public string GoogleId { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }
        [JsonPropertyName("securityLevel")]public int SecurityLevel { get; set; }
        [JsonPropertyName("phoneNumber")]public string PhoneNumber { get; set; }
        [JsonPropertyName("role")]public int Role { get; set; }
        [JsonPropertyName("aminoIdEditable")]public bool AminoIdEditable { get; set; }
        [JsonPropertyName("aminoId")]public string AminoId { get; set; }
        [JsonPropertyName("createdTime")]public string CreatedTime { get; set; }
        [JsonPropertyName("email")]public string Email { get; set; }
        [JsonPropertyName("advancedSettings")]public UserAccountAdvancedSettings AdvancedSettings { get; set; }
        [JsonPropertyName("extensions")]public UserAccountExtensions Extensions { get; set; }

        public UserAccount(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            AdvancedSettings = new _AdvancedSettings(_json);
            Extensions = new _Extensions(_json);
            json = _json.ToString();
            userName = (string)jsonObj["account"]["username"];
            status = (int)jsonObj["account"]["status"];
            userId = (string)jsonObj["account"]["uid"];
            modifiedTime = (string)jsonObj["account"]["modifiedTime"];
            twitterID = (string)jsonObj["account"]["twitterID"];
            activation = (int)jsonObj["account"]["activation"];
            phoneNumberActivation = (int)jsonObj["account"]["phoneNumberActivation"];
            emailActivation = (int)jsonObj["account"]["emailActivation"];
            appleID = (string)jsonObj["account"]["appleID"];
            facebookID = (string)jsonObj["account"]["facebookID"];
            nickName = (string)jsonObj["account"]["nickname"];
            googleID = (string)jsonObj["account"]["googleID"];
            iconUrl = (string)jsonObj["account"]["icon"];
            securityLevel = (int)jsonObj["account"]["securityLevel"];
            phoneNumber = (string)jsonObj["account"]["phoneNumber"];
            role = (int)jsonObj["account"]["role"];
            aminoIdEditable = (bool)jsonObj["account"]["aminoIdEditable"];
            aminoId = (string)jsonObj["account"]["aminoId"];
            createdTime = (string)jsonObj["account"]["createdTime"];
            email = (string)jsonObj["account"]["email"];
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _AdvancedSettings
        {
            public int? analyticsEnabled { get; private set; }

            public _AdvancedSettings(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                analyticsEnabled = (int)jsonObj["account"]["advancedSettings"]["analyticsEnabled"];

            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Extensions
        {
            public string contentLanguage { get; private set; }
            public int? adsFlags { get; private set; }
            public int? adsLevel { get; private set; }
            public bool mediaLabAdsMigrationJuly2020 { get; private set; }
            public string avatarFrameId { get; private set; }
            public bool mediaLabAdsMigrationAugust2020 { get; private set; }
            public bool adsEnabled { get; private set; }
            public _DeviceInfo DeviceInfo { get; }

            public _Extensions(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                DeviceInfo = new _DeviceInfo(_json);
                contentLanguage = (string)jsonObj["account"]["extensions"]["contentLanguage"];
                adsFlags = (int)jsonObj["account"]["extensions"]["adsFlags"];
                mediaLabAdsMigrationJuly2020 = (bool)jsonObj["account"]["extensions"]["mediaLabAdsMigrationJuly2020"];
                avatarFrameId = (string)jsonObj["account"]["extensions"]["avatarFrameId"];
                mediaLabAdsMigrationAugust2020 = (bool)jsonObj["account"]["extensions"]["mediaLabAdsMigrationAugust2020"];
                adsEnabled = (bool)jsonObj["account"]["extensions"]["adsEnabled"];

            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _DeviceInfo
            {
                public int? lastClientType { get; private set; }

                public _DeviceInfo(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    lastClientType = (int)jsonObj["account"]["extensions"]["deviceInfo"]["lastClientType"];

                } 
            }
        }
    }
}
