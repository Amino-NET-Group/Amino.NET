
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FromCode
    {
        public string path { get; }
        public string objectId { get; }
        public int targetCode { get; }
        public int communityId { get; }
        public string fullPath { get; }
        public string shortCode { get; }
        public int objectType { get; }
        public string json { get; }

        public FromCode(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            path = (string)jsonObj["linkInfoV2"]["path"];
            objectId = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectId"];
            targetCode = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["targetCode"];
            communityId = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["ndcId"];
            if(jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"] != null) { fullPath = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"]; }
            if(jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"] != null) { shortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"]; }
            objectType = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectType"];
            json = _json.ToString();
        }
    }
}
