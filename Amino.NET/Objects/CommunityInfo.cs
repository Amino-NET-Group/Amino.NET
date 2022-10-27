using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CommunityInfo
    {
        public int objectType { get; private set; }
        public string aminoId { get; private set; }
        public string objectId { get; private set; }
        public int communityId { get; private set; }
        public string userAddedTopicList { get; private set; }
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
        public string communityLink { get; private set; }
        public string iconUrl { get; private set; }
        public string updatedTime { get; private set; }
        public string endpoint { get; private set; }
        public string name { get; private set; }
        public int templateId { get; private set; }
        public string createdTime { get; private set; }
        public string json { get; private set; }
        public _Agent Agent { get; }
        public _ThemePack ThemePack { get; }

        public CommunityInfo(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            objectType = (int)jsonObj["objectType"];
            aminoId = (string)jsonObj["aminoId"];
            objectId = (string)jsonObj["objectId"];
            communityId = (int)jsonObj["ndcId"];
            userAddedTopicList = (string)jsonObj["refObject"]["userAddedTopicList"];
            listedStatus = (int)jsonObj["refObject"]["listedStatus"];
            probationStatus = (int)jsonObj["refObject"]["probationStatus"];
            membersCount = (int)jsonObj["refObject"]["membersCount"];
            primaryLanguage = (string)jsonObj["refObject"]["primaryLanguage"];
            communityHeat = (float)jsonObj["refObject"]["communityHeat"];
            strategyInfo = (string)jsonObj["refObject"]["strategyInfo"];
            tagline = (string)jsonObj["refObject"]["tagline"];
            joinType = (int)jsonObj["refObject"]["joinType"];
            status = (int)jsonObj["refObject"]["status"];
            modifiedTime = (string)jsonObj["refObject"]["modifiedTime"];
            communityLink = (string)jsonObj["refObject"]["link"];
            iconUrl = (string)jsonObj["refObject"]["icon"];
            updatedTime = (string)jsonObj["refObject"]["updatedTime"];
            endpoint = (string)jsonObj["refObject"]["endpoint"];
            name = (string)jsonObj["refObject"]["name"];
            templateId = (int)jsonObj["refObject"]["templateId"];
            createdTime = (string)jsonObj["refObject"]["createdTime"];
            json = _json.ToString();
            Agent = new _Agent(_json);
            ThemePack = new _ThemePack(_json);
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Agent
        {
            public string status { get; private set; }
            public bool isNicknameVerified { get; private set; }
            public string userId { get; private set; }
            public int level { get; private set; }
            public int followingStatus { get; private set; }
            public int accountMembershipStatus { get; private set; }
            public bool isGlobal { get; private set; }
            public int membershipStatus { get; private set; }
            public int reputation { get; private set; }
            public int role { get; private set; }
            public int communityId { get; private set; }
            public int membersCount { get; private set; }
            public string nickname { get; private set; }
            public string iconUrl { get; private set; }

            public _Agent(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (string)jsonObj["refObject"]["agent"]["status"];
                isNicknameVerified = (bool)jsonObj["refObject"]["agent"]["isNicknameVerified"];
                userId = (string)jsonObj["refObject"]["agent"]["uid"];
                level = (int)jsonObj["refObject"]["agent"]["level"];
                followingStatus = (int)jsonObj["refObject"]["agent"]["followingStatus"];
                accountMembershipStatus = (int)jsonObj["refObject"]["agent"]["accountMembershipStatus"];
                isGlobal = (bool)jsonObj["refObject"]["agent"]["isGlobal"];
                membershipStatus = (int)jsonObj["refObject"]["agent"]["membershipStatus"];
                reputation = (int)jsonObj["refObject"]["agent"]["reputation"];
                if(jsonObj["refObject"]["agent"]["role"] != null) { role = (int)jsonObj["refObject"]["agent"]["role"]; }
                if(jsonObj["refObject"]["agent"]["ndcId"] != null) { communityId = (int)jsonObj["refObject"]["agent"]["ndcId"]; }
                membersCount = (int)jsonObj["refObject"]["agent"]["membersCount"];
                if(jsonObj["refObject"]["agent"]["nickname"] != null) { nickname = (string)jsonObj["refObject"]["agent"]["nickname"]; }
                if(jsonObj["refObject"]["agent"]["icon"] != null) { iconUrl = (string)jsonObj["refObject"]["agent"]["icon"]; }
            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _ThemePack
        {
            public string themeColor { get; private set; }
            public string themePackHash { get; private set; }
            public int themePackRevision { get; private set; }
            public string themePackUrl { get; private set; }

            public _ThemePack(JObject _json)
            {
               dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                themeColor = (string)jsonObj["refObject"]["themePack"]["themeColor"];
                themePackHash = (string)jsonObj["refObject"]["themePack"]["themePackHash"];
                themePackRevision = (int)jsonObj["refObject"]["themePack"]["themePackRevision"];
                themePackUrl = (string)jsonObj["refObject"]["themePack"]["themePackUrl"];
            }
        }
    }
}
