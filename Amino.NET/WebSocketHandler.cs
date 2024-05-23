using Amino.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
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
        private WebsocketClient ws_client;
        private Amino.Client _client;

        public WebSocketHandler(Amino.Client client)
        {
            _client = client;
            _ = Task.Run(async () => { await StartWebSocket(client); });
        }

        private async Task StartWebSocket(Amino.Client _client)
        {
            var final = $"{_client.deviceID}|{(Math.Round(helpers.GetTimestamp())) * 1000}";
            WebSocketURL += $"/?signbody={final.Replace("|", "%7C")}";

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
                ws_client = new WebsocketClient(new Uri(WebSocketURL), factory);

                var eventHandler = new Events.EventHandler();

                ws_client.ReconnectTimeout = TimeSpan.FromSeconds(45);
                ws_client.DisconnectionHappened.Subscribe(info =>
                {
                    if (_client.debug)
                    {
                        Trace.WriteLine($"WebSocket: Disconnected\nReason: {info.CloseStatusDescription}");
                        // Attempt reconnection only if the disconnection wasn't triggered by the user
                        if (info.CloseStatus != WebSocketCloseStatus.NormalClosure)
                        {
                            ws_client.Reconnect();
                        }
                    }
                });
                ws_client.ReconnectionHappened.Subscribe(info =>
                {
                    if (_client.debug)
                    {
                        Trace.WriteLine($"WebSocket: Reconnected\nMessage: {info.Type}");
                    }
                });
                ws_client.MessageReceived.Subscribe(msg =>
                {
                    if (_client.debug)
                    {
                        Trace.WriteLine($"WebSocket: Received Message: {msg.Text}");
                    }
                    eventHandler.ReceiveEvent(msg.Text, _client);
                });

                await ws_client.Start();

                // Keep the console application running until explicitly stopped
                await Task.Delay(Timeout.Infinite);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task SendSocketData(JObject _data)
        {
            string data = _data.ToString().Replace('=', ':');
            ws_client.Send(data);
        }

        public async Task DisconnectSocket()
        {
            await ws_client.Stop(WebSocketCloseStatus.NormalClosure, "WebSocket closed successfully");
            ws_client.Dispose();
            if (_client.debug)
            {
                Trace.WriteLine("WebSocket closed successfully.");
            }
        }
    }
}
