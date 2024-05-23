using System.Text.Json;
using System.Threading.Tasks;
using Amino.Objects;


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
        public Task ReceiveEvent(string webSocketMessage, Client client)
        {
            Client.Events eventCall = new Client.Events();
            eventCall.callWebSocketMessageEvent(client, webSocketMessage);
            try
            {
                JsonElement root = JsonDocument.Parse(webSocketMessage.ToString()).RootElement;
                if(root.GetProperty("o").GetProperty("chatMessage").GetProperty("mediaType").ValueKind != JsonValueKind.Null)
                {
                    SocketBase _socketBase = JsonSerializer.Deserialize<SocketBase>(root.GetProperty("o").GetRawText());
                    string element = root.GetProperty("o").GetProperty("chatMessage").GetRawText();
                    switch (root.GetProperty("o").GetProperty("chatMessage").GetProperty("mediaType").GetInt32())
                    {
                        case 0: //TextMessage / MessageDeleted / ChatMember Left, ChatMember Joined / ChatBackground changed / ChatTitle changed / ChatContent chaaged / ChatAnnouncementPin / ChatAnnouncementUnpin / ChatViewOnlyOn / ChatViewOnlyOff / ChatTipEnabled / ChatTipDisabled / MessageForceRemoved / ChatTip
                            switch(root.GetProperty("o").GetProperty("chatMessage").GetProperty("type").GetInt32())
                            {
                                case 0: //Textmessage recevied
                                    Objects.Message _message = JsonSerializer.Deserialize<Message>(element);
                                    _message.Json = webSocketMessage;
                                    _message.SocketBase = _socketBase;
                                    
                                    eventCall.callMessageEvent(client, this, _message);
                                    break;
                                case 100: // Textmessage deleted
                                    Amino.Objects.DeletedMessage _deletedMessage = JsonSerializer.Deserialize<DeletedMessage>(element);
                                    _deletedMessage.SocketBase = _socketBase;
                                    _deletedMessage.Json = webSocketMessage;
                                    eventCall.callMessageDeletedEvent(client, _deletedMessage);
                                    break;
                                case 101: // ChatMember Joined
                                    Amino.Objects.JoinedChatMember _joinedMember = JsonSerializer.Deserialize<JoinedChatMember>(element);
                                    _joinedMember.Json = webSocketMessage;
                                    _joinedMember.SocketBase = _socketBase;
                                    eventCall.callChatMemberJoinEvent(client, _joinedMember);
                                    break;
                                case 102: // ChatMember Left
                                    Amino.Objects.LeftChatMember _leftMember = JsonSerializer.Deserialize<LeftChatMember>(element);
                                    _leftMember.Json = webSocketMessage;
                                    _leftMember.SocketBase = _socketBase;
                                    eventCall.callChatMemberLeaveEvent(client, _leftMember);
                                    break;
                                case 104: // ChatBackground changed
                                    Amino.Objects.ChatEvent _chatBackgroundChanged = JsonSerializer.Deserialize<ChatEvent>(element);
                                    _chatBackgroundChanged.SocketBase = _socketBase;
                                    _chatBackgroundChanged.Json = webSocketMessage;
                                    eventCall.callChatBackgroundChangedEvent(client, _chatBackgroundChanged);
                                    break;
                                case 105: // ChatTitle changed
                                    Amino.Objects.ChatEvent _chatTitleChanged = JsonSerializer.Deserialize<ChatEvent>(element);
                                    _chatTitleChanged.SocketBase = _socketBase;
                                    _chatTitleChanged.Json = webSocketMessage;
                                    eventCall.callChatTitleChangedEvent(client, _chatTitleChanged);  
                                    break;
                                case 113: // ChatContent Changed
                                    Amino.Objects.ChatEvent _chatContentChanged = JsonSerializer.Deserialize<ChatEvent>(element);
                                    _chatContentChanged.Json = webSocketMessage;
                                    _chatContentChanged.SocketBase = _socketBase;
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
