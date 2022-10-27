using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Community
    {
        public int listedStatus { get; private set; }
        public int probationStatus { get; private set; }
        public int membersCount { get; private set; }
        public string primaryLanguage { get; private set; }
        public float communityHeat { get; private set; }
        public string strategyInfo { get; private set; }
        public string tagline { get; private set; }
        public int joinType { get; private set; }
        public int status { get; private set; }
        public string modifiedTime { get; private set; }
        public int communityId { get; private set; }
        public string communityLink { get; private set; }
        public string iconUrl { get; private set; }
        public string updatedTime { get; private set; }
        public string endpoint { get; private set; }
        public string communityName { get; private set; }
        public int templateId { get; private set; }
        public string createdTime { get; private set; }
        public string json { get; private set; }
        public _Agent Agent { get; private set; }


        public Community(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            listedStatus = (int)jsonObj["listedStatus"];
            probationStatus = (int)jsonObj["probationStatus"];
            membersCount = (int)jsonObj["membersCount"];
            primaryLanguage = (string)jsonObj["primaryLanguage"];
            communityHeat = (float)jsonObj["communityHeat"];
            strategyInfo = (string)jsonObj["strategyInfo"];
            tagline = (string)jsonObj["tagline"];
            joinType = (int)jsonObj["joinType"];
            status = (int)jsonObj["status"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            communityId = (int)jsonObj["ndcId"];
            communityLink = (string)jsonObj["link"];
            iconUrl = (string)jsonObj["icon"];
            updatedTime = (string)jsonObj["updatedTime"];
            endpoint = (string)jsonObj["endpoint"];
            communityName = (string)jsonObj["name"];
            templateId = (int)jsonObj["templateId"];
            createdTime = (string)jsonObj["createdTime"];
            json = _json.ToString();
            Agent = new _Agent(_json);
        }

        public class _Agent
        {
            public bool isNickNameVerified { get; private set; }
            public string userId { get; private set; }
            public int level { get; private set; }
            public int followingStatus { get; private set; }
            public int membershipStatus { get; private set; }
            public bool isGlobal { get; private set; }
            public int reputation { get; private set; }
            public int membersCount { get; private set; }

            public _Agent(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                isNickNameVerified = (bool)jsonObj["agent"]["isNicknameVerified"];
                userId = (string)jsonObj["agent"]["uid"];
                level = (int)jsonObj["agent"]["level"];
                followingStatus = (int)jsonObj["agent"]["followingStatus"];
                membershipStatus = (int)jsonObj["agent"]["membershipStatus"];
                isGlobal = (bool)jsonObj["agent"]["isGlobal"];
                reputation = (int)jsonObj["agent"]["reputation"];
                membersCount = (int)jsonObj["agent"]["membersCount"];
            }
        }
    }
}
