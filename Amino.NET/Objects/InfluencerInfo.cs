using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Amino.Objects
{
    public class InfluencerInfo
    {

        public _FanClubMember MyFanClub { get; }
        public _FanClubUserProfile InfluencerUserProfile { get; }
        public List<_FanClubMember> FanClubMembers { get; } = new();
        public string json { get; }

        public InfluencerInfo(JObject json)
        {
            this.json = json.ToString();
            try { MyFanClub = new(JObject.Parse((string)json["myFanClub"])); } catch { }
            try { InfluencerUserProfile = new(JObject.Parse((string)json["influencerUserProfile"])); } catch { }
            if (json["fanClubList"] != null) { foreach (JObject clubMember in json["fanClubList"]) { FanClubMembers.Add(new(clubMember)); } }
            
        }



        public class _FanClubMember
        {
            public string userId { get; }
            public string lastThankedTime { get; }
            public string expiredTime { get; }
            public string createdTime { get; }
            public int fansStatus { get; } = 0;
            public _FanClubUserProfile FanUserProfile { get; }

            public _FanClubMember(JObject json)
            {
                try { userId = (string)json["uid"]; } catch { }
                try { lastThankedTime = (string)json["lastThankedTime"]; } catch { }
                try { expiredTime = (string)json["expiredTime"]; } catch { }
                try { createdTime = (string)json["createdTime"]; } catch { }
                try { fansStatus = (int)json["fansStatus"]; } catch { }

                try { FanUserProfile = new(JObject.Parse((string)json["fansUserProfile"])); } catch { }
            } 



        }
        public class _FanClubUserProfile
        {
            public int status { get; } = 0;
            public string moodSticker { get; }
            public int itemsCount { get; } = 0;
            public int consecutiveCheckInDays { get; } = 0;
            public string userId { get; }
            public string modifiedTime { get; }
            public int followingStatus { get; } = 0;
            public int onlineStatus { get; } = 0;
            public int accountMembershipStatus { get; } = 0;
            public bool isGlobal { get; } = false;
            public string avatarFrameId { get; }
            public int reputation { get; } = 0;
            public int postsCount { get; } = 0;
            public int membersCount { get; } = 0;
            public string nickname { get; }
            public string iconUrl { get; }
            public bool isNicknameVerified { get; } = false;
            public string mood { get; }
            public int level { get; } = 0;
            public int notificationSubscriptionStatus { get; } = 0;
            public bool pushEnabled { get; } = false;
            public int membershipStatus { get; } = 0;
            public string content { get; }
            public int joinedCount { get; } = 0;
            public int role { get; } = 0;
            public int commentsCount { get; } = 0;
            public int communityId { get; } = 0;
            public string createdTime { get; }
            public int storiesCount { get; } = 0;
            public int blogsCount { get; } = 0;
            public _AvatarFrame AvatarFrame { get; }
            public _Extensions Extensions { get; }
            public _InfluencerInfo InfluencerInfo { get; }


            public _FanClubUserProfile(JObject json)
            {
                try { status = (int)json["status"]; } catch { }
                try { moodSticker = (string)json["moodSticker"]; } catch { }
                try { itemsCount = (int)json["itemsCount"]; } catch { }
                try { consecutiveCheckInDays = (int)json["consecutiveCheckInDays"]; } catch { }
                try { userId = (string)json["uid"]; } catch { }
                try { modifiedTime = (string)json["modifiedTime"]; } catch { }
                try { followingStatus = (int)json["followingStatus"]; } catch { }
                try { onlineStatus = (int)json["onlineStatus"]; } catch { }
                try { accountMembershipStatus = (int)json["accountMembershipStatus"]; } catch { }
                try { isGlobal = (bool)json["isGlobal"]; } catch { }
                try { reputation = (int)json["reputation"]; } catch { }
                try { postsCount = (int)json["postsCount"]; } catch { }
                try { membersCount = (int)json["membersCount"]; } catch { }
                try { nickname = (string)json["nickname"]; } catch { }
                try { iconUrl = (string)json["icon"]; } catch { }
                try { isNicknameVerified = (bool)json["isNicknameVerified"]; } catch { }
                try { mood = (string)json["mood"]; } catch { }
                try { level = (int)json["level"]; } catch { }
                try { notificationSubscriptionStatus = (int)json["notificationSubscriptionStatus"]; } catch { }
                try { pushEnabled = (bool)json["pushEnabled"]; } catch { }
                try { membershipStatus = (int)json["membershipStatus"]; } catch { }
                try { content = (string)json["content"]; } catch { }
                try { joinedCount = (int)json["joinedCount"]; } catch { }
                try { role = (int)json["role"]; } catch { }
                try { commentsCount = (int)json["commentsCount"]; } catch { }
                try { communityId = (int)json["ndcId"]; } catch { }
                try { createdTime = (string)json["createdTime"]; } catch { }
                try { storiesCount = (int)json["storiesCount"]; } catch { }
                try { blogsCount = (int)json["blogsCount"]; } catch { }

                if (json["extensions"] != null) { Extensions = new(JObject.Parse((string)json["extensions"])); }
                if (json["avatarFrame"] != null) { AvatarFrame = new(JObject.Parse((string)json["avatarFrame"])); }
                if (json["influencerInfo"] != null) { InfluencerInfo = new(JObject.Parse((string)json["influencerInfo"])); }
            }



            public class _AvatarFrame
            {
                public int status { get; } = 0;
                public int ownershipStatus { get; } = 0;
                public int version { get; } = 0;
                public string resoureUrl { get; }
                public string name { get; }
                public string iconUrl { get; }
                public int frameType { get; } = 0;
                public string frameId { get; }
                
                public _AvatarFrame(JObject json)
                {
                    try { status = (int)json["status"]; } catch { }
                    try { ownershipStatus = (int)json["ownershipStatus"]; } catch { }
                    try { version = (int)json["version"]; } catch { }
                    try { resoureUrl = (string)json["resourceUrl"]; } catch { }
                    try { name = (string)json["name"]; } catch { }
                    try { iconUrl = (string)json["icon"]; } catch { }
                    try { frameType = (int)json["frameType"]; } catch { }
                    try { frameId = (string)json["frameId"]; } catch { }
                }
            }

            public class _InfluencerInfo
            {
                public bool pinned { get; } = false;
                public string createdTime { get; }
                public int fansCount { get; } = 0;
                public int monthlyFee { get; } = 0;

                public _InfluencerInfo(JObject json)
                {
                    try { pinned = (bool)json["pinned"]; } catch { }
                    try { createdTime = (string)json["createdTime"]; } catch { }
                    try { fansCount = (int)json["fansCount"]; } catch { }
                    try { monthlyFee = (int)json["monthlyFee"]; } catch { }
                }
            }

            public class _Extensions
            {
                public string defaultBubbleId { get; }
                public _Style Style { get; }
                public List<_CustomTitle> CustomTitles { get; } = new List<_CustomTitle>();

                public _Extensions(JObject json)
                {
                    try { defaultBubbleId = (string)json["defaultBubbleId"]; } catch { }
                    if (json["style"] != null) { Style = new(JObject.Parse((string)json["style"])); }
                    if (json["customTitles"] != null)
                    {
                        foreach(JObject title in json["customTitles"]) { CustomTitles.Add(new(title)); }
                    }
                }

                public class _CustomTitle
                { 
                    public string color { get; }
                    public string title { get; }

                    public _CustomTitle(JObject json)
                    {
                        try { color = (string)json["color"]; } catch { }
                        try { title = (string)json["title"]; } catch { }
                    }
                }

                public class _Style
                {
                    public string backgroundColor { get; }

                    public _Style(JObject json)
                    {
                        try { backgroundColor = (string)json["backgroundColor"]; } catch { }
                    }
                }
            }
        }
    }
}
