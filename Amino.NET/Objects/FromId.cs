using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FromId
    {
        public string path { get; }
        public string objectId { get; }
        public string shareURLShortCode { get; }
        public int targetCode { get; }
        public int communityId { get; }
        public string fullPath { get; }
        public string shortCode { get; }
        public string shareURLFullPath { get; }
        public int objectType { get; }
        public string json { get; }

        public FromId(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            path = (string)jsonObj["linkInfoV2"]["path"];
            objectId = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectId"];
            shareURLShortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shareURLShortCode"];
            targetCode = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["targetCode"];
            communityId = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["ndcId"];
            fullPath = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"];
            if(jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"] != null) { shortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"]; }
            shareURLFullPath = (string)jsonObj["linKInfoV2"]["extensions"]["linkInfo"]["shareURLFullPath"];
            objectType = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectType"];
            json = _json.ToString();

        }
    }
}
