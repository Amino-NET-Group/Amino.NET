# Amino.Net
An unofficial C# wrapper for Aminos REST API to make Amino Bots and Tools

# What can this wrapper do?
Amino.Net has a lot of functionality that allow you to develop Amino tools and bots for multiple types of C# / dotNet projects

## Extras and Credits
- This C# library has been made possible using [Amino.py](https://github.com/Slimakoi/Amino.py) as it is based on [Slimakoi](https://github.com/Slimakoi/)s work
- Some values or Objects might return `null`, this is because the library pulls its data straight from the Amino REST API, if values in the API return `null` there's nothing i can do!
- Some values / objects might change depending on if the Client is logged in or not!
- Some functions require the Client to be logged in, they will throw an Exception if it is not.
- If you find a bug, an issue or have a recommendation to make, please open an Issue on GitHub about it!

## Important Notice
By using this library you agree that you are aware of the fact that you are breaking the App services Terms of Service - as Team Amino strictly forbids the use of any sort of third party software / scripting to gain an advantage over other members, any activity by third party tools found by Team Amino may result in your account getting banned from their services!

## How to install
You can get Amino.Net straight from [NuGet.org](https://nuget.org) or any NuGet Package manager!

## Quick Links
- Main Documentation and Extra info ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/README.md))
- Client Documentation (Coming soon)
- SubClient Documentation (Coming soon)
- ACM Documentation (Coming soon)
- Helpers Documentation (Coming soon)
- Types (Coming soon)
- Objects (Coming soon)
- Events (Coming soon)
- Exceptions (Coming soon)
- Amino REST API Documentation (Coming soon)


# GENERAL DOCUMENTATION
## Client
The Amino.Client() Object is a crucial object to make Bots or tools, as there need to be an instance of it to make the library work
### Values
Note that some values might be `null` if you don't `login` into an Amino account
- deviceID : string
- sessionID : string
- secret : string
- userID : string
- json : string
- googleID : string
- appleID : string
- facebookID : string
- twitterID : string
- iconUrl : string
- aminoID : string
- email : string
- phoneNumber : string
- nickname : string
- is_Global : bool
- debug : bool (default: false) : If turned to true, all API responses will be printed into the Trace log


### Client(string _deviceId)
You need to create an Object instance of the Amino.Client object to use the Clients functions, it accepts 1 value
### Extras
- Consider using try catch statements, if a function fails while executing the request, the response code doesn't indicate success or if a custom condition is triggered, it will throw an Exception!
### Values:
- _deviceId : string : if left empty, it will generate a valid Amino Device ID

### Example:
```CSharp
Amino.Client client = new Amino.Client(); // This client will be used as an Example Client for the rest of the Amino.Client() docuemntations, whenever "client" is being used, its just an instance of Amino.Client()
```

## Methods / Functions

### request_verify_code(string email, bool resetPassword) : Task
You can request an Amino verification code using this function.
- Success: Task completes successfully
- Error: Throws an exception
### Values:
- email : string : The email address for the Amino account
- resetPassword : bool (default: false) : This decides if the verification code is supposed to reset the accounts password
### Example:
```CSharp
try
{
    client.request_verify_code("myEmail@domain.com", true);
    Console.WriteLine("Requested Verification code!");
} catch
{
    Console.WriteLine("Could not send email");
}
```


### login(string email, string password, string secret) : Task
You can log into an existing Amino account using this function.
- Success: Sets all of the clients values, the Client headers and completes the Task successfully
- Error: Throws an Exception
### Values:
- email : string : The email of the account
- password : string : The password of the account
- secret : string (default: null) : The login secret of the account
### Example:
```CSharp
try 
{
    client.login("myEmail@Domain.com", "myEpicPassword");
    Console.WriteLine("Logged in!");
} catch 
{
    Console.WriteLine("Could not log into the account!");
}
```


### logout() : Task
You can log out of an Amino account using this function, make sure you are logged into an account to use this function!
- Success: Clears the Clients headers, values and completes the Task successfully
- Error: Throws an Exception
### Example:
```CSharp
try 
{
    client.logout();
    Console.WriteLine("Logged out!");
} catch 
{
    Console.WriteLine("Could not log out!");
}
```


