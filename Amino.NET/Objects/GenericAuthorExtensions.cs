using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class GenericAuthorExtensions
    {
        /// <summary>
        /// <para>The ID of the original sticker</para>
        /// NOTE: this might only be availabe in the context of a <see cref="Objects.StickerMessage"/>
        /// </summary>
        [JsonPropertyName("originalStickerId")] public string OriginalStickerId { get; set; }
        /// <summary>
        /// <para>The Sticker object related to this Extensions object</para>
        /// NOTE: this might only be availabe in the context of a <see cref="Objects.StickerMessage"/>
        /// </summary>
        [JsonPropertyName("sticker")] public Sticker Sticker { get; set; }
    }
}
