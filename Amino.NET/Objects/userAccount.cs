using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserAccount
    {
        public string userName { get; private set; }
        public int? status { get; private set; }
        public string userId { get; private set; }
        public string modifiedTime { get; private set; }
        public string twitterID { get; private set; }
        public int? activation { get; private set; }
        public int? phoneNumberActivation { get; private set; }
        public int? emailActivation { get; private set; }
        public string appleID { get; private set; }
        public string facebookID { get; private set; }
        public string nickName { get; private set; }
        public string googleID { get; private set; }
        public string iconUrl { get; private set; }
        public int? securityLevel { get; private set; }
        public string phoneNumber { get; private set; }
        public int? role { get; private set; }
        public bool aminoIdEditable { get; private set; }
        public string aminoId { get; private set; }
        public string createdTime { get; private set; }
        public string email { get; private set; }
        public string json { get; private set; }
        public _AdvancedSettings AdvancedSettings { get; }
        public _Extensions Extensions { get; }

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
