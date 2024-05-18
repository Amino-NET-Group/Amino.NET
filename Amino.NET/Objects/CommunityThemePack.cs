using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class CommunityThemePack
    {
        [JsonPropertyName("themeColor")] public string ThemeColor { get; set; }
        [JsonPropertyName("themePackHash")] public string ThemePackHash { get; set; }
        [JsonPropertyName("themePackRevision")] public string ThemePackRevision { get; set; }
        [JsonPropertyName("themePackUrl")] public string ThemePackUrl { get; set; }
    }
}
