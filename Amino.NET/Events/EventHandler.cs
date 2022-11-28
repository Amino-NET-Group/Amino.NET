using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Events
{
    /// <summary>
    /// This class is responsible for all Events of Amino.Net
    /// </summary>
    public class EventHandler
    {
        /// <summary>
        /// This function handles the websocket responses and converts them into objects to call events with
        /// </summary>
        /// <param name="webSocketMessage"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Task ReceiveEvent(JObject webSocketMessage, Client client)
        {
            Client.Events eventCall = new Client.Events();
            eventCall.callWebSocketMessageEvent(client, webSocketMessage);
            try
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(webSocketMessage.ToString());
                if(jsonObj["o"]["chatMessage"]["mediaType"] != null)
                {
                    switch((int)jsonObj["o"]["chatMessage"]["mediaType"])
                    {
                        case 0: //TextMessage / MessageDeleted / ChatMember Left, ChatMember Joined / ChatBackground changed / ChatTitle changed
                            switch((int)jsonObj["o"]["chatMessage"]["type"])
                            {
                                case 0: //Textmessage recevied
                                    Amino.Objects.Message _message = new Amino.Objects.Message(webSocketMessage);
                                    eventCall.callMessageEvent(client, this, _message);
                                    break;
                                case 100: // Textmessage deleted
                                    Amino.Objects.DeletedMessage _deletedMessage = new Objects.DeletedMessage(webSocketMessage);
                                    eventCall.callMessageDeletedEvent(client, _deletedMessage);
                                    break;
                                case 101: // ChatMember Joined
                                    Amino.Objects.JoinedChatMember _joinedMember = new Objects.JoinedChatMember(webSocketMessage);
                                    eventCall.callChatMemberJoinEvent(client, _joinedMember);
                                    break;
                                case 102: // ChatMember Left
                                    Amino.Objects.LeftChatMember _leftMember = new Objects.LeftChatMember(webSocketMessage);
                                    eventCall.callChatMemberLeaveEvent(client, _leftMember);
                                    break;
                                case 104: // ChatBackground changed
                                    Amino.Objects.ChatEvent _chatBackgroundChanged = new Objects.ChatEvent(webSocketMessage);
                                    eventCall.callChatBackgroundChangedEvent(client, _chatBackgroundChanged);
                                    break;
                                case 105: // ChatTitle changed
                                    Amino.Objects.ChatEvent _chatTitleChanged = new Objects.ChatEvent(webSocketMessage);
                                    eventCall.callChatTitleChangedEvent(client, _chatTitleChanged);  
                                    break;
                            }

                            break;
                        case 100: //ImageMessage
                            Amino.Objects.ImageMessage _imageMessage = new Amino.Objects.ImageMessage(webSocketMessage);
                            eventCall.callImageMessageEvent(client, _imageMessage);
                            break;
                        case 103: //YouTubeMessage
                            Amino.Objects.YouTubeMessage _youtubeMessage = new Objects.YouTubeMessage(webSocketMessage);
                            eventCall.callYouTubeMessageEvent(client, _youtubeMessage);
                            break;
                        case 110: //VoiceMessage
                            Amino.Objects.VoiceMessage _voiceMessage = new Objects.VoiceMessage(webSocketMessage);
                            eventCall.callVoiceMessageEvent(client, _voiceMessage);
                            break;
                        case 113: //StickerMessage
                            Amino.Objects.StickerMessage _stickerMessage = new Objects.StickerMessage(webSocketMessage);
                            eventCall.callStickerMessageEvent(client, _stickerMessage);
                            break;
                    }
                }
            }
            catch { }


            return Task.CompletedTask;
        }
    }
}
