using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Amino.Objects
{
    //MediaType: 0
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
            try { Author = new _Author(_json); } catch { }
            try { content = (string)jsonObj["o"]["chatMessage"]["content"]; } catch { }
            try { messageId = (string)jsonObj["o"]["chatMessage"]["messageId"]; } catch { }
            try { chatId = (string)jsonObj["o"]["chatMessage"]["threadId"]; }catch{ }
            try { objectId = (string)jsonObj["o"]["chatMessage"]["uid"]; } catch { }
            json = _json.ToString();
            try { communityId = (int)jsonObj["o"]["ndcId"]; } catch { }
            try { chatBubbleId = (string)jsonObj["o"]["chatBubbleId"]; }catch{ }
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
                try { userName = (string)jsonObj["o"]["chatMessage"]["author"]["nickname"]; } catch { }
                try { userId = (string)jsonObj["o"]["chatMessage"]["author"]["uid"]; } catch { }
                try { userLevel = (int)jsonObj["o"]["chatMessage"]["author"]["level"]; } catch { }
                try { iconUrl = (string)jsonObj["o"]["chatMessage"]["author"]["icon"]; } catch { }
                try { userReputation = (int)jsonObj["o"]["chatMessage"]["author"]["reputation"]; } catch { }
                try { frameId = (string)jsonObj["o"]["chatMessage"]["author"]["avatarFrame"]["frameId"]; } catch { }
            }
        }
    }

}
