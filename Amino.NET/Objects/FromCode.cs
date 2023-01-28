
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
        public Community Community { get; } = null;

        public FromCode(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            try { if (jsonObj["linkInfoV2"]["path"] != null) { path = (string)jsonObj["linkInfoV2"]["path"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectId"] != null) { objectId = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectId"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["targetCode"] != null) { targetCode = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["targetCode"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["ndcId"] != null) { communityId = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["ndcId"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"] != null) { fullPath = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"] != null) { shortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"]; } } catch { }
            try { if (jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectType"] != null) { objectType = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectType"]; } } catch { }
            if(jsonObj["linkInfoV2"]["extensions"]["community"] != null) { Community = new Community(jsonObj["linkInfoV2"]["extensions"]["community"]); }
            
            json = _json.ToString();
        }
    }
}
