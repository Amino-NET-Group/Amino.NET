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
        public List<_member> MemberSummary { get; }
        public _author Author { get; }
        public _extensions Extensions { get; }

        public chatThreads(JObject _json)
        {
            json = _json.ToString();
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

            }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _extensions
        {
            public bool viewOnly { get; private set; }
            public int lastMembersSummaryUpdateTime { get; private set; }

            public _extensions(JObject _json)
            {

            }
        }
    }
}
