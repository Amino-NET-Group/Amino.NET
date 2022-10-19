using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class chatThreads
    {
        public string userId { get; private set; }
        public int membersQuota { get; private set; }
        public string threadId { get; private set; }
        public string keywords { get; private set; }
        public int membersCount { get; private set; }
        public string strategyInfo { get; private set; }
        public bool isPinned { get; private set; }
        public string title { get; private set; }
        public int membershipStatus { get; private set; }
        public string content { get; private set; }
        public bool needHidden { get; private set; }
        public int alertOption { get; private set; }
        public string lastReadTime { get; private set; }
        public int type { get; private set; }
        public int status { get; private set; }
        public bool mentionMe { get; private set; }
        public string modifiedTime { get; private set; }
        public int condition { get; private set; }
        public string iconUrl { get; private set; }
        public string latestActivityTime { get; private set; }
        public int communityId { get; private set; }
        public string createdTime { get; private set; }
        public string json { get; private set; }
        public List<_member> MemberSummary { get; } = new List<_member>();
        public _author Author { get; }
        public _extensions Extensions { get; }

        public chatThreads(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            json = _json.ToString();

            userId = (string)jsonObj["uid"];
            membersQuota = (int)jsonObj["membersQuota"];
            threadId = (string)jsonObj["threadId"];
            keywords = (string)jsonObj["keywords"];
            membersCount = (int)jsonObj["membersCount"];
            strategyInfo = (string)jsonObj["strategyInfo"];
            isPinned = (bool)jsonObj["isPinned"];
            title = (string)jsonObj["title"];
            membershipStatus = (int)jsonObj["membershipStatus"];
            content = (string)jsonObj["content"];
            needHidden = (bool)jsonObj["needHidden"];
            alertOption = (int)jsonObj["alertOption"];
            lastReadTime = (string)jsonObj["lastReadTime"];
            type = (int)jsonObj["type"];
            status = (int)jsonObj["status"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            condition = (int)jsonObj["condition"];
            iconUrl = (string)jsonObj["icon"];
            communityId = (int)jsonObj["ndcId"];
            createdTime = (string)jsonObj["createdTime"];
            JArray _memberList = jsonObj["membersSummary"];

            foreach (JObject _member in _memberList)
            {
                _member member = new _member(_member);

                MemberSummary.Add(member);
            }

            Author = new _author(_json);
            Extensions = new _extensions(_json);
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _member
        {
            public int status { get; private set; }
            public string userId { get; private set; }
            public int membershipStatus { get; private set; }
            public int role { get; private set; }
            public string nickname { get; private set; }
            public string iconUrl { get; private set; }

            public _member(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["status"];
                userId = (string)jsonObj["uid"];
                membershipStatus = (int)jsonObj["membershipStatus"];
                role = (int)jsonObj["role"];
                nickname = (string)jsonObj["nickname"];
                iconUrl = (string)jsonObj["icon"];
            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _lastMessageSummary
        {
            public string userId { get; private set; }
            public bool isHidden { get; private set; }
            public string mediaType { get; private set; }
            public string content { get; private set; }
            public string messageId { get; private set; }
            public string createdTime { get; private set; }
            public int type { get; private set; }
            public string mediaValue { get; private set; }

            public _lastMessageSummary(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                userId = (string)jsonObj["lastMessageSummary"]["uid"];
                isHidden = (bool)jsonObj["lastMessageSummary"]["isHidden"];
                mediaType = (string)jsonObj["lastMessageSummary"]["mediaType"];
                content = (string)jsonObj["lastMessageSummary"]["content"];
                messageId = (string)jsonObj["lastMessageSummary"]["messageId"];
                createdTime = (string)jsonObj["lastMessageSummary"]["createdTime"];
                type = (int)jsonObj["lastMessageSummary"]["type"];
                mediaType = (string)jsonObj["lastMessageSummary"]["mediaType"];
            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _author
        {
            public int status { get; private set; }
            public bool isNicknameVerified { get; private set; }
            public string userId { get; private set; }
            public int level { get; private set; }
            public int followingStatus { get; private set; }
            public int accountMembershipStatus { get; private set; }
            public bool isGlobal { get; private set; }
            public int membershipStatus { get; private set; }
            public int reputation { get; private set; }
            public int role { get; private set; }
            public string aminoId { get; private set; }
            public int communityId { get; private set; }
            public int membersCount { get; private set; }
            public string nickname { get; private set; }
            public string iconUrl { get; private set; }

            public _author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["author"]["status"];
                isNicknameVerified = (bool)jsonObj["author"]["isNicknameVerified"];
                userId = (string)jsonObj["author"]["uid"];
                level = (int)jsonObj["author"]["level"];
                followingStatus = (int)jsonObj["author"]["followingStatus"];
                accountMembershipStatus = (int)jsonObj["author"]["accountMembershipStatus"];
                isGlobal = (bool)jsonObj["author"]["isGlobal"];
                membershipStatus = (int)jsonObj["author"]["membershipStatus"];
                reputation = (int)jsonObj["author"]["reputation"];
                role = (int)jsonObj["author"]["role"];
                communityId = (int)jsonObj["author"]["ndcId"];
                membersCount = (int)jsonObj["author"]["membersCount"];
                nickname = (string)jsonObj["author"]["nickname"];
                iconUrl = (string)jsonObj["author"]["icon"];
                    
            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _extensions
        {
            public bool viewOnly { get; private set; }
            public int lastMembersSummaryUpdateTime { get; private set; }
            public string channelType { get; private set; }

            public _extensions(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                
                if (jsonObj["extensions"]["viewOnly"] != null) { viewOnly = (bool)jsonObj["extensions"]["viewOnly"]; }
                lastMembersSummaryUpdateTime = (int)jsonObj["extensions"]["lastMembersSummaryUpdateTime"];
                if (jsonObj["extensions"]["channelType"] != null) { channelType = (string)jsonObj["extensions"]["channelType"]; }
            }
        }
    }
}
