# Amino.Net Events
## This is a seperated documentation file written for Amino.Net's Events!
- This file might not always be up to date right away
- This file contains every Amino.Net Event nicely wrapped up
- To see the full general documentation consider reading Readme.md ([Click here](https://github.com/FabioGaming/Amino.NET))


<details>
<summary id="functionName">Amino.Net Events</summary>

- This library features a number of events that you can subscribe to!
- All events run on an Amino.Client() instance!
- All events will return either a value or an Object.
</details>

<details>
<summary id="functionName">onMessage</summary>
<p id="functionDescription">This event fires each time the Client receives a Text message</p>

### Event:
- This event returns an Amino.Objects.Message object
### Example:
```CSharp
static void onMessageEvent(Amino.Objects.Message message) 
{
    Console.WriteLine($"User {message.Author.userName} has sent a message: {message.content} in chat: {message.chatId}");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onMessage += onMessageEvent;
}
```

### Returns:
- Amino.Objects.Message
</details>

<details>
<summary id="functionName">onImageMessage</summary>
<p id="functionDescription">This event fires each time the Client receives an Image message</p>

### Event:
- This event returns an Amino.Objects.ImageMessage Object
### Example:
```CSharp
static void onImageMessageEvent(Amino.Objects.ImageMessage imageMessage) 
{
    Console.WriteLine($"User {imageMessage.Author.nickname} has sent an image: {imageMessage.mediaUrl}");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onImageMessage += onImageMessageEvent;
}
```

### Returns:
- Amino.Objects.ImageMessage
</details>

<details>
<summary id="functionName">onWebSocketMessage</summary>
<p id="functionDescription">This event fires each time a websocket message has been recevied by the Client</p>

### Event:
- This event returns a string, that being the raw (probably JSON) websocket message
### Example:
```CSharp
static void onWebSocketMessageEvent(string socketMessage) 
{
    Console.WriteLine("Recevied websocket message: " + socketMessage);
}


[...]

static void main(string[] args) 
{
    [...]
    client.onWebSocketMessage += onWebSocketMessageEvent;
}
```

### Returns:
- string
</details>

<details>
<summary id="functionName">onYouTubeMessage</summary>
<p id="functionDescription">This event fires each time a YouTube message has been received by the Client</p>

### Event:
- This event returns an Amino.Objects.YouTubeMessage Object
### Example:
```CSharp
static void onYouTubeMessageEvent(Amino.Objects.YouTubeMessage youtubeMessage) 
{
    Console.WriteLine("Video title of the received Video: " + youtubeMessage.videoTitle);
}


[...]

static void main(string[] args) 
{
    [...]
    client.onYouTubeMessage += onYouTubeMessageEvent;
}
```

### Returns:
- Amino.Objects.YouTubeMessage
</details>

<details>
<summary id="functionName">onVoiceMessage</summary>
<p id="functionDescription">This event fires each time a Voice message / note is received by the Client</p>

### Event:
- This event returns an Amino.Objects.VoiceMessage Object
### Example:
```CSharp
static void onVoiceMessageEvent(Amino.Objects.VoiceMessage voiceMessage) 
{
    Console.WriteLine("URL to the audio file: " + voiceMessage.mediaValue);
    Console.WriteLine("Duration of the voice message: " + voiceMessage.Extensions.duration);
}


[...]

static void main(string[] args) 
{
    [...]
    client.onVoiceMessage += onVoiceMessageEvent;
}
```

### Returns:
- Amino.Objects.VoiceMessage
</details>

<details>
<summary id="functionName">onStickerMessage</summary>
<p id="functionDescription">This event fires each time an Amino sticker message has been received by the Client</p>

### Event:
- This event returns an Amino.Objects.StickerMessage Object
### Example:
```CSharp
static void onStickerMessageEvent(Amino.Objects.StickerMessage stickerMessage)
{
    Console.WriteLine("Sticker ID: " + stickerMessage.Sticker.stickerId);
}


[...]

static void main(string[] args) 
{
    [...]
    client.onStickerMessage += onStickerMessageEvent;
}
```

### Returns:
- Amino.Objects.StickerMessage
</details>


<details>
<summary id="functionName">onMessageDeleted</summary>
<p id="functionDescription">This event fires each time an Amino message has been deleted in any chat where the current Amino account is in</p>

### Event:
- This even returns an Amino.Objects.DeletedMessage Object
### Example:
```CSharp
static void onDeletedMessageEvent(Amino.Objects.DeletedMessage deletedMessage)
{
    Console.WriteLine($"User: {deletedMessage.Author.username}({deletedMessage.Author.userId}) has deleted a message in chat: {deletedMessage.chatId}");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onMessageDeleted += onStickerMessageEvent;
}
```

### Returns:
- Amino.Objects.DeletedMessage
</details>


<details>
<summary id="functionName">onChatMemberJoin</summary>
<p id="functionDescription">This event fires each time an Amino user has joined a chat thread where the current Amino account is in</p>

### Event:
- This event returns an Amino.Obejcts.JoinedChatMember Object

### Example:
```CSharp
static void onUserChatJoinEvent(Amino.Objects.JoinedChatMember joinedMember)
{
    Console.WriteLine($"User: {joinedMember.Author.nickname} joined chat {joinedMember.chatId}");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatMemberJoin += onUserChatJoinEvent;
}
```

### Returns:
- Amino.Objects.JoinedChatMember
</details>


<details>
<summary id="functionName">onChatMemberLeave</summary>
<p id="functionDescription">This even fires each time an Amino user has left a chat where the current Amino account is in</p>

### Event:
- This event returns an Amino.Objects.LeftChatMember Object
### Example:
```CSharp
static void onUserChatLeaveEvent(Amino.Objects.LeftChatMember leftMember)
{
    Console.WriteLine($"User: {leftMember.userId} left chat {leftMember.chatId}");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatMemberLeave += onUserChatLeaveEvent;
}
```

### Returns:
- Amino.Objects.LeftChatMember
</details>


<details>
<summary id="functionName">onChatBackgroundChanged</summary>
<p id="functionDescription">This event fires each time an Amino chat thread background has been changed (only chats where the current Amino account is in)</p>

### Event:
- This event returns an Amino.Objects.ChatEvent Object

### Example:
```CSharp
static void onChatBackgroundChangedEvent(Amino.Objects.ChatEvent chatEvent)
{
    Console.WriteLine($"Background in Chat thread {chatEvent.chatId} has changed.");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatBackgroundChanged += onChatBackgroundChangedEvent;
}
```

### Returns:
- Amino.Objects.ChatEvent
</details>


<details>
<summary id="functionName">onChatTitleChanged</summary>
<p id="functionDescription">This event fires each time an Amino chat thread Title has been changed (only for chats where the current Amino account is in)</p>

### Event:
- This event returns an Amino.Objects.ChatEvent Object
### Example:
```CSharp
static void onChatTitleChangedEvent(Amino.Objects.ChatEvent chatEvent)
{
    Console.WriteLine($"Title of Chat thread {chatEvent.chatId} has changed.");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatTitleChanged += onChatTitleChangedEvent;
}
```

### Returns:
- Amino.Objects.ChatEvent
</details>


<details>
<summary id="functionName">onChatContentChanged</summary>
<p id="functionDescription">This event fires each time an Amino chat thread Content (Description) has been changed (only for chats where the current Amino account is in)</p>

### Event:
- This event returns an Amino.Objects.ChatEvent Object
### Example:
```CSharp
static void onChatContentChangedEvent(Amino.Objects.ChatEvent chatEvent)
{
    Console.WriteLine($"Content of Chat thread {chatEvent.chatId} has changed.");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatContentChanged += onChatContentChangedEvent;
}
```

### Returns:
- Amino.Objects.ChatEvent
</details>



<details>
<summary id="functionName">onChatAnnouncementPin</summary>
<p id="functionDescription">This event fires each time an Amino chat Announcement has been pinned /    changed (only for chats where the current Amino account is in)</p>

### Event:
- This event returns an Amino.Objects.ChatAnnouncement Object
### Example:
```CSharp
static void onChatAnnouncementChangedEvent(Amino.Objects.ChatAnnouncement chatAnnouncement)
{
    Console.WriteLine($"Chat Announcement of Chat thread {chatAnnouncement.chatId} has changed to {chatAnnouncement.content}.");
}


[...]

static void main(string[] args) 
{
    [...]
    client.onChatAnnouncementPin += onChatAnnouncementChangedEvent;
}
```

### Returns:
- Amino.Objects.ChatAnnouncement
</details>


<!--- JUST A TEMPLATE

<details>
<summary id="functionName"></summary>
<p id="functionDescription"></p>

### Event:

### Example:
```CSharp
```

### Returns:
</details>
--->


<!---

<style>
#functionName {
    font-size:15px;
    font-weight: bold;
}
#functionDescription {
    font-style: italic
}

summary {
    padding: 8px;
    cursor: pointer;
}

</style> 

--->