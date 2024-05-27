using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class ChatTipExtensions
    {
        [JsonPropertyName("tippingCoins")] public int TippedCoins { get; set; }
    }
}
