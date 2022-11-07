using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CoinHistoryEntry
    {
        public bool isPositive { get; }
        public int totalCoins { get; }
        public float originCoinsFloat { get; }
        public int sourceType { get; }
        public string createdTime { get; }
        public int bonusCoins { get; }
        public float totalCoinsFloat { get; }
        public float bonusCoinsFloat { get; }
        public float changedCoinsFloat { get; }
        public float taxCoinsFloat { get; }
        public int taxCoins { get; }
        public string userId { get; }
        public int changedCoins { get; }
        public int originCoins { get; }
        public string json { get; }
        public _ExtData ExtData { get; }

        public CoinHistoryEntry(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            isPositive = (bool)jsonObj["isPositive"];
            totalCoins = (int)jsonObj["totalCoins"];
            originCoinsFloat = (int)jsonObj["originCoinsFloat"];
            sourceType = (int)jsonObj["sourceType"];
            createdTime = (string)jsonObj["createdTime"];
            if(jsonObj["bonusCoins"] != null) { bonusCoins = (int)jsonObj["bonusCoins"]; }
            totalCoinsFloat = (float)jsonObj["totalCoinsFloat"];
            if(jsonObj["bonusCoinsFloat"] != null) { bonusCoinsFloat = (float)jsonObj["bonusCoinsFloat"]; }
            changedCoinsFloat = (int)jsonObj["changedCoinsFloat"];
            if(jsonObj["taxCoinsFloat"] != null) { taxCoinsFloat = (float)jsonObj["taxCoinsFloat"]; }
            if(jsonObj["taxCoins"] != null) { taxCoins = (int)jsonObj["taxCoins"]; }
            userId = (string)jsonObj["uid"];
            changedCoins = (int)jsonObj["changedCoins"];
            originCoins = (int)jsonObj["originCoins"];
            json = _json.ToString();
            ExtData = new _ExtData(_json);

        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _ExtData
        {
            public string objectDeeplinkUrl { get; }
            public string description { get; }
            public string iconUrl { get; }
            public string subtitle { get; }

            public _ExtData(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                objectDeeplinkUrl = (string)jsonObj["extData"]["objectDeeplinkUrl"];
                description = (string)jsonObj["extData"]["description"];
                if(jsonObj["extData"]["icon"] != null) { iconUrl = (string)jsonObj["extData"]["icon"]; }
                subtitle = (string)jsonObj["extData"]["subtitle"];
            }
        }
    }
}
