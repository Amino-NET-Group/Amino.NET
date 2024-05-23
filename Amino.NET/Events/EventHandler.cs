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
                                    Amino.Objects.SpecialChatEvent _messageForceRemovedByAdmin = JsonSerializer.Deserialize<SpecialChatEvent>(element);
                                    _messageForceRemovedByAdmin.Json = webSocketMessage;
                                    _messageForceRemovedByAdmin.SocketBase = _socketBase;
                                    eventCall.callMessageForceRemovedByAdminEvent(client, _messageForceRemovedByAdmin);
                                    break;
                                case 120: // ChatTip
                                    Amino.Objects.ChatTip _chatTip = JsonSerializer.Deserialize<ChatTip>(element);
                                    _chatTip.Json = webSocketMessage;
                                    _chatTip.SocketBase = _socketBase;
                                    eventCall.callChatTipEvent(client, _chatTip);
                                    break;
                                case 121: // ChatAnnouncementPin
                                    Amino.Objects.ChatAnnouncement _chatAnnouncementPinEvent = JsonSerializer.Deserialize<ChatAnnouncement>(element);
                                    _chatAnnouncementPinEvent.Json = webSocketMessage;
                                    _chatAnnouncementPinEvent.SocketBase = _socketBase;
                                    eventCall.callChatPinAnnouncementEvent(client, _chatAnnouncementPinEvent);
                                    break;
                                case 125: // ChatViewModeOn
                                    Amino.Objects.ViewMode _viewModeOn = JsonSerializer.Deserialize<ViewMode>(element);
                                    _viewModeOn.Json = webSocketMessage;
                                    _viewModeOn.SocketBase = _socketBase;
                                    eventCall.callChatViewModeOnEvent(client, _viewModeOn);
                                    break;
                                case 126: // ChatViewModeOff
                                    Amino.Objects.ViewMode _viewModeOff = JsonSerializer.Deserialize<ViewMode>(element);
                                    _viewModeOff.Json = webSocketMessage;
                                    _viewModeOff.SocketBase = _socketBase;
                                    eventCall.callChatViewModeOffEvent(client, _viewModeOff);
                                    break;
                                case 127: // ChatAnnouncementUnPin
                                    Amino.Objects.ChatEvent _chatAnnouncementUnPin = JsonSerializer.Deserialize<ChatEvent>(element);
                                    _chatAnnouncementUnPin.Json = webSocketMessage;
                                    _chatAnnouncementUnPin.SocketBase = _socketBase;
                                    eventCall.callChatUnpinAnnouncementEvent(client, _chatAnnouncementUnPin);
                                    break;
                                case 128: // ChatTipEnabled
                                    Amino.Objects.ChatTipToggle _chatTipEnabled = JsonSerializer.Deserialize<ChatTipToggle>(element);
                                    _chatTipEnabled.Json = webSocketMessage;
                                    _chatTipEnabled.SocketBase = _socketBase;
                                    eventCall.callChatTipEnabledEvent(client, _chatTipEnabled);
                                    break;
                                case 129: // ChatTipDisabled
                                    Amino.Objects.ChatTipToggle _chatTipDisabled = JsonSerializer.Deserialize<ChatTipToggle>(element);
                                    _chatTipDisabled.Json = webSocketMessage;
                                    _chatTipDisabled.SocketBase = _socketBase;
                                    eventCall.callChatTipDisabledEvent(client, _chatTipDisabled);
                                    break;
                                
                            }

                            break;
                        case 100: //ImageMessage
                            Amino.Objects.ImageMessage _imageMessage = JsonSerializer.Deserialize<ImageMessage>(element);
                            _imageMessage.Json = webSocketMessage;
                            _imageMessage.SocketBase = _socketBase;
                            eventCall.callImageMessageEvent(client, _imageMessage);
                            break;
                        case 103: //YouTubeMessage
                            Amino.Objects.YouTubeMessage _youtubeMessage = JsonSerializer.Deserialize<YouTubeMessage>(element);
                            _youtubeMessage.Json = webSocketMessage;
                            _youtubeMessage.SocketBase = _socketBase;
                            eventCall.callYouTubeMessageEvent(client, _youtubeMessage);
                            break;
                        case 110: //VoiceMessage
                            Amino.Objects.VoiceMessage _voiceMessage = JsonSerializer.Deserialize<VoiceMessage>(element);
                            _voiceMessage.Json = webSocketMessage;
                            _voiceMessage.SocketBase = _socketBase;
                            eventCall.callVoiceMessageEvent(client, _voiceMessage);
                            break;
                        case 113: //StickerMessage
                            Amino.Objects.StickerMessage _stickerMessage = JsonSerializer.Deserialize<StickerMessage>(element);
                            _stickerMessage.Json = webSocketMessage;
                            _stickerMessage.SocketBase = _socketBase;
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
