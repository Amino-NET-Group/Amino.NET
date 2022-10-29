using Newtonsoft.Json.Linq;
using System;


namespace Amino.Events
{
    public class webSocketMessageEvent : EventArgs
    {
        public string webSocketMessageString { get; private set; }
        public JObject webSocketMessageJObject { get; private set; }
        public webSocketMessageEvent(JObject _webSocketMessage)
        {
            webSocketMessageString = _webSocketMessage.ToString();
            webSocketMessageJObject = _webSocketMessage;
        }
    }
}
