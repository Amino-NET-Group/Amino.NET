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
        public int? targetCode { get; }
        public int? communityId { get; }
        public string fullPath { get; }
        public string shortCode { get; }
        public string shareURLFullPath { get; }
        public int? objectType { get; }
        public string json { get; }

        public FromId(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            try { path = (string)jsonObj["linkInfoV2"]["path"];  } catch { }
            try { objectId = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectId"]; } catch { }
            try { shareURLShortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shareURLShortCode"];  } catch { }
            try { targetCode = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["targetCode"];  } catch { }
            try { communityId = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["ndcId"];  } catch { }
            try { fullPath = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["fullPath"];  } catch { }
            try { shortCode = (string)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["shortCode"];  } catch { }
            try { shareURLFullPath = (string)jsonObj["linKInfoV2"]["extensions"]["linkInfo"]["shareURLFullPath"];  } catch { }
            try { objectType = (int)jsonObj["linkInfoV2"]["extensions"]["linkInfo"]["objectType"];  } catch { }
            json = _json.ToString();

        }
    }
}
