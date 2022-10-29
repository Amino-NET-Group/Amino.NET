using Amino.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace Amino
{

    class WebSocketHandler
    {
        private string WebSocketURL = "wss://ws3.aminoapps.com";
        private IWebsocketClient ws_client;
        private Amino.Client _client;
        
        /// <summary>
        /// If you're trying to experiment with this package, there's no one to stop you, tho playing with the WebSocketHandler can lead to runtime issues I will not account for.
        /// </summary>
        public WebSocketHandler(Amino.Client client)
        {
            _client = client;
            _ = Task.Run(async () => { startWebSocket(client); });
        }

        private async Task startWebSocket(Amino.Client _client)
        {

            var final = $"{_client.deviceID}|{(Math.Round(helpers.GetTimestamp())) * 1000}";
            var factory = new Func<ClientWebSocket>(() =>
            {
                var client = new ClientWebSocket
                {
                    Options =
                    {
                            KeepAliveInterval = TimeSpan.FromSeconds(30)
                    }
                };
                client.Options.SetRequestHeader("NDCDEVICEID", _client.deviceID);
                client.Options.SetRequestHeader("NDCAUTH", $"sid={_client.sessionID}");
                client.Options.SetRequestHeader("NDC-MSG-SIG", helpers.generate_signiture(final));
                client.Options.SetRequestHeader("Upgrade", "websocket");
                client.Options.SetRequestHeader("Connection", "Upgrade");
                return client;

            });
            try
            {
                ws_client = new WebsocketClient(new Uri($"{WebSocketURL}/?signbody={final.Replace("|", "%7C")}"), factory);

                var exitEvent = new ManualResetEvent(false);
                var eventHandler = new Events.EventHandler();

                ws_client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                ws_client.DisconnectionHappened.Subscribe(info => { if (_client.debug) { Trace.WriteLine($"WebSocket: Disconnected\nReason: {info.CloseStatusDescription}"); } });
                ws_client.ReconnectionHappened.Subscribe(info => { if (_client.debug) { Trace.WriteLine($"WebSocket: Reconnected\nMessage: {info.Type}"); } });
                ws_client.MessageReceived.Subscribe(msg =>
                {
                    if (_client.debug) { Trace.WriteLine($"WebSocket: Received Message: {msg.Text}"); }
                    eventHandler.ReceiveEvent(JObject.Parse(msg.Text), _client);
                });
                ws_client.Start().Wait();
                exitEvent.WaitOne();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task send_socket_data(JObject _data)
        {
            string data = _data.ToString().Replace('=', ':');
            ws_client.Send(data);
            return Task.CompletedTask;
        }

        public async Task disconnect_socket()
        {
            await ws_client.Stop(WebSocketCloseStatus.NormalClosure, "WebSocket closed successfully");
            ws_client.Dispose();
            if (_client.debug) { Trace.WriteLine("WebSocket closed successfully."); }
            
            
        }
    }
}
