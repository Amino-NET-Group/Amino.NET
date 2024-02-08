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
            foreach(JObject clubMember in json["fanClubList"]) { FanClubMembers.Add(new(clubMember)); }
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

                }
            }

            public class _Extensions
            {
                public string defaultBubbleId { get; }
                public _Style Style { get; }
                public List<_CustomTitle> CustomTitles { get; } = new List<_CustomTitle>();

                public _Extensions(JObject json)
                {

                }

                public class _CustomTitle
                { 
                    public string color { get; }
                    public string title { get; }

                    public _CustomTitle(JObject json)
                    {

                    }
                }

                public class _Style
                {
                    public string backgroundColor { get; }

                    public _Style(JObject json)
                    {

                    }
                }
            }
        }
    }
}
