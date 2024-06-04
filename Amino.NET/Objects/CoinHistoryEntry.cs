using System.Text.Json.Serialization;

namespace Amino.Objects
{

    public class CoinHistoryEntry
    {
        [JsonPropertyName("isPositive")] public bool? IsPositive { get; set; }
        [JsonPropertyName("totalCoins")] public int? TotalCoins { get; set; }
        [JsonPropertyName("originalCoinsFloat")] public float? OriginCoinsFloat { get; set; }
        [JsonPropertyName("sourceType")] public int? SourceType { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("bonusCoins")] public int? BonusCoins { get; set; }
        [JsonPropertyName("totalCoinsFloat")] public float? TotalCoinsFloat { get; set; }
        [JsonPropertyName("bonusCoinsFloat")] public float? BonusCoinsFloat { get; set; }
        [JsonPropertyName("changedCoinsFloat")] public float? ChangedCoinsFloat { get; set; }
        [JsonPropertyName("taxCoinsFloat")] public float? TaxCoinsFloat { get; set; }
        [JsonPropertyName("taxCoins")] public int? TaxCoins { get; set; }
        [JsonPropertyName("uid")] public string EntryId { get; set; }
        [JsonPropertyName("changedCoins")] public int? ChangedCoins { get; set; }
        [JsonPropertyName("originCoins")] public int? OriginCoins { get; set; }
        [JsonPropertyName("extData")] public WalletHistoryExtraData ExtData { get; set; }

    }
}
