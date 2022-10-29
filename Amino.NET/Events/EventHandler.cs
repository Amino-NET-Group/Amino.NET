using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Events
{
    public class EventHandler
    {
        public Task ReceiveEvent(JObject webSocketMessage, Client client)
        {
            Client.Events eventCall = new Client.Events();
            eventCall.callWebSocketMessageEvent(client, this, webSocketMessage);
            try
            {
                Amino.Objects.Message _message = new Amino.Objects.Message(webSocketMessage);
                Client.Events events = new Client.Events();
                events.callMessageEvent(client, this, _message);
            }
            catch { }


            return Task.CompletedTask;
        }
    }
}
