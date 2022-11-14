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