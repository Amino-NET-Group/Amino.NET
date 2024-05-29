using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class LinkInfo
    {
        [JsonPropertyName("objectId")] public string ObjectId { get; set; }
        [JsonPropertyName("targetCode")] public string TargetCode { get; set; }
        [JsonPropertyName("ndcId")] public int? CommunityId { get; set; }
        [JsonPropertyName("fullPath")] public string FullPath { get; set; }
        [JsonPropertyName("shortCode")] public string ShortCode { get; set; }
        [JsonPropertyName("objectType")] public int? ObjectType { get; set; }
        /// <summary>
        /// <para>The shareable URL Shortcode of an Object</para>
        /// Available with <see cref="Amino.Client.get_from_id(string, Types.Object_Types, string)"/>
        /// </summary>
        [JsonPropertyName("shareURLShortCode")] public string ShareURLShortCode { get; set; }
        /// <summary>
        /// <para>The full path URL of an Object</para>
        /// Availabe with <see cref="Amino.Client.get_from_id(string, Types.Object_Types, string)"/>
        /// </summary>
        [JsonPropertyName("shareURLFullPath")] public string ShareURLFullPath { get; set; }

    }
}
