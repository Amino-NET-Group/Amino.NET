using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class WalletInfo
    {
        public float totalCoinsFloat { get; }
        public bool adsEnabled { get; }
        public string adsVideoStats { get; }
        public string adsFlag { get; }
        public int totalCoins { get; }
        public bool businessCoinsEnabled { get; }
        public int totalBusinessCoins { get; }
        public float totalBusinessCoinsFloat { get; }
        public string json { get; }

        public WalletInfo(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            totalCoinsFloat = (float)jsonObj["wallet"]["totalCoinsFloat"];
            adsEnabled = (bool)jsonObj["wallet"]["adsEnabled"];
            adsVideoStats = (string)jsonObj["wallet"]["adsVideoStats"];
            adsFlag = (string)jsonObj["wallet"]["adsFlag"];
            totalCoins = (int)jsonObj["wallet"]["totalCoins"];
            businessCoinsEnabled = (bool)jsonObj["wallet"]["businessCoinsEnabled"];
            totalBusinessCoins = (int)jsonObj["wallet"]["totalBusinessCoins"];
            totalBusinessCoinsFloat = (float)jsonObj["wallet"]["totalBusinessCoinsFloat"];
            json = _json.ToString();
        }
    }
}