### register(string name, string email, string password, string verificationCode, string deviceId) : Task
This function allows you to register an Amino account
- Success: The account will be created and the Task completes Successfully
- Error: Throws an Exception
### Values:
- name : string : The name of the account
- email : string : The email of the account
- password : string : The password of the account
- verificationCode : string : The verification code for the email, refer to `request_verify_code()`
- deviceId : string (default: null) : The device ID of the account, if left empty it will generate one for you
### Example:
```CSharp
try 
{
    client.register("epicName", "myEmail@Domain.com", "myNicePassword", "ABCDEF");
    Console.WriteLine("Account registered!");
} catch 
{
    Console.Writeline("Could not register account!");
}
``` 


### restore_account(string email, string password, string deviceId) : Task
This function allows you to restore a deleted Amino account
- Success: Restores the account and completes the Task successfully
- Error: Throws an Exception
### Values:
- email : string : The email of the account you want to restore
- password : string : The password of the account you want to restore
- deviceId : string (default: null) : The device ID you want to restore the account with, if left empty it will generate one for you
### Example:
```CSharp
try 
{
    client.restore_account("myEmail@Domain.com", "myEpicPassword", "someDeviceId");
    Console.WriteLine("Restored account successfully!");
} catch 
{
    Console.WriteLine("Could not restore account!");
}
```


### delete_account(string password) : Task
This function allows you to delete the current Amino account in use.
- Success: Deletes the current Amino account, clears all headers, stops the webSocket and completes the Task successfully
- Error: Throws an Exception
### Values:
- password : string : The password of the current Amino Account
### Example:
```CSharp
try 
{
    client.delete_account("myEpicPassword");
    Console.WriteLine("Account has been deleted Successfully!");
} catch 
{
    Console.WriteLine("Account could not be deleted!");
}
```


### activate_account(string email, string verificationCode, string deviceId) : Task
This function allows you to activate an Amino account using a verification Code
- Success: Activates the Amino account and completes the Task successfully
- Error: Throws an Exception
### Values:
- email : string : The email address of the account you want to activate
- verificationCode : string : The verification Code to activate the account (refer to `request_verify_code()`)
- deviceId : string (default: null) : The device ID  you want to activate the account from, if left empty it will generate one for you
### Example:
```CSharp
try 
{
    client.activate_account("myEmail@Domain.com", "ABCDEF");
    Console.WriteLine("The account has been activated!")
} catch 
{
    Console.WriteLine("Could not activate the account!");
}
```


### configure_account(Amino.Types.account_gender gender, int age) : Task
This function allows you to configure an Amino accounts age and gender
- Success: Configures the account and completes the Task successfully
- Error: Throws an Exception
### Values:
- gender : Amino.Types.account_gender : The gender you want the account to be configured to
- age : int : Sets the age of the account : This value cannot be lower than 13!
### Example:
```CSharp
try 
{
    client.configure_account(Amino.Types.account_gender.Non_Binary, 18);
    Console.WriteLine("Configured account successfully!");
} catch 
{
    Console.WriteLine("Could not configure account!");
}
```


### change_password(string email, string password, string verificationCode) : Task
This function allows you to change the password of the current Amino account.
- Success: Changes the account password and completes the Task successfully
- Error: Throws an Exception
### Values:
- email : string : The email of your account
- password : string : The new password you want the account to change to
- verificationCode : string : The verification code needed to reset your Password (refer to `request_verify_code()`)
### Example:
```CSharp
try 
{
    client.change_password("myEmail@Domain.com", "myNewPassword", "ABCDEF");
    Console.WriteLine("Account password has been changed successfully!");
} catch 
{
    Console.WriteLine("Could not reset password!");
}
```


### get_user_info(string userId) : Amino.Objects.GlobalProfile
This function allows you to get information about a global Amino Profile
- Success: Gets the users information and returns it as an Object (Amino.Objects.GlobalProfile)
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the Amino user you want to get information about
### Example:
```CSharp
try 
{
    var userProfile = client.get_user_info("anyUserId");
    Console.WriteLine("Account username: " + userProfile.nickname);
} catch 
{
    Console.WriteLine("Could not get user information");
}
```


### check_device(string deviceId) : bool
This function allows you to check if a device ID is valid or not
- Success: Checks if the device ID is valid and returns either True or False
- Error: Throws an Exception
### Values:
- deviceId : string : The device ID you want to check
### Example:
```CSharp
try 
{
    if(client.check_device("someDeviceId")) 
    {
        Console.WriteLine("This device ID is valid!");
    } else 
    {
        Console.WriteLine("This device ID is invalid!");
    }
} catch 
{
    Console.WriteLine("Could not check device ID");
}
```


