using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LeftChatMember
    {
        public int _type { get; }
        public int alertOption { get; }
        public int membershipStatus { get; }
        public int communityId { get; }

        public string chatId { get; }
        public int mediaType { get; }
        public int clientrefId { get; }
        public string messageId { get; }
        public string userId { get; }
        public string createdTime { get; }
        public int type { get; }
        public bool isHidden { get; }
        public bool includedInSummary { get; }
        public string json { get; }

        public LeftChatMember(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            _type = (int)jsonObj["t"];
            alertOption = (int)jsonObj["o"]["alertOption"];
            membershipStatus = (int)jsonObj["o"]["membershipStatus"];
            communityId = (int)jsonObj["o"]["ndcId"];

            chatId = (string)jsonObj["o"]["chatMessage"]["threadId"];
            mediaType = (int)jsonObj["o"]["chatMessage"]["mediaType"];
            clientrefId = (int)jsonObj["o"]["chatMessage"]["clientRefId"];
            messageId = (string)jsonObj["o"]["chatMessage"]["messageId"];
            userId = (string)jsonObj["o"]["chatMessage"]["uid"];
            createdTime = (string)jsonObj["o"]["chatMessage"]["createdTime"];
            type = (int)jsonObj["o"]["chatMessage"]["type"];
            isHidden = (bool)jsonObj["o"]["chatMessage"]["isHidden"];
            includedInSummary = (bool)jsonObj["o"]["chatMessage"]["includedInSummary"];
            json = _json.ToString();

        }
    }
}
