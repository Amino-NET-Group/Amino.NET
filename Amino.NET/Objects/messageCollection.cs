using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class MessageCollection
    {
        [JsonPropertyName("messageList")]public List<GenericMessage> Messages { get; set; }
        [JsonPropertyName("paging")]public PagingInfo Paging { get; set; }
    }
}
