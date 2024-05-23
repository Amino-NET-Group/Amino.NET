using System.Text.Json.Serialization;

namespace Amino.Objects
{

    // ROOT JSON ELEMENT: wallet
    public class WalletInfo
    {
        [JsonPropertyName("totalCoinsFloat")]public float TotalCoinsFloat { get; set; }
        [JsonPropertyName("adsEnabled")]public bool AdsEnabled { get; set; }
        [JsonPropertyName("adsVideoStats")]public string AdsVideoStats { get; set; }
        [JsonPropertyName("adsFlag")]public string AdsFlag { get; set; }
        [JsonPropertyName("totalCoins")]public int TotalCoins { get; set; }
        [JsonPropertyName("businessCoinsEnabled")]public bool BusinessCoinsEnabled { get; set; }
        [JsonPropertyName("totalBusinessCoins")]public int TotalBusinessCoins { get; set; }
        [JsonPropertyName("totalBusinessCoinsFloat")]public float TotalBusinessCoinsFloat { get; set; }
    }
}
