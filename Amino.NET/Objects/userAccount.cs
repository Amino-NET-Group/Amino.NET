using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class userAccount
    {
        public string userName { get; private set; }
        public int status { get; private set; }
        public string userId { get; private set; }
        public string modifiedTime { get; private set; }
        public string twitterID { get; private set; }
        public int activation { get; private set; }
        public int phoneNumberActivation { get; private set; }
        public int emailActivation { get; private set; }
        public string appleID { get; private set; }
        public string facebookID { get; private set; }
        public string googleID { get; private set; }
        public string iconUrl { get; private set; }
        public int securityLevel { get; private set; }
        public string phoneNumber { get; private set; }
        public string membership { get; private set; }
        public int role { get; private set; }
        public bool aminoIdEditable { get; private set; }
        public string aminoId { get; private set; }
        public string createdTime { get; private set; }
        public string email { get; private set; }
        public string json { get; private set; }
        public _advancedSettings advancedSettings { get; }
        public _extensions extensions { get; }

        public userAccount(JObject _json)
        {
            advancedSettings = new _advancedSettings(_json);
            extensions = new _extensions(_json);
            json = _json.ToString();
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _advancedSettings
        {
            public int analyticsEnabled { get; private set; }

            public _advancedSettings(JObject _json)
            {

            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _extensions
        {
            public string contentLanguage { get; private set; }
            public int adsFlags { get; private set; }
            public int adsLevel { get; private set; }
            public bool mediaLabAdsMigrationJuly2020 { get; private set; }
            public string avatarFrameId { get; private set; }
            public bool mediaLabAdsMigrationAugust202 { get; private set; }
            public bool adsEnabled { get; private set; }
            public _deviceInfo deviceInfo { get; }

            public _extensions(JObject _json)
            {
                deviceInfo = new _deviceInfo(_json);
            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _deviceInfo
            {
                public int lastClientType { get; private set; }

                public _deviceInfo(JObject _json)
                {

                } 
            }
        }
    }
}
