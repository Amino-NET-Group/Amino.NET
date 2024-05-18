using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amino.Objects;
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
                JsonElement root = JsonDocument.Parse(webSocketMessage.ToString()).RootElement;
                SocketBase sBase = System.Text.Json.JsonSerializer.Deserialize<SocketBase>(root.GetProperty("o"));
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(webSocketMessage.ToString());
                if(jsonObj["o"]["chatMessage"]["mediaType"] != null)
                {

                    switch((int)jsonObj["o"]["chatMessage"]["mediaType"])
                    {
                        case 0: //TextMessage / MessageDeleted / ChatMember Left, ChatMember Joined / ChatBackground changed / ChatTitle changed / ChatContent chaaged / ChatAnnouncementPin / ChatAnnouncementUnpin / ChatViewOnlyOn / ChatViewOnlyOff / ChatTipEnabled / ChatTipDisabled / MessageForceRemoved / ChatTip
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
                                case 113: // ChatContent Changed
                                    Amino.Objects.ChatEvent _chatContentChanged = new Objects.ChatEvent(webSocketMessage);
                                    eventCall.callChatContentChangedEvent(client, _chatContentChanged);
                                    break;
                                case 119: // MessageForceRemovedByAdmin
                                    Amino.Objects.SpecialChatEvent _messageForceRemovedByAdmin = new Objects.SpecialChatEvent(webSocketMessage);
                                    eventCall.callMessageForceRemovedByAdminEvent(client, _messageForceRemovedByAdmin);
                                    break;
                                case 120: // ChatTip
                                    Amino.Objects.ChatTip _chatTip = new Objects.ChatTip(webSocketMessage);
                                    eventCall.callChatTipEvent(client, _chatTip);
                                    break;
                                case 121: // ChatAnnouncementPin
                                    Amino.Objects.ChatAnnouncement _chatAnnouncementPinEvent = new Objects.ChatAnnouncement(webSocketMessage);
                                    eventCall.callChatPinAnnouncementEvent(client, _chatAnnouncementPinEvent);
                                    break;
                                case 125: // ChatViewModeOn
                                    Amino.Objects.ViewMode _viewModeOn = new Objects.ViewMode(webSocketMessage);
                                    eventCall.callChatViewModeOnEvent(client, _viewModeOn);
                                    break;
                                case 126: // ChatViewModeOff
                                    Amino.Objects.ViewMode _viewModeOff = new Objects.ViewMode(webSocketMessage);
                                    eventCall.callChatViewModeOffEvent(client, _viewModeOff);
                                    break;
                                case 127: // ChatAnnouncementUnPin
                                    Amino.Objects.ChatEvent _chatAnnouncementUnPin = new Objects.ChatEvent(webSocketMessage);
                                    eventCall.callChatUnpinAnnouncementEvent(client, _chatAnnouncementUnPin);
                                    break;
                                case 128: // ChatTipEnabled
                                    Amino.Objects.ChatTipToggle _chatTipEnabled = new Objects.ChatTipToggle(webSocketMessage);
                                    eventCall.callChatTipEnabledEvent(client, _chatTipEnabled);
                                    break;
                                case 129: // ChatTipDisabled
                                    Amino.Objects.ChatTipToggle _chatTipDisabled = new Objects.ChatTipToggle(webSocketMessage);
                                    eventCall.callChatTipDisabledEvent(client, _chatTipDisabled);
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
