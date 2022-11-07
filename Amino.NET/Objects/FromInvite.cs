using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FromInvite
    {
        public bool isCurrentUserJoined { get; }
        public string path { get; }
        public string invitationId { get; }
        public _Community Community { get; }
        public _CurrentUserInfo CurrentUserInfo { get; }
        public _Invitation Invitation { get; }
        public string json { get; }

        public FromInvite(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            if(jsonObj["isCurrentUserJoined"] != null) { isCurrentUserJoined = (bool)jsonObj["isCurrentUserJoined"]; }
            path = (string)jsonObj["path"];
            if(jsonObj["invitationId"] != null) { invitationId = (string)jsonObj["invitationId"]; }
            json = _json.ToString();
            if(jsonObj["invitation"] != null) { Invitation = new _Invitation(_json); }
            if(jsonObj["currentUserInfo"] != null) { CurrentUserInfo = new _CurrentUserInfo(_json); }
            if(jsonObj["community"] != null) { Community = new _Community(_json); }
            

        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Community
        {
            public string link { get; }
            public string primaryLanguage { get; }
            public string iconUrl { get; }
            public string name { get; }
            public int membersCount { get; }
            public int probationStatus { get; }
            public string content { get; }
            public int templateId { get; }
            public bool isStandaloneAppMonetizationEnabled { get; }
            public string tagline { get; }
            public int status { get; }
            public string endpoint { get; }
            public string createdTime { get; }
            public bool isStandaloneAppDeprecated { get; }
            public int listedStatus { get; }
            public float communityHeat { get; }
            public bool searchable { get; }
            public int communityId { get; }
            public int joinType { get; }
            public string modifiedTime { get; }
            public string updatedTime { get; }
            public _AdvancedSettings AdvancedSettings { get; }
            public _ThemePack ThemePack { get; }
            public _Agent Agent { get; }

            public _Community(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                link = (string)jsonObj["community"]["link"];
                primaryLanguage = (string)jsonObj["community"]["primaryLanguage"];
                if(jsonObj["community"]["icon"] != null) { iconUrl = (string)jsonObj["community"]["icon"]; }
                name = (string)jsonObj["community"]["name"];
                membersCount = (int)jsonObj["community"]["membersCount"];
                probationStatus = (int)jsonObj["community"]["probationStatus"];
                if(jsonObj["community"]["content"] != null) { content = (string)jsonObj["community"]["content"]; }
                templateId = (int)jsonObj["community"]["templateId"];
                isStandaloneAppMonetizationEnabled = (bool)jsonObj["community"]["isStandaloneAppMonetizationEnabled"];
                if(jsonObj["community"]["tagline"] != null) { tagline = (string)jsonObj["community"]["tagline"]; }
                status = (int)jsonObj["community"]["status"];
                endpoint = (string)jsonObj["community"]["endpoint"];
                createdTime = (string)jsonObj["community"]["createdTime"];
                isStandaloneAppDeprecated = (bool)jsonObj["community"]["isStandaloneAppDeprecated"];
                listedStatus = (int)jsonObj["community"]["listedStatus"];
                communityHeat = (float)jsonObj["community"]["communityHeat"];
                searchable = (bool)jsonObj["community"]["searchable"];
                communityId = (int)jsonObj["community"]["ndcId"];
                joinType = (int)jsonObj["community"]["joinType"];
                modifiedTime = (string)jsonObj["community"]["modifiedTime"];
                updatedTime = (string)jsonObj["community"]["updatedTime"];
                if(jsonObj["community"]["themePack"] != null) { ThemePack = new _ThemePack(_json); }
                if(jsonObj["community"]["agent"] != null) { Agent = new _Agent(_json); }
                if(jsonObj["community"]["advancedSettings"] != null) { AdvancedSettings = new _AdvancedSettings(_json); }
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _Agent
            {
                public int membershipStatus { get; }
                public int accountMembershipStatus { get; }
                public int membersCount { get; }
                public bool isGlobal { get; }
                public string userId { get; }
                public int level { get; }
                public bool isNicknameVerified { get; }
                public int reputation { get; }
                public int followingStatus { get; }
                public string iconUrl { get; }

                public _Agent(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    membershipStatus = (int)jsonObj["community"]["agent"]["membershipStatus"];
                    accountMembershipStatus = (int)jsonObj["community"]["agent"]["accountMembershipStatus"];
                    membersCount = (int)jsonObj["community"]["agent"]["membersCount"];
                    isGlobal = (bool)jsonObj["community"]["agent"]["isGlobal"];
                    userId = (string)jsonObj["community"]["agent"]["uid"];
                    level = (int)jsonObj["community"]["agent"]["level"];
                    isNicknameVerified = (bool)jsonObj["community"]["agent"]["isNicknameVerified"];
                    reputation = (int)jsonObj["community"]["agent"]["reputation"];
                    followingStatus = (int)jsonObj["community"]["agent"]["followingStatus"];
                    if(jsonObj["community"]["agent"]["icon"] != null) { iconUrl = (string)jsonObj["community"]["argent"]["icon"]; }
                }
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _ThemePack
            {
                public int themePackRevision { get; }
                public string themePackUrl { get; }
                public string themeColor { get; }
                public string themePackHash { get; }

                public _ThemePack(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    themePackRevision = (int)jsonObj["community"]["themePack"]["themePackRevision"];
                    themePackUrl = (string)jsonObj["community"]["themePack"]["themePackUrl"];
                    themeColor = (string)jsonObj["community"]["themePack"]["themeColor"];
                    themePackHash = (string)jsonObj["community"]["themePack"]["themePackHash"];
                }
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _AdvancedSettings
            {
                public string welcomeMessageText { get; }
                public int pollMinFullBarVoteCount { get; }
                public int frontPageLayout { get; }
                public bool hasPendingReviewRequest { get; }
                public int defaultRankingTypeInLeaderboard { get; }

                public _AdvancedSettings(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    welcomeMessageText = (string)jsonObj["community"]["advancedSettings"]["welcomeMessageText"];
                    pollMinFullBarVoteCount = (int)jsonObj["community"]["advancedSettings"]["pollMinFullBarVoteCount"];
                    frontPageLayout = (int)jsonObj["community"]["advancedSettings"]["frontPageLayout"];
                    hasPendingReviewRequest = (bool)jsonObj["community"]["advancedSettings"]["hasPendingReviewRequest"];
                    defaultRankingTypeInLeaderboard = (int)jsonObj["community"]["advancedSettings"]["defaultRankingTypeInLeaderboard"];
                }
            }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _CurrentUserInfo
        {
            public int notificationsCount { get; }
            public _UserProfile UserProfile { get; }

            public _CurrentUserInfo(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                notificationsCount = (int)jsonObj["currentUserInfo"]["notificationsCount"];
                UserProfile = new _UserProfile(_json);
            }


            [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
            public class _UserProfile
            {
                public string iconUrl { get; }
                public string createdTime { get; }
                public int commentsCount { get; }
                public int followingStatus { get; }
                public string nickname { get; }
                public string userId { get; }
                public string content { get; }
                public int joinedCount { get; }
                public int storiesCount { get; }
                public int reputation { get; }
                public int consecutiveCheckInDays { get; }
                public int accountMembershipStatus { get; }
                public int status { get; }
                public int onlineStatus { get; }
                public int postsCount { get; }
                public int membershipStatus { get; }
                public bool pushEnabled { get; }
                public int level { get; }
                public string modifiedTime { get; }
                public bool isGlobal { get; }
                public int communityId { get; }
                public int itemsCount { get; }
                public string moodSticker { get; }
                public bool isNicknameVerified { get; }
                public int role { get; }
                public int notificationSubscriptionStatus { get; }
                public string avatarFrameId { get; }
                public int blogsCount { get; }
                public int membersCount { get; }
                public _Settings Settings { get; }
                public List<_FanClubMember> FanClubList { get; } = new List<_FanClubMember>();
                public _InfluencerInfo InfluencerInfo { get; }
                public _Extensions Extensions { get; }
                public _AvatarFrame AvatarFrame { get; }

                public _UserProfile(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    iconUrl = (string)jsonObj["currentUserInfo"]["userProfile"]["icon"];
                    createdTime = (string)jsonObj["currentUserInfo"]["userProfile"]["createdTime"];
                    commentsCount = (int)jsonObj["currentUserInfo"]["userProfile"]["commentsCount"];
                    followingStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["followingStatus"];
                    nickname = (string)jsonObj["currentUserInfo"]["userProfile"]["nickname"];
                    userId = (string)jsonObj["currentUserInfo"]["userProfile"]["uid"];
                    content = (string)jsonObj["currentUserInfo"]["userProfile"]["content"];
                    joinedCount = (int)jsonObj["currentUserInfo"]["userProfile"]["joinedCount"];
                    storiesCount = (int)jsonObj["currentUserInfo"]["userProfile"]["storiesCount"];
                    reputation = (int)jsonObj["currentUserInfo"]["userProfile"]["reputation"];
                    if (jsonObj["currentUserInfo"]["userProfile"]["consecutiveCheckInDays"] != null) { consecutiveCheckInDays = (int)jsonObj["currentUserInfo"]["userProfile"]["consecutiveCheckInDays"]; }
                    accountMembershipStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["accountMembershipStatus"];
                    status = (int)jsonObj["currentUserInfo"]["userProfile"]["status"];
                    onlineStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["onlineStatus"];
                    postsCount = (int)jsonObj["currentUserInfo"]["userProfile"]["postsCount"];
                    membershipStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["membershipStatus"];
                    pushEnabled = (bool)jsonObj["currentUserInfo"]["userProfile"]["pushEnabled"];
                    level = (int)jsonObj["currentUserInfo"]["userProfile"]["level"];
                    modifiedTime = (string)jsonObj["currentUserInfo"]["userProfile"]["modifiedTime"];
                    isGlobal = (bool)jsonObj["currentUserInfo"]["userProfile"]["isGlobal"];
                    communityId = (int)jsonObj["currentUserInfo"]["userProfile"]["ndcId"];
                    itemsCount = (int)jsonObj["currentUserInfo"]["userProfile"]["itemsCount"];
                    moodSticker = (string)jsonObj["currentUserInfo"]["userProfile"]["moodSticker"];
                    isNicknameVerified = (bool)jsonObj["currentUserInfo"]["userProfile"]["isNicknameVerified"];
                    role = (int)jsonObj["currentUserInfo"]["userProfile"]["role"];
                    notificationSubscriptionStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["notificationSubscriptionStatus"];
                    if(jsonObj["currentUserInfo"]["userProfile"]["avatarFrameId"] != null) { avatarFrameId = (string)jsonObj["currentUserInfo"]["userProfile"]["avatarFrameId"]; }
                    blogsCount = (int)jsonObj["currentUserInfo"]["userProfile"]["blogsCount"];
                    membersCount = (int)jsonObj["currentUserInfo"]["userProfile"]["membersCount"];

                    if (jsonObj["currentUserInfo"]["userProfile"]["settings"] != null) { Settings = new _Settings(_json); }
                    if(jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
                    if(jsonObj["currentUserInfo"]["userProfile"]["influencerInfo"] != null) { InfluencerInfo = new _InfluencerInfo(_json); }
                    if(jsonObj["currentUserInfo"]["userProfile"]["extensions"] != null) { Extensions = new _Extensions(_json); }
                    if(jsonObj["currentUserInfo"]["userProfile"]["fanClubList"] != null)
                    {
                        JArray _fanClubMemberArray = jsonObj["currentUserInfo"]["userProfile"]["fanClubList"];
                        foreach(JObject fanClubMember in _fanClubMemberArray)
                        {
                            _FanClubMember _fanClubMember = new _FanClubMember(fanClubMember);
                            FanClubList.Add(_fanClubMember);
                        }
                    }
                }


                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _AvatarFrame
                {
                    public string frameId { get; }
                    public int status { get; }
                    public int ownershipStatus { get; }
                    public int version { get; }
                    public string resourceUrl { get; }
                    public string name { get; }
                    public string iconUrl { get; }
                    public int frameType { get; }

                    public _AvatarFrame(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        frameId = (string)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["frameId"];
                        status = (int)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["status"];
                        if(jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["ownershipStatus"] != null) { ownershipStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["ownershipStatus"]; }
                        version = (int)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["version"];
                        resourceUrl = (string)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["resourceUrl"];
                        name = (string)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["name"];
                        iconUrl = (string)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["icon"];
                        frameType = (int)jsonObj["currentUserInfo"]["userProfile"]["avatarFrame"]["frameType"];
                    }
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _Extensions
                {
                    public string defaultBubbleId { get; }

                    public _Extensions(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        if (jsonObj["currentUserInfo"]["userProfile"]["extensions"]["defaultBubbleId"] != null) { defaultBubbleId = jsonObj["currentUserInfo"]["userProfile"]["extensions"]["defaultBubbleId"]; }
                    }
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _InfluencerInfo
                {
                    public int monthlyFee { get; }
                    public bool isPinned { get; }
                    public string createdTime { get; }
                    public int fansCount { get; }

                    public _InfluencerInfo(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        monthlyFee = (int)jsonObj["currentUserInfo"]["userProfile"]["influencerInfo"]["monthlyFee"];
                        isPinned = (bool)jsonObj["currentUserInfo"]["userProfile"]["influencerInfo"]["pinned"];
                        createdTime = (string)jsonObj["currentUserInfo"]["userProfile"]["influencerInfo"]["createdTime"];
                        fansCount = (int)jsonObj["currentUserInfo"]["userProfile"]["influencerInfo"]["fansCount"];
                    }
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _Settings
                {
                    public int onlineStatus { get; }

                    public _Settings(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        onlineStatus = (int)jsonObj["currentUserInfo"]["userProfile"]["settings"]["onlineStatus"];
                    }
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _FanClubMember
                {
                    public int fansStatus { get; }
                    public string createdTime { get; }
                    public string targetUserId { get; }
                    public string expiredTime { get; }
                    public _TargetUserProfile TargetUserProfile { get; }


                    public _FanClubMember(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        fansStatus = (int)jsonObj["fansStatus"];
                        createdTime = (string)jsonObj["createdTime"];
                        targetUserId = (string)jsonObj["targetUid"];
                        expiredTime = (string)jsonObj["expiredTime"];
                        TargetUserProfile = new _TargetUserProfile(_json);
                    }

                    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                    public class _TargetUserProfile
                    {
                        public int membershipStatus { get; }
                        public int accountMembershipStatus { get; }
                        public int status { get; }
                        public int followingStatus { get; }
                        public int reputation { get; }
                        public int communityId { get; }
                        public bool isNicknameVerified { get; }
                        public string iconUrl { get; }
                        public string userId { get; }
                        public int role { get; }
                        public bool isGlobal { get; }
                        public int level { get; }
                        public int membersCount { get; }
                        public string nickname { get; }
                        public _AvatarFrame AvatarFrame { get; }


                        public _TargetUserProfile(JObject _json)
                        {
                            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                            membershipStatus = (int)jsonObj["targetUserProfile"]["membershipStatus"];
                            accountMembershipStatus = (int)jsonObj["targetUserProfile"]["accountMembershipStatus"];
                            status = (int)jsonObj["targetUserProfile"]["status"];
                            followingStatus = (int)jsonObj["targetUserProfile"]["followingStatus"];
                            reputation = (int)jsonObj["targetUserProfile"]["reputation"];
                            communityId = (int)jsonObj["targetUserProfile"]["ndcId"];
                            isNicknameVerified = (bool)jsonObj["targetUserProfile"]["isNicknameVerified"];
                            if(jsonObj["targetUserProfile"]["icon"] != null) { iconUrl = (string)jsonObj["targetUserProfile"]["icon"]; }
                            userId = (string)jsonObj["targetUserProfile"]["uid"];
                            role = (int)jsonObj["targetUserProfile"]["role"];
                            isGlobal = (bool)jsonObj["targetUserProfile"]["isGlobal"];
                            level = (int)jsonObj["targetUserProfile"]["level"];
                            membersCount = (int)jsonObj["targetUserProfile"]["membersCount"];
                            nickname = (string)jsonObj["targetUserProfile"]["nickname"];
                            if(jsonObj["targetUserProfile"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
                        }

                        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                        public class _AvatarFrame
                        {
                            public string frameId { get; }
                            public int status { get; }
                            public int ownershipStatus { get; }
                            public int version { get; }
                            public string resourceUrl { get; }
                            public string name { get; }
                            public string iconUrl { get; }
                            public int frameType { get; }

                            public _AvatarFrame(JObject _json)
                            {
                                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                                frameId = (string)jsonObj["targetUserProfile"]["avatarFrame"]["frameId"];
                                status = (int)jsonObj["targetUserProfile"]["avatarFrame"]["status"];
                                if(jsonObj["targetUserProfile"]["avatarFrame"]["ownershipStatus"] != null) { ownershipStatus = (int)jsonObj["targetUserProfile"]["avatarFrame"]["ownershipStatus"]; }
                                version = (int)jsonObj["targetUserProfile"]["avatarFrame"]["version"];
                                resourceUrl = (string)jsonObj["targetUserProfile"]["avatarFrame"]["resourceUrl"];
                                name = (string)jsonObj["targetUserProfile"]["avatarFrame"]["name"];
                                iconUrl = (string)jsonObj["targetUserProfile"]["avatarFrame"]["iconUrl"];
                                frameType = (int)jsonObj["targetUserProfile"]["avatarFrame"]["frameType"];
                            }
                        }
                    }
                }
            }


        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Invitation
        {
            public string link { get; }
            public string modifiedTime { get; }
            public string invitationId { get; }
            public string createdTime { get; }
            public int duration { get; }
            public int status { get; }
            public int communityId { get; }
            public string inviteCode { get; }
            public _Author Author { get; }

            public _Invitation(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());

                link = (string)jsonObj["invitation"]["link"];
                modifiedTime = (string)jsonObj["invitation"]["modifiedTime"];
                invitationId = (string)jsonObj["invitation"]["invitationId"];
                createdTime = (string)jsonObj["invitation"]["createdTime"];
                duration = (int)jsonObj["invitation"]["duration"];
                status = (int)jsonObj["invitation"]["status"];
                communityId = (int)jsonObj["invitation"]["ndcId"];
                inviteCode = (string)jsonObj["invitation"]["inviteCode"];
                Author = new _Author(_json);

            }


            public class _Author
            {
                public int reputation { get; }
                public bool isGlobal { get; }
                public int role { get; }
                public string userId { get; }
                public int followingStatus { get; }
                public int level { get; }
                public bool isNicknameVerified { get; }
                public string avatarFrameId { get; }
                public string nickname { get; }
                public string iconUrl { get; }
                public int membershipStatus { get; }
                public int communityId { get; }
                public int membersCount { get; }
                public int accountMembershipStatus { get; }
                public int status { get; }
                public _InfluencerInfo InfluencerInfo { get; }
                public _AvatarFrame AvatarFrame { get; }

                public _Author(JObject _json)
                {
                    dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                    reputation = (int)jsonObj["invitation"]["author"]["reputation"];
                    isGlobal = (bool)jsonObj["invitation"]["author"]["isGlobal"];
                    role = (int)jsonObj["invitation"]["author"]["role"];
                    userId = (string)jsonObj["invitation"]["author"]["uid"];
                    followingStatus = (int)jsonObj["invitation"]["author"]["followingStatus"];
                    level = (int)jsonObj["invitation"]["author"]["level"];
                    isNicknameVerified = (bool)jsonObj["invitation"]["author"]["isNicknameVerified"];
                    if(jsonObj["invitation"]["author"]["avatarFrameId"] != null) { avatarFrameId = (string)jsonObj["invitation"]["author"]["avatarFrameId"]; }
                    if(jsonObj["invitation"]["author"]["influencerInfo"] != null) { InfluencerInfo = new _InfluencerInfo(_json); }
                    nickname = (string)jsonObj["invitation"]["author"]["nickname"];
                    iconUrl = (string)jsonObj["invitation"]["author"]["icon"];
                    if(jsonObj["invitation"]["author"]["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
                    membershipStatus = (int)jsonObj["invitation"]["author"]["membershipStatus"];
                    accountMembershipStatus = (int)jsonObj["invitation"]["author"]["accountMembershipStatus"];
                    status = (int)jsonObj["invitation"]["author"]["status"];
                }


                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _InfluencerInfo
                {
                    public bool isPinned { get; }
                    public string createdTime { get; }
                    public int fansCount { get; }
                    public int monthlyFee { get; }

                    public _InfluencerInfo(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        isPinned = (bool)jsonObj["invitation"]["author"]["influencerInfo"]["pinned"];
                        createdTime = (string)jsonObj["invitation"]["author"]["influencerInfo"]["createdTime"];
                        fansCount = (int)jsonObj["invitation"]["author"]["influencerInfo"]["fansCount"];
                        monthlyFee = (int)jsonObj["invitation"]["author"]["influencerInfo"]["monthlyFee"];
                    }
                }

                [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
                public class _AvatarFrame
                {
                    public int version { get; }
                    public string resourceUrl { get; }
                    public string name { get; }
                    public string iconUrl { get; }
                    public int frameType { get; }
                    public string frameId { get; }
                    public int status { get; }
                    public int ownershipStatus { get; }

                    public _AvatarFrame(JObject _json)
                    {
                        dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                        version = (int)jsonObj["invitation"]["author"]["avatarFrame"]["version"];
                        resourceUrl = (string)jsonObj["invitation"]["author"]["avatarFrame"]["resourceUrl"];
                        name = (string)jsonObj["invitation"]["author"]["avatarFrame"]["name"];
                        iconUrl = (string)jsonObj["invitation"]["author"]["avatarFrame"]["icon"];
                        frameType = (int)jsonObj["invitation"]["author"]["avatarFrame"]["frameType"];
                        frameId = (string)jsonObj["invitation"]["author"]["avatarFrame"]["frameId"];
                        status = (int)jsonObj["invitation"]["author"]["avatarFrame"]["status"];
                        if(jsonObj["invitation"]["author"]["avatarFrame"]["ownershipStatus"] != null) { ownershipStatus = (int)jsonObj["invitation"]["author"]["avatarFrame"]["ownershipStatus"]; }
                    }

                }
            }
        }
    }
}
