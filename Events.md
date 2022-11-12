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