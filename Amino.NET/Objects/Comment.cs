using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Comment
    {
        public string modifiedTime { get; private set; }
        public int communityId { get; private set; }
        public int votedValue { get; private set; }
        public int parentType { get; private set; }
        public string commentId { get; private set; }
        public int parentCommunityId { get; private set; }
        public int votesSum { get; private set; }
        public string content { get; private set; }
        public string parentId { get; private set; }
        public string createdTime { get; private set; }
        public int subcommentsCount { get; private set; }
        public int type { get; private set; }
        public string json { get; private set; }
        public _Author Author { get; private set; }

        public Comment(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            modifiedTime = (string)jsonObj["modifiedTime"];
            communityId = (int)jsonObj["ndcId"];
            votedValue = (int)jsonObj["votedValue"];
            parentType = (int)jsonObj["parentType"];
            commentId = (string)jsonObj["commentId"];
            parentCommunityId = (int)jsonObj["parentNdcId"];
            votesSum = (int)jsonObj["votesSum"];
            content = (string)jsonObj["content"];
            parentId = (string)jsonObj["parentId"];
            createdTime = (string)jsonObj["createdTime"];
            subcommentsCount = (int)jsonObj["subcommentsCount"];
            type = (int)jsonObj["type"];
            json = _json.ToString();
            Author = new _Author(_json);

        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public int status { get; private set; }
            public bool isNicknameVerified { get; private set; }
            public string userId { get; private set; }
            public int level { get; private set; }
            public int followingStatus { get; private set; }
            public int accountMembershipStatus { get; private set; }
            public int reputation { get; private set; }
            public bool isGlobal { get; private set; }
            public int membershipStatus { get; private set; }
            public int role { get; private set; }
            public string aminoId { get; private set; }
            public int communityId { get; private set; }
            public int membersCount { get; private set; }
            public string nickname { get; private set; }
            public string iconUrl { get; private set; }

            public _Author(JObject _json)
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
                aminoId = (string)jsonObj["author"]["aminoId"];
                communityId = (int)jsonObj["author"]["ndcId"];
                membersCount = (int)jsonObj["author"]["membersCount"];
                nickname = (string)jsonObj["author"]["nickname"];
                iconUrl = (string)jsonObj["author"]["icon"];
            }
        }
    }
}
