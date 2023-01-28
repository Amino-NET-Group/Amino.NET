using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Community
    {
        public int? listedStatus { get; private set; }
        public int? probationStatus { get; private set; }
        public int? membersCount { get; private set; }
        public string primaryLanguage { get; private set; }
        public float? communityHeat { get; private set; }
        public string strategyInfo { get; private set; }
        public string tagline { get; private set; }
        public int? joinType { get; private set; }
        public int? status { get; private set; }
        public string modifiedTime { get; private set; }
        public int? communityId { get; private set; }
        public string communityLink { get; private set; }
        public string iconUrl { get; private set; }
        public string updatedTime { get; private set; }
        public string endpoint { get; private set; }
        public string communityName { get; private set; }
        public int? templateId { get; private set; }
        public string createdTime { get; private set; }
        public string json { get; private set; }
        public _Agent Agent { get; private set; }


        public Community(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            try { listedStatus = (int)jsonObj["listedStatus"]; } catch { }
            try { probationStatus = (int)jsonObj["probationStatus"]; }catch{ }
            try { membersCount = (int)jsonObj["membersCount"]; } catch { }
            try { joinType = (int)jsonObj["joinType"]; } catch { }
            try { status = (int)jsonObj["status"]; } catch { }
            try { communityId = (int)jsonObj["ndcId"]; } catch { }
            try { communityHeat = (float)jsonObj["communityHeat"]; } catch { }
            try { primaryLanguage = (string)jsonObj["primaryLanguage"]; } catch { }
            try { strategyInfo = (string)jsonObj["strategyInfo"]; } catch { }
            try { tagline = (string)jsonObj["tagline"]; } catch { }
            try { modifiedTime = (string)jsonObj["modifiedTime"]; } catch { }
            try { communityLink = (string)jsonObj["link"]; } catch { }
            try { iconUrl = (string)jsonObj["icon"]; } catch { }
            try { updatedTime = (string)jsonObj["updatedTime"]; } catch { }
            try { endpoint = (string)jsonObj["endpoint"]; } catch { }
            try { communityName = (string)jsonObj["name"]; } catch { }
            try { createdTime = (string)jsonObj["createdTime"]; } catch { }
            json = _json.ToString();
            Agent = new _Agent(_json);
        }

        public class _Agent
        {
            public bool isNickNameVerified { get; private set; }
            public string userId { get; private set; }
            public int? level { get; private set; }
            public int? followingStatus { get; private set; }
            public int? membershipStatus { get; private set; }
            public bool isGlobal { get; private set; }
            public int? reputation { get; private set; }
            public int? membersCount { get; private set; }

            public _Agent(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                try { isNickNameVerified = (bool)jsonObj["agent"]["isNicknameVerified"]; } catch { }
                try { userId = (string)jsonObj["agent"]["uid"]; } catch { }
                try { level = (int)jsonObj["agent"]["level"]; } catch { }
                try { followingStatus = (int)jsonObj["agent"]["followingStatus"]; } catch { }
                try { membershipStatus = (int)jsonObj["agent"]["membershipStatus"]; } catch { }
                try { isGlobal = (bool)jsonObj["agent"]["isGlobal"]; } catch { }
                try { reputation = (int)jsonObj["agent"]["reputation"]; } catch { }
                try { membersCount = (int)jsonObj["agent"]["membersCount"]; } catch { } 
            }
        }
    }
}
