using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class AdditionalItemBenefits
    {
        [JsonPropertyName("firstMonthFreeAminoPlusMembership")] public bool FirstMonthFreeAminoPlus { get; set; }
    }
}