### get_event_log() : Amino.Objects.EventLog
This function allows you to get information about the current accounts event log!
- Success: Gets the accounts eventLog and returns it as an Object (Amino.Objects.EventLog)
- Error: Throws an Exception
### Values:
- None
### Example:
```CSharp
try 
{
    var eventLog = client.get_event_log();
    Console.WriteLine("EventLog JSON: " + eventLog.json);
} catch 
{
    Console.WriteLine("Could not get eventLog!");
}
``` 


### get_subClient_communities(int start, int size) : List<Amino.Objects.Community>
This function allows you to get information about all the Communities where the current Amino account is in
- Success: Gets all communities of the main Client and returns them as an Object List (List<Amino.Objects.Community>)
- Error: Throws an Exception
- The range between `start` and `size` **cannot** be larger than `100`
### Values:
- start : int (default: 0) : The start index for getting the communities
- size : int (default: 25) : Sets the range between `start` and whatever number this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.Community> communityList = client.get_subClient_communities();
    Console.WriteLine("First community Name: " + communityList[0].communityName);
} catch 
{
    Console.WriteLine("Could not get subClient communities.");
}
```


### get_subClient_profiles(int start, int size) : List<Amino.Objects.CommunityProfile>
This function allows you to get information about all the community profiles where the current Amino account is in
- Success: Gets Community profiles and returns them as an Object List (List<Amino.Objects.CommunityProfile>)
- Error: Throws an Exception
- The range between `start` and `size` **cannot** be larger than `100`
### Values:
- start : int (default: 0) : The start index of the Community profiles
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.CommunityProfile> profileList = client.get_subClient_profiles();
    Console.WriteLine("Profile name in first community: " + profileList[0].nickname);
} catch 
{
    Console.WriteLine("Could not get subClient profiles");
}
```


### get_account_info() : Amino.Objects.UserAccount
This function allows you to get information about the current Amino account
- Success: Gets user information and returns it as an Object (Amino.Objects.UserAccount)
- Error: Throws an Exception
### Values:
- None
### Example:
```CSharp
try 
{
    var accountInfo = client.get_account_info();
    Console.WriteLine("Account was created on " + accountInfo.createdTime);
} catch 
{
    Console.WriteLine("Could not get user information");
}
```


