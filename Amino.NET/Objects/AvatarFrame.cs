using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AvatarFrame
    {
        public bool isGloballyAvailable { get; }
        public bool isNew { get; }
        public int version { get; }
        public string createdTime { get; }
        public int frameType { get; }
        public string modifiedTime { get; }
        public string frameUrl { get; }
        public string md5 { get; }
        public string iconUrl { get; }
        public string frameId { get; }
        public string objectId { get; }
        public int ownershipStatus { get; }
        public string name { get; }
        public string resourceUrl { get; }
        public int status { get; }
        public _AdditionalBenefits AdditionalBenefits { get; }
        public _RestrictionInfo RestrictionInfo { get; }
        public _OwnershipInfo OwnershipInfo { get; }
        public _Config Config { get; }


        public AvatarFrame(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            isGloballyAvailable = (bool)jsonObj["isGloballyAvailable"];
            isNew = (bool)jsonObj["isNew"];
            version = (int)jsonObj["version"];
            createdTime = (string)jsonObj["createdTime"];
            frameType = (int)jsonObj["frameType"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            frameUrl = (string)jsonObj["frameUrl"];
            md5 = (string)jsonObj["md5"];
            iconUrl = (string)jsonObj["icon"];
            frameId = (string)jsonObj["frameId"];
            objectId = (string)jsonObj["uid"];
            ownershipStatus = (int)jsonObj["ownershipStatus"];
            name = (string)jsonObj["name"];
            resourceUrl = (string)jsonObj["resourceUrl"];
            status = (int)jsonObj["status"];
            if(jsonObj["additionalBenefits"] != null) { AdditionalBenefits = new _AdditionalBenefits(_json); }
            if(jsonObj["restrictionInfo"] != null) { RestrictionInfo = new _RestrictionInfo(_json); }
            if(jsonObj["ownershipInfo"] != null) { OwnershipInfo = new _OwnershipInfo(_json); }
            if(jsonObj["config"] != null) { Config = new _Config(_json); }

        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _AdditionalBenefits
        {
            public bool firstMonthFreeAminoPlus { get; }

            public _AdditionalBenefits(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                firstMonthFreeAminoPlus = (bool)jsonObj["additionalBenefits"]["firstMonthFreeAminoPlusMembership"];
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _RestrictionInfo
        {
            public int restrictValue { get; }
            public string availableDuration { get; }
            public int discountValue { get; }
            public int discountStatus { get; }
            public string ownerUserId { get; }
            public int ownerType { get; }
            public int restrictType { get; }

            public _RestrictionInfo(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                restrictValue = (int)jsonObj["restrictionInfo"]["restrictValue"];
                availableDuration = (string)jsonObj["restrictionInfo"]["availableDuration"];
                if(jsonObj["restrictionInfo"]["discountValue"] != null) { discountValue = (int)jsonObj["restrictionInfo"]["discountValue"]; }
                if(jsonObj["restrictionInfo"]["discountStatus"] != null) { discountStatus = (int)jsonObj["restrictionInfo"]["discountStatus"]; }
                ownerType = (int)jsonObj["restrictionInfo"]["ownerType"];
                restrictType = (int)jsonObj["restrictionInfo"]["restrictType"];
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _OwnershipInfo
        {
            public string createdTime { get; }
            public int ownershipStatus { get; }
            public bool isAutoRenew { get; }
            public string expiredTime { get; }

            public _OwnershipInfo(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                createdTime = (string)jsonObj["ownershipInfo"]["createdTime"];
                ownershipStatus = (int)jsonObj["ownershipInfo"]["ownershipStatus"];
                isAutoRenew = (bool)jsonObj["ownershipInfo"]["isAutoRenew"];
                expiredTime = (string)jsonObj["ownershipInfo"]["expiredTime"];
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Config
        {
            public string avatarFramePath { get; }
            public string objectId { get; }
            public string moodColor { get; }
            public string name { get; }
            public int version { get; }
            public string userIconBorderColor { get; }

            public _Config(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                avatarFramePath = (string)jsonObj["config"]["avatarFramePath"];
                objectId = (string)jsonObj["config"]["id"];
                moodColor = (string)jsonObj["config"]["moodColor"];
                name = (string)jsonObj["config"]["name"];
                version = (int)jsonObj["config"]["version"];
                userIconBorderColor = (string)jsonObj["config"]["userIconBorderColor"];
            }

        }
    }
}
