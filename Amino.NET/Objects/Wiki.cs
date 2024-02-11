using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Wiki
    {
        public string itemId { get; }
        public int status { get; } = 0;
        public int style { get; } = 0;
        public int globalCommentsCount { get; } = 0;
        public string modifiedTime { get; }
        public int votedValue { get; } = 0;
        public int globalVotesCount { get; } = 0;
        public int globalVotedValue { get; } = 0;
        public int contentRating { get; } = 0;
        public string label { get; }
        public string content { get; } = "";
        public string keywords { get; }
        public bool needHidden { get; } = false;
        public int guestVotesCount { get; } = 0;
        public int votesCount { get; } = 0;
        public int communityId { get; } = 0;
        public string createdTime { get; }
        public int commentsCount { get; } = 0;
        public string json { get; }
        public _Author Author { get; }

        public Wiki(JObject json)
        {
            this.json = json.ToString();
            try { itemId = (string)json["itemId"]; } catch { }
            try { status = (int)json["status"]; } catch { }
            try { style = (int)json["style"]; } catch { }
            try { globalCommentsCount = (int)json["globalCommentsCount"]; } catch { }
            try { modifiedTime = (string)json["modifiedTime"]; } catch { }
            try { votedValue = (int)json["votedValue"]; } catch { }
            try { globalVotesCount = (int)json["globalVotesCount"]; } catch { }
            try { globalVotedValue = (int)json["globalVotedValue"]; } catch { }
            try { contentRating = (int)json["contentRating"]; } catch { }
            try { label = (string)json["label"]; } catch { }
            try { content = (string)json["content"]; } catch { }
            try { keywords = (string)json["keywords"]; } catch { }
            try { needHidden = (bool)json["needHidden"]; } catch { }
            try { guestVotesCount = (int)json["guestVotesCount"]; } catch { }
            try { votesCount = (int)json["votesCount"]; } catch { }
            try { communityId = (int)json["ndcId"]; } catch { }
            try { createdTime = (string)json["createdTime"]; } catch { }
            try { commentsCount = (int)json["commentsCount"]; } catch { }

            try { Author = new(JObject.Parse((string)json["author"])); } catch { }

        }



        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public int status { get; } = 0;
            public bool isNicknameVerified { get; } = false;
            public int level { get; } = 0;
            public int followingStatus { get; } = 0;
            public string userId { get; }
            public int accountMembershipStatus { get; }
            public bool isGlobal { get; } = false;
            public int membershipStatus { get; } = 0;
            public string avatarFrameId { get; }
            public int reputation { get; } = 0;
            public int role { get; } = 0;
            public int communityId { get; } = 0;
            public int membersCount { get; } = 0;
            public string nickname { get; }
            public string iconUrl { get; }
            public _AvatarFrame AvatarFrame { get; }

            public _Author(JObject json)
            {
                try { status = (int)json["status"]; } catch { }
                try { isNicknameVerified = (bool)json["isNicknameVerified"]; } catch { }
                try { level = (int)json["level"]; } catch { }
                try { userId = (string)json["uid"]; } catch { }
                try { accountMembershipStatus = (int)json["accountMembershipStatus"]; } catch { }
                try { isGlobal = (bool)json["isGlobal"]; } catch { }
                try { followingStatus = (int)json["followingStatus"]; } catch { }
                try { membershipStatus = (int)json["membershipStatus"]; } catch { }
                try { avatarFrameId = (string)json["avatarFrameId"]; } catch { }
                try { reputation = (int)json["reputation"]; } catch { }
                try { role = (int)json["role"]; } catch { }
                try { communityId = (int)json["ndcId"]; } catch { }
                try { membersCount = (int)json["membersCount"]; } catch { }
                try { nickname = (string)json["nickname"]; } catch { }
                try { iconUrl = (string)json["icon"]; } catch { }
                try { AvatarFrame = new(JObject.Parse((string)json["avatarFrame"])); } catch { }
            }

            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _AvatarFrame
            {
                public int status { get; } = 0;
                public int version { get; } = 0;
                public string resourceUrl { get; }
                public string name { get; }
                public string iconUrl { get; }
                public int frameType { get; }
                public string frameId { get; }

                public _AvatarFrame(JObject json)
                {
                    try { status = (int)json["status"]; } catch { }
                    try { version = (int)json["version"]; } catch { }
                    try { resourceUrl = (string)json["resourceUrl"]; } catch { }
                    try { name = (string)json["name"]; } catch { }
                    try { iconUrl = (string)json["icon"]; } catch { }
                    try { frameType = (int)json["frameType"]; } catch { }
                    try { frameId = (string)json["frameId"]; } catch { }
                }
            }
        }
    }
}
