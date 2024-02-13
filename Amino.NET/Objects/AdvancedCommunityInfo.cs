using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AdvancedCommunityInfo
    {
        public bool isCurrentUserJoined { get; }
        public bool searchable { get; }
        public bool isStandaloneAppDeprecated { get; }
        public int listedStatus { get; }
        public int probationStatus { get; }
        public string keywords { get; }
        public int membersCount { get; }
        public string primaryLanguage { get; }
        public float communityHeat { get; }
        public string content { get; }
        public bool isStandaloneAppMonetizationEnabled { get; }
        public string tagline { get; }
        public int joinType { get; }
        public int status { get; }
        public string modifiedTime { get; }
        public int communityId { get; }
        public string link { get; }
        public string iconUrl { get; }
        public string updatedTime { get; }
        public string endpoint { get; }
        public string name { get; }
        public int templateId { get; }
        public string createdTime { get; }
        public string json { get; }
        public List<_User> communityHeadList { get; } = new List<_User>();
        public _User Agent { get; }
        public _ThemePack ThemePack { get; }
        public _AdvancedSettings AdvancedSettings { get; }



        public AdvancedCommunityInfo(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            try { isCurrentUserJoined = (bool)jsonObj["isCurrentUserJoined"]; } catch { }
            try { searchable = (bool)jsonObj["community"]["searchable"]; } catch { }
            try { isStandaloneAppDeprecated = (bool)jsonObj["community"]["isStandaloneAppDeprecated"]; } catch { }
            try { listedStatus = (int)jsonObj["community"]["listedStatus"]; } catch { }
            try { probationStatus = (int)jsonObj["community"]["probationStatus"]; }catch { }
            try { keywords = (string)jsonObj["community"]["keywords"]; }catch { }
            try { membersCount = (int)jsonObj["community"]["membersCount"]; }catch { }
            try { primaryLanguage = (string)jsonObj["community"]["primaryLanguage"]; } catch { }
            try { communityHeat = (float)jsonObj["community"]["communityHeat"]; } catch { }
            try { content = (string)jsonObj["community"]["content"]; } catch { }
            try { isStandaloneAppMonetizationEnabled = (bool)jsonObj["community"]["isStandaloneAppMonetizationEnabled"]; } catch { }
            try { tagline = (string)jsonObj["community"]["tagline"]; } catch { }
            try { joinType = (int)jsonObj["community"]["joinType"]; } catch { }
            try { status = (int)jsonObj["community"]["status"]; } catch { }
            try { modifiedTime = (string)jsonObj["community"]["modifiedTime"]; } catch { }
            try { communityId = (int)jsonObj["community"]["ndcId"]; } catch { }
            try { link = (string)jsonObj["community"]["link"]; } catch { }
            try { iconUrl = (string)jsonObj["community"]["icon"]; } catch { }
            try { updatedTime = (string)jsonObj["community"]["updatedTime"]; }catch { }
            try { endpoint = (string)jsonObj["community"]["endpoint"]; } catch { }
            try { name = (string)jsonObj["community"]["name"]; } catch { }
            try { templateId = (int)jsonObj["community"]["templateId"]; }catch { }
            try { createdTime = (string)jsonObj["community"]["createdTime"]; } catch { }
            try { json = _json.ToString(); } catch { } 
            try { Agent = new _User(jsonObj["community"]["agent"]); } catch { }
            try { ThemePack = new _ThemePack(jsonObj["community"]["themePack"]); } catch { }
            try { AdvancedSettings = new _AdvancedSettings(jsonObj["community"]["advancedSettings"]); } catch { }
            try
            {
                JArray userArray = jsonObj["community"]["communityHeadList"];
                if (userArray != null)
                {
                    foreach (JObject user in userArray)
                    {
                        _User _user = new _User(user);
                        communityHeadList.Add(_user);
                    }
                }

            }catch { }
        
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _User
        {
            public int status { get; }
            public bool isNicknameVerified { get; }
            public string userId { get; }
            public int level { get; }
            public int followingStatus { get; }
            public int accountMembershipStatus { get; }
            public bool isGlobal { get; }
            public int membershipStatus { get; }
            public string avatarFrameId { get; }
            public int reputation { get; }
            public int role { get; }
            public int communityId { get; }
            public int membersCount { get; }
            public string nickname { get; }
            public string iconUrl { get; }


            public _User(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                try { status = (int)jsonObj["status"]; } catch { }
                try { isNicknameVerified = (bool)jsonObj["isNicknameVerified"]; } catch { }
                try { userId = (string)jsonObj["uid"]; } catch { }
                try { level = (int)jsonObj["level"]; } catch { }
                try { followingStatus = (int)jsonObj["followingStatus"]; } catch { }
                try { accountMembershipStatus = (int)jsonObj["accountMembershipStatus"]; } catch { }
                try { isGlobal = (bool)jsonObj["isGlobal"]; } catch { }
                try { membershipStatus = (int)jsonObj["membershipStatus"]; } catch { }
                try { avatarFrameId = (string)jsonObj["avatarFrameId"]; } catch { }
                try { reputation = (int)jsonObj["reputation"]; } catch { }
                try { role = (int)jsonObj["role"]; } catch { }
                try { communityId = (int)jsonObj["ndcId"]; } catch { }
                try { membersCount = (int)jsonObj["membersCount"]; } catch { }
                try { nickname = (string)jsonObj["nickname"]; } catch { }
                try { iconUrl = (string)jsonObj["icon"]; } catch { }

            }



        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _ThemePack
        {
            public string themeColor { get; }
            public string themePackHash { get; }
            public int themePackRevision { get; }
            public string themePackUrl { get; }

            public _ThemePack(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                try { themeColor = (string)jsonObj["themeColor"]; } catch { }
                try { themePackHash = (string)jsonObj["themePackHash"]; } catch { }
                try { themePackRevision = (int)jsonObj["themePackRevision"]; } catch { }
                try { themePackUrl = (string)jsonObj["themePackUrl"]; } catch { }
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _AdvancedSettings
        {
            public int defaultRakingTypeInLeaderBoard { get; }
            public int frontPageLayout { get; }
            public bool hasPendingReviewRequest { get; }
            public bool welcomeMessageEnabled { get; }
            public string welcomeMessageText { get; }
            public int pollMinFullBarCount { get; }
            public bool catalogEnabled { get; }
            public List<_NewsFeed> NewsFeed { get; } = new List<_NewsFeed>();
            public List<_Ranks> Ranks { get; } = new List<_Ranks>();

            public _AdvancedSettings(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                try { defaultRakingTypeInLeaderBoard = (int)jsonObj["defaultRankingTypeInLeaderboard"]; } catch { }
                try { frontPageLayout = (int)jsonObj["frontPageLayout"]; } catch { }
                try { hasPendingReviewRequest = (bool)jsonObj["hasPendingReviewRequest"]; } catch { }
                try { welcomeMessageEnabled = (bool)jsonObj["welcomeMessageEnabled"]; } catch { }
                try { pollMinFullBarCount = (int)jsonObj["pollMinFullBarCount"]; } catch { }
                try { catalogEnabled = (bool)jsonObj["catalogEnabled"]; } catch { }
                try
                {
                    JArray newsFeed = jsonObj["newsfeedPages"];
                    foreach (JObject post in newsFeed)
                    {
                        _NewsFeed _post = new _NewsFeed(post);
                        NewsFeed.Add(_post);
                    }
                }catch { }
                try
                {
                    JArray ranks = jsonObj["rankingTable"];
                    foreach (JObject rank in ranks)
                    {
                        _Ranks _rank = new _Ranks(rank);
                        Ranks.Add(_rank);
                    }
                }catch { }
            }



            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _NewsFeed
            {
                public int status { get; }
                public int type { get; }

                public _NewsFeed(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    try { status = (int)jsonObj["status"]; } catch { }
                    try { type = (int)jsonObj["type"]; } catch { }
                }
            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _Ranks
            {
                public int level { get; }
                public int reputation { get; }
                public string id { get; }
                public string title { get; }

                public _Ranks(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    try { level = (int)jsonObj["level"]; } catch { }
                    try { reputation = (int)jsonObj["reputation"]; } catch { }
                    try { id = (string)jsonObj["id"]; } catch { }
                    try { title = (string)jsonObj["title"]; } catch { }
                }
            }

        }


    }
}