### get_chat_threads(int start, int size) : List<Amino.Obejcts.Chat>
This function allows you to get all chat threads where the current Amino account is in
- Success: Gets all chat threads and returns them as an Object List (List<Amino.Objects.Chat>)
- Error: Throws an Exception
- The range between `start` and `size` **cannot** be larger than `100`
### Values:
- start : int (default: 0) : Sets the Start index for getting chat list
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.Chat> chatList = client.get_chat_threads();
    Console.WriteLine("Nickname of the owner of the first chat: " + chatList[0].Author.nickname);
} catch 
{
    Console.WriteLine("Could not get chats!");
}
```


### get_chat_thread(string chatId) : Amino.Objects.Chat
This function allows you to get information about a specific chat thread where the current Amino account is in
- Success: Gets the chat threads information and returns it as an Object (Amino.Objects.Chat)
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat thread you want the information from
### Example:
```CSharp
try 
{
    var Chat = client.get_chat_thread("myChatId");
    Console.WriteLine("Chat Member Count: " + Chat.membersCount);
} catch 
{
    Console.WriteLine("Could not get chat Thread);
}
```


### get_chat_users(string chatId, int start, int size) : List<Amino.Objects.ChatMember>
This function allows you to get chat member information about a specific chat thread
- Success: Gets the member information and returns it as an Object List (List<Amino.Objects.ChatMember>)
- Error: Throws an Exception
- The range between `start` and `size` **cannot** be larger than `100`
### Values:
- chatId : string : The object / chat ID of the chat thread
- start : int (default: 0) : Sets the Start index for getting chat list
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.ChatMember> chatMemberList = client.get_chat_users("myChatId");
    Console.WriteLine("Name of the first chat member: " + chatMemberList[0].nickname);
} catch 
{
    Console.WriteLine("Could not get chat users");
}
```


### join_chat(string chatId) : Task
This function allows you to join a chat thread using the current Amino account.
- Success: Joins the chat thread and completes the Task Successfully
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat thread you want to join
### Example:
```CSharp
try 
{
    client.join_chat("myChatId");
    Console.WriteLine("Joined chat");
} catch 
{
    Console.WriteLine("Could not join chat!");
}
```


### leave_chat(string chatId) : Task
This function allows you to leave a chat thread using the current Amino account.
- Success: Leaves the chat and completes the Task successfully
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat thread you want to leave
### Example:
```CSharp
try 
{
    client.leave_chat("myChatId");
    Console.WriteLine("Left chat");
} catch 
{
    Console.WriteLine("Could not leave chat!");
}
```


### invite_to_chat(stiring[] userIds, string chatId) : Task
This function allows you to invite one or more members to a chat thread with the current Amino account
- Success: Invites the members to the chat thread and completes the Task successfully
- Error: Throws an Exception
### Values:
- userIds : string[] : A string array of the user IDs that you want to invite
- chatId : string : The object / chat ID that you want to invite the users into
### Example:
```CSharp
try 
{
    string[] users = new string[] { "userId_1", "userId_2" };
    client.invite_to_chat(users, "chatId");
    Console.WriteLine("Invited users!");
} catch 
{
    Console.WriteLine("Could not invite members to chat");
}
```


### kick_from_chat(string userId, string chatId, bool allowRejoin) : Task 
This function allows you to kick a user from a chat thread
- Success: Kicks the user from the chat tread and completes the Task successfully7
- Error: Throws an Exception
### Values:
- userId : string : The userId of the user you want to kick
- chatId : string : The object / chat ID of the chat thread you want to kick the user from
- allowRejoin : bool (default: true) : Decides if the user is allowed to rejoin the chat thread or not
### Example:
```CSharp
try 
{
    client.kick_from_chat("userId", "chatId", false);
    Console.WriteLine("User has been kicked from chat!");
} catch 
{
    Console.WriteLine("Could not kick member from chat!");
}
```


### get_chat_messages(string chatId, int size, string pageToken) : List<Amino.Objects.MessageCollection>
This function allows you to get a collection of messages in a specific chat thread the current Amino account is in
- Success: Gets the chat messages and returns them as an Object List (List<Amino.Objects.MessageCollection>)
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat thread that you want the messages from
- size : int (default: 25) : The amount of messages you want to get
- pageToken : string (default: null) : The page Token of the messages
### Example:
```CSharp
try 
{
    List<Amino.Obejcts.MessageCollection> messageList = client.get_chat_messages("someChatId", 50);
    Console.WriteLine("Nickname of the author of the first message: " + messageList[0].Author.nickname);
} catch 
{
    Console.WriteLine("Could not get chat messages!");
}
```


### search_community(string aminoId) : List<Amino.Objects.CommunityInfo>
This function allows you to search for a Community by its aminoId (**not** and ObjectId) and retrive information about it
- Success: Searches the community and returns it as an Object List (List<Amino.Objects.CommunityInfo>)
- Error: Throws an Exception
### Values:
- aminoId : string : The aminoId that you want to look up
### Example:
```CSharp
try 
{
    List<Amino.Objects.CommunityInfo> communityInfo = client.search_community("myLookupTerm");
    Console.WriteLine("Name of the first community result: " + communityInfo[0].name);
} catch 
{
    Console.WriteLine("Could not search for community");
}
```


### get_user_following(string userId, int start, int size) : List<Amino.Objects.UserFollowings>
This function allows you to get the users a target is following
- Success: Gets the user followings and returns the data as an Object List (List<Amino.Objects.UserFollowings>)
- Error: Throws an Exception
### Values:
- userId : string : The object / user Id of a target user
- start : int (default: 0) : Sets the Start index for getting chat list
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.UserFollowings> userFollowings = client.get_user_followings("someUserId");
    Console.WriteLine("Name of the first user following: " + userFollowings[0].nickname);
} catch 
{
    Console.WriteLine("Could not get user followings");
}
```


### get_user_followers(string userId, int start, int size) : List<Amino.Objects.UserFollowings>
This function allows you to get a list of users that follow a user
- Success: Gets the followers and returns them as an Object List (List<Amino.Objects.UserFollowings>)
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user you want to get the followers of
- start : int (default: 0) : Sets the Start index for getting chat list
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.UserFollowings> userFollowers = client.get_user_followers("someUserId");
    Console.WriteLine("Name of the first follower: " + userFollowers[0].nickname);
} catch 
{
    Console.WriteLine("Could not get user followers");
}
``` 
<button onClick="testFunction()">test</button>


<script>
    function testFunction() {
        alert("Test");
    }
</script>
