using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Post
    {
        public int globalVotesCount { get; }
        public int globalVotedValue { get; }
        public int votedValue { get; }
        public string keywords { get; }
        public bool isGlobalAnnouncement { get; }
        public int style { get; }
        public int totalQuizPlayCount { get; }
        public string title { get; }
        public int contentRating { get; }
        public string content { get; }
        public bool needHidden { get; }
        public int guestVotesCount { get; }
        public int type { get; }
        public int status { get; }
        public int globalCommentsCount { get; }
        public string modifiedTime { get; }
        public int totalPollVoteCount { get; }
        public string postId { get; }
        public string shareURLFullPath { get; }
        public int viewCount { get; }
        public int votesCount { get; }
        public int communityId { get; }
        public string createdTime { get; }
        public string endTime { get; }
        public int commentsCount { get; }
        public string json { get; }
        public _TipInfo TipInfo { get; }
        public _Author Author { get; }
        public _Extensions Extensions { get; }


        public Post(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            globalVotesCount = (int)jsonObj["globalVotesCount"];
            globalVotedValue = (int)jsonObj["globalVotedValue"];
            votedValue = (int)jsonObj["votedValue"];
            if(jsonObj["keywords"] != null) { keywords = (string)jsonObj["keywords"]; }
            isGlobalAnnouncement = (bool)jsonObj["isGlobalAnnouncement"];
            style = (int)jsonObj["style"];
            totalQuizPlayCount = (int)jsonObj["totalQuizPlayCount"];
            title = (string)jsonObj["title"];
            contentRating = (int)jsonObj["contentRating"];
            content = (string)jsonObj["content"];
            needHidden = (bool)jsonObj["needHidden"];
            guestVotesCount = (int)jsonObj["guestVotesCount"];
            type = (int)jsonObj["type"];
            status = (int)jsonObj["status"];
            globalCommentsCount = (int)jsonObj["globalCommentsCount"];
            modifiedTime = (string)jsonObj["modifiedTime"];
            totalPollVoteCount = (int)jsonObj["totalPollVoteCount"];
            postId = (string)jsonObj["blogId"];
            shareURLFullPath = (string)jsonObj["shareURLFullPath"];
            viewCount = (int)jsonObj["viewCount"];
            votesCount = (int)jsonObj["votesCount"];
            communityId = (int)jsonObj["ndcId"];
            createdTime = (string)jsonObj["createdTime"];
            if(jsonObj["endTime"] != null) { endTime = (string)jsonObj["endTime"]; }
            commentsCount = (int)jsonObj["commentsCount"];
            json = _json.ToString();
            TipInfo = new _TipInfo(_json);
            Author = new _Author(_json);
            Extensions = new _Extensions(_json);

        }

        public class _TipInfo
        {
            public int tipMaxCoin { get; }
            public int tippersCount { get; }
            public bool tippable { get; }
            public int tipMinCoin { get; }
            public int tippedCoins { get; }

            public _TipInfo(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                tipMaxCoin = (int)jsonObj["tipInfo"]["tipMaxCoin"];
                tippersCount = (int)jsonObj["tipInfo"]["tippersCount"];
                tippable = (bool)jsonObj["tipInfo"]["tippable"];
                System.Console.WriteLine("Check 5");
                tipMinCoin = (int)jsonObj["tipInfo"]["tipMinCoin"];
                tippedCoins = (int)jsonObj["tipInfo"]["tippedCoins"];
            }
        }

        public class _Author
        {
            public int status { get; }
            public string userId { get; }
            public bool isGlobal { get; }
            public int role { get; }
            public bool isStaff { get; }
            public string nickname { get; }
            public string iconUrl { get; }

            public _Author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["author"]["status"];
                userId = (string)jsonObj["author"]["uid"];
                isGlobal = (bool)jsonObj["author"]["isGlobal"];
                role = (int)jsonObj["author"]["role"];
                if (jsonObj["author"]["isStaff"] != null) { isStaff = (bool)jsonObj["author"]["isStaff"]; }
                nickname = (string)jsonObj["author"]["nickname"];
                if(jsonObj["author"]["icon"] != null) { iconUrl = (string)jsonObj["author"]["icon"]; }
            }
        }

        public class _Extensions
        {
            public bool commentEnabled { get; }
            public string author { get; }

            public _Extensions(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                commentEnabled = (bool)jsonObj["extensions"]["commentEnabled"];
                author = (string)jsonObj["extensions"]["author"];
            }
        }

    }
}
