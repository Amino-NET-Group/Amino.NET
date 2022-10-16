using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class eventLog
    {
        public string? globalStrategyInfo { get; private set; }
        public string? userId { get; private set; }
        public string? contentLanguage { get; private set; }
        public int? signUpStrategy { get; private set; }
        public int? landingOption { get; private set; }
        public bool? needsBirthDateUpdate { get; private set; }
        public int? interestPickerStyle { get; private set; }
        public bool? showStoreBadge { get; private set; }
        public string? amino_userId { get; private set; }
        public bool? needTriggerInterestPicker { get; private set; }
        public string? json { get; private set; }
        public _participatedExperiments participatedExperiments { get; private set; }

        public eventLog(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            globalStrategyInfo = (string)jsonObj["globalStrategyInfo"];
            userId = (string)jsonObj["uid"];
            contentLanguage = (string)jsonObj["contentLanguage"];
            signUpStrategy = (int)jsonObj["signUpStrategy"];
            landingOption = (int)jsonObj["landingOption"];
            needsBirthDateUpdate = (bool)jsonObj["needsBirthDateUpdate"];
            interestPickerStyle = (int)jsonObj["interestPickerStyle"];
            showStoreBadge = (bool)jsonObj["showStoreBadge"];
            amino_userId = (string)jsonObj["auid"];
            needTriggerInterestPicker = (bool)jsonObj["needTriggerInterestPicker"];
            json = _json.ToString();

            participatedExperiments = new _participatedExperiments(_json);
        }


        public class _participatedExperiments
        {
            public int? chatMembersCommonChannel { get; private set; }
            public int? couponPush { get; private set; }
            public int? communityMembersCommonChannel { get; private set; }
            public int? communityTabExp { get; private set; }
            public int? userVectorCommunitySimilarityChannel { get; private set; }


            public _participatedExperiments(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                chatMembersCommonChannel = (int)jsonObj["participatedExperiments"]["chatMembersCommonChannel"];
                couponPush = (int)jsonObj["participatedExperiments"]["couponPush"];
                communityMembersCommonChannel = (int)jsonObj["participatedExperiments"]["communityMembersCommonChannel"];
                communityTabExp = (int)jsonObj["participatedExperiments"]["communityTabExp"];
                userVectorCommunitySimilarityChannel = (int)jsonObj["participatedExperiments"]["userVectorCommunitySimilarityChannel"];
            }

        }
    }
}
