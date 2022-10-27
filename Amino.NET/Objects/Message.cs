using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Message
    {
        public string? content { get; private set; }
        public string? messageId { get; private set; }
        public string? chatId { get; private set; }
        public string? objectId { get; private set; }
        public string? json { get; private set; }
        public int? communityId { get; private set; }
        public string? chatBubbleId { get; private set; }
        public _Author? Author { get; }

        public Message(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            Author = new _Author(_json);
            content = (string)jsonObj["o"]["chatMessage"]["content"];
            messageId = (string)jsonObj["o"]["chatMessage"]["messageId"];
            chatId = (string)jsonObj["o"]["chatMessage"]["threadId"];
            objectId = (string)jsonObj["o"]["chatMessage"]["uid"];
            json = _json.ToString();
            communityId = (int)jsonObj["o"]["ndcId"];
            chatBubbleId = (string)jsonObj["o"]["chatBubbleId"];
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _Author
        {
            public string? userName { get; }
            public string? userId { get; }
            public int? userLevel { get; }
            public string? iconUrl { get; }
            public int? userReputation { get; }
            public string? frameId { get; }

            public _Author(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                userName = (string)jsonObj["o"]["chatMessage"]["author"]["nickname"];
                userId = (string)jsonObj["o"]["chatMessage"]["author"]["uid"];
                userLevel = (int)jsonObj["o"]["chatMessage"]["author"]["level"];
                iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["icon"];
                userReputation = (int)jsonObj["o"]["chatMessage"]["author"]["reputation"];
                try { frameId = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["frameId"]; } catch { }
            }
        }
    }

}
