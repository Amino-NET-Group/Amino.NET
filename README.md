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
- Please note that this library is built for an easy and dynamic start into making Amino bots and tools, it is **not** made for spam bots or any sort of harmful tools, so use it the way it's intended for.
- This is a non profit project, however, if you do want to support me and my **general** work, you can refer to the GitHub sponsor options linked to this repository
- I will not take any responsibilty for harm being done using this library, as this is only a free and **open** library, therefore I can't prevent any harm!
- If you run into issues or want to talk to other Amino.Net developers, you can check out the official Amino.Net discord server [HERE](https://discord.gg/gNnBnADQkz)

## Important Notice
By using this library you agree that you are aware of the fact that you are breaking the App services Terms of Service - as Team Amino strictly forbids the use of any sort of third party software / scripting to gain an advantage over other members, any activity by third party tools found by Team Amino may result in your account getting banned from their services!

## How to install
You can get Amino.Net straight from [NuGet.org](https://nuget.org) or any NuGet Package manager!

## Quick Links
- Main Documentation and Extra info ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/README.md))
- Client Documentation ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/Client.md))
- SubClient Documentation (Coming soon)
- ACM Documentation (Coming soon)
- Helpers Documentation ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/Helpers.md))
- Types (Coming soon)
- Objects (Coming soon)
- Events ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/Events.md))
- Exceptions & Troubleshooting ([Click Here](https://github.com/FabioGaming/Amino.NET/blob/master/Exceptions.md))
- Amino REST API Documentation (Coming soon)
- Official Amino.Net Discord Server ([Click Here](https://discord.gg/qyv8P2gegK))

# GENERAL DOCUMENTATION
### To see a better view on the Documentation consider viewing the seperated documentation files linked in `Quick Links`
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
    Console.WriteLine("Could not get chat Thread");
}
```


### get_chat_users(string chatId, int start, int size) : List<Amino.Objects.ChatMember>
This function allows you to get chat member information about a specific chat thread
- Success: Gets the member information and returns it as an Object List (List<Amino.Objects.ChatMember>)
- Error: Throws an Exception
- The range between `start` and `size` **cannot** be larger than `100`
### Values:
- chatId : string : The object / chat ID of the chat thread
- start : int (default: 0) : Sets the Start index for getting chat users
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
This function allows you to search for a Community by its aminoId (**not** and ObjectId) and retrieve information about it
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
- start : int (default: 0) : Sets the Start index for getting user followings
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
- start : int (default: 0) : Sets the Start index for getting user followers
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


### get_user_visitors(string userId, int start, int size) : List<Amino.Objects.UserVisitor>
This function allows you to get a list of users that have visited a target profile
- Success: Gets all user visitors and returns them as an Object List (List<Amino.Objects.UserVisitor>)
- Error: Throws an Exception
### Values:
- userId : string : The target users object / user ID that you want to get the visitors of
- start : int (default: 0) : Sets the Start index for getting user visitors
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example: 
```CSharp
try 
{
    List<Amino.Objects.UserVisitor> userVisitors = client.get_user_visitors("someUserId");
    Console.WriteLine("Name list of all visitors:");
    foreach(Amino.Objects.UserVisitor visitor in userVisitors)
    {
        Console.WriteLine(visitor.Profile.nickname);
    }

} catch 
{
    Console.WriteLine("Could not get user visitors!");
}
```


### get_blocked_users(int start, int size) : List<Amino.Objects.BlockedUser>
This function allows you to get a list of users that the current Amino account has blocked
- Success: Gets the blocked users and returns them as an Object List (List<Amino.Objects.BlockedUser>)
- Error: Throws an Exception
### Values:
- start : int (default: 0) : Sets the Start index for getting blocked users
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.BlockedUser> blockedUsers = client.get_blocked_users();
    Console.WriteLine("List of blocked users (user IDs)");
    foreach(Amino.Obejcts.BlockedUser user in blockedUsers) 
    {
        Console.WriteLine(user.userId);
    }
} catch 
{
    Console.WriteLine("Could not get blocked users");
}
```


### get_blocker_users(int start, int size) : List<<string>string>
This function allows you to get a list of user IDs of the users who have currenty blocked the current Amino account
- Success: Gets all blocker user IDs and returns them as a string list
- Error: Throws an Exception
### values:
- start : int (default: 0) : Sets the Start index for getting blocker users
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<string> blockerUsers = client.get_blocker_users();
    if(blockerUsers.Count > 0) 
    {
        Console.WriteLine("First blocker user: " + blockerUsers[0]);
    }
} catch 
{
    Console.WriteLine("Could not get blocker users");
}
```


### get_wall_comments(string userId, Amino.Types.Sorting_Types type, int start, int size) : List<Amino.Objects.Comment>
This function allows you to get a list of comments that have been left on a users wall
- Success: Gets a users wall comments and returns them as an Object List (List<Amino.Objects.Comment>)
- Error: Throws an Exception
### values:
- userId : string : The object / user ID of the user you want to get the wall comments from
- type : Amino.Types.Sorting_Types : The type of sorting you want to apply to the comment filter
- start : int (default: 0) : Sets the Start index for getting wall comments
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Obejcts.Comment> wallComments = client.get_wall_comments("someUserId", Types.Sorting_Types.Newest);
    Console.WriteLine("First wall comment content: " + wallComments[0].content);
} catch 
{
    Console.WriteLine("Could not get wall comments");
}
```


### flag(string reason, Amino.Types.Flag_Types flagType, Amino.Types.Flag_Targets targetType, string targetId, bool asGuest) : Task
This function allows you to flag a post / profile on Amino
- Success: Flags the target and completes the Task successfully
- Error: Throws an Exception
### Values:
- reason : string : The reason you are flagging the target
- flagType : Amino.Types.Flag_Types : The type of flagging that is being done
- targetType : Amino.Types.Flag_Targets : The type of the target that is being flagged
- targetId : string : The object / user ID of the target that you want to flag
- asGuest : bool : This value decides if you want to flag the content as a guest or as a logged in Amino user
### Example:
```CSharp
try 
{
    client.flag("spamming posts", Amino.Types.Flag_Types.Spam, Amino.Types.Flag_Targets.User, false);
    Console.WriteLine("Flagged content!");
} catch 
{
    Console.WriteLine("Could not flag content");
}
```


### delete_message(string chatId, string messageId, bool asStaff, string reason) : Task
This function allows you to delete a specific chat message using the current Amino account
- Success: Deletes the target message and completes the task successfully
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID where the message has been sent in
- messageId : string : The object / message ID of the message that you want to delete
- asStaff : bool (default: false) : This value decides if you're deleting the message as a staff membber
- reason : string (default: null) : The reason provided if the message is being deleted as a staff member
### Example:
```CSharp
try 
{
    client.delete_message("someChatId", "someMessageId", true, "spam content");
    Console.WriteLine("Message deleted!");
} catch 
{
    Console.WriteLine("Could not delete message!");
}
```  


### mark_as_read(string chatId, string messageId) : Task
This function allows you to mark a message as read
- Success: Marks the message as read and completes the task successfully
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat where the message has been sent in
- messageId : string : The object / message ID that you want to mark as read
### Example:
```CSharp
try 
{
    client.mark_as_read("someChatId", "someMessageId");
    Console.WriteLine("Marked message as read");
} catch 
{
    Console.WriteLine("Could not mark message as read");
}
```


### visit(string userId) : Task
This function allows you to visit a users Amino profile
- Success: Visits the targets profile and completes the Task successfully
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user that you want to visit
### Example:
```CSharp
try 
{
    client.visit("someUserId");
    Console.WriteLine("Visited profile");
} catch 
{
    Console.WriteLine("Could not visit user!");
}
```


### follow_user(string userId) : Task
This function allows you to follow a user using the current Amino account
- Success: Follows the user and completes the task successfully
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user you want to follow
### Example:
```CSharp
try 
{
    client.follow("someUserId");
    Console.WriteLine("Followed user");
} catch 
{
    Console.WriteLine("Could not follow user");
}
```


### unfollow_user(string userId) : Task
This function allows you to unfollow a user using the current Amino account
- Success: Unfollows the user and completes the task successfully
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user you want to unfollow
### Example:
```CSharp
try 
{
    client.unfollow_user("someUserId");
    Console.WriteLine("Unfollowed user");
} catch 
{
    Console.WriteLine("Could not unfollow user");
}
```


### block_user(string userId) : Task
This function allows you to block a user using the current Amino account
- Success: Blocks the user and completes the task successfully
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user you want to block
### Example:
```CSharp
try 
{
    client.block_user("someUserId");
    Console.WriteLine("Blocked user");
} catch 
{
    Console.WriteLine("Could not block user");
}
```


### unblock_user(string userId) : Task
This function allows you to unblock a user using the current Amino account
- Success: Unblocks the user and completes the task successfully
- Error: Throws an Exception
### Values:
- userId : string : The object / user ID of the user you want to unblock
### Example:
```CSharp
try 
{
    client.unblock_user("someUserId");
    Console.WriteLine("Unblocked user");
} catch 
{
    Console.WriteLine("Could not unblock user");
}
```


### join_community(string communityId, string invitationCode) : Task
This function allows you to join a community using the current Amino account
- Success: Joins the community and completes the task successfully
- Error: Throws an Exception
### Values:
- communityId : string : The ID of the community that you want to join
- invitationCode : string (default: null) : The invitation code of the community (if there is one)
### Example:
```CSharp
try 
{
    client.join_community("123456");
    Console.WriteLine("Joined community");
} catch 
{
    Console.WriteLine("Could not join community");
}
```


### join_community_request(string communityId, string message) : Task
This function allows you to make a join request to a community
- Success: Requests to join the community and completes the task successfully
- Error: Throws an Exception
### Values:
- communityId : string : The community ID of the community that you want to request to join in
- message : string (default: null) : The message you want to state as reason on why you want to join
### Example:
```CSharp
try 
{
    client.join_community_request("123456", "I like foxes.");
    Console.WriteLine("Requested to join community");
} catch 
{
    Console.WriteLine("Could not request to join the community");
}
```


### leave_community(string communityId) : Task
This function allows you to leave a comunity using the current Amino account
- Success: Leaves the community and completes the Task successfully
- Error: Throws an Exception
### Values:
- communityId : string : The community ID of the community that you want to leave
### Example:
```CSharp
try 
{
    client.leave_community("123456");
    Console.WriteLine("Left community");
} catch 
{
    Console.WriteLine("Could not leave community");
}
```


### flag_community(string communityId, string reason, Amino.Types.Flag_Types flagType, bool asGuest) : Task
This function allows you to flag a community
- Success: Flags the community and completes the task successfully
- Error: Throws an Exception
### Values:
- communityId : string : The community ID of the community you want to flag
- reason : string : The reason why you want to flag the community
- flagType : Amino.Types.Flag_Types : The Type of flagging you want to do
- asGuest : bool (default: false) : This value decides if you want to flag the community as a guest or with an Amino account
### Example:
```CSharp
try 
{
    client.flag_community("123456", "No foxes", Amino.Types.Flag_Types.Trolling, true);
    Console.WriteLine("Flagged community");
} catch 
{
    Console.WriteLine("Could not flag community");
}
```


### upload_media(byte[] file | string filePath, Amino.Types.Upload_File_Types type) : string
This function allows you to upload media directly to the Amino servers, it will return the resulting media URL.
- Success: Uploads the media to the Amino servers and returns the resulting media URL
- Error: Throws an Exception
- This function can be used in multiple ways and have the same output, choose between byte[] for the direct file or string for filepath!
### Values:
- file : byte[] : The bytes of the file that you want to upload
- filePath : string : The path to the file that you want to upload
- type : Amino.Types.Upload_File_Types : The type of media that you want to upload
### Example:
```CSharp
try 
{
    //Uploading media using the file bytes
    byte[] fileBytes = File.ReadAllBytes("Path_To_File");
    string uploaded_with_bytes = client.upload_media(fileBytes, Types.upload_File_Types.Image);

    //Uploading media using file path
    string uploaded_with_path = client.upload_media("Path_To_File", Types.upload_File_Types.Image);

} catch 
{
    Console.WriteLine("Could not upload media");
}
```


### edit_profile(string nickname, string content, byte[] icon, string backgroundColor, string backgroundMediaUrl, string defaultChatbubbleId) : Task
This function allows you to edit your global Amino profile
- Success: Edits the profile and completes the task successfully 
- Error: Throws an Exception
### Values:
- nickname : string (default: null) : The nickname you want the account to have
- content : string (default: null) : The content of the accounts description you want the account to have
- icon : byte[] (default: null) : The icon you want the account to have as profile picture
- backgroundColor : string (default: null) : The background color of the account as HEX format
- backgroundMediaUrl : string (default: null) : The backkground media you want the account to have
- defaultChatbubbleId : string (default: null) : The default chat bubble ID you want the account to have
### Example:
```CSharp
try 
{
    client.edit_profile("I hated making this one.", "it sucked and probably doesn't even work.", backgroundColor: "#FFFFFF");
    Console.WriteLine("Edited profile");
} catch 
{
    Console.WriteLine("Could not edit profile");
}
```


### set_privacy_status(bool isAnonymous, bool getNotifications) : Task
This function allows you to set the privacy status of the current Amino account
- Success: Sets the privacy status and completes the task successfully
- Error: Throws an Exception
### Values:
- isAnonymous : bool (default: false) : Decides if the account is anonymous
- getNotifications : bool (default: true) : Decides if you get notifications or not
### Example:
```CSharp
try 
{
    client.set_privacy_status(true, false);
    Console.WriteLine("Set privacy status");
} catch 
{
    Console.WriteLine("Could not set privacy status");
}
```


### set_amino_id(string aminoId) : Task
This function allows you to change your Amino ID, note that you can't do this an unlimited amount of times
- Success: Changes the accounts Amino ID and completes the Task successfully
- Error: Throws an Exception
### Values:
- aminoId : string : The Amino ID that you want to assign for the account
### Example:
```CSharp
try 
{
    client.set_amino_id("someAminoID");
    Console.WriteLine("Set Amino ID");
} catch 
{
    Console.WriteLine("Could not set Amino ID");
}
```


### add_linked_community(int communityId) : Task
This function allows you to add a linked community to the profile of the current Amino account
- Success: Adds the community and completes the Task successfully
- Error: Throws an Exception
### Values:
- communityId : int : The ID of the community that you want to add
### Example:
```CSharp
try 
{
    client.add_linked_community(123456);
    Console.WriteLine("Added linked community");
} catch 
{
    Console.WriteLine("Could not add linked community");
}
```


### remove_linked_community(int communityId) : Task
This function allows you to remove a linked community from the profile of the current Amino account
- Success: Removes the linked community and completes the Task successfully
- Error: Throws an Exception
### Values:
- communityId : int : The ID of the community that you want to remove
### Example:
```CSharp
try 
{
    client.remove_linked_community(123456);
    Console.WriteLine("Removed linked community");
} catch 
{
    Console.WriteLine("Could not remove linked community");
}
``` 


### comment(string message, Amino.Types.Comment_Types type, objectId) : Task
This function allows you to comment below a post, a wall or reply to a comment using the current Amino account
- Success: Executes the comment and completes the Task succeessfully
- Error: Throws an Exception
### Values:
- message : string : The content of the comment that you want to post
- type : Amino.Types.Comment_Types : The type of comment you want to post
- objectId : string : The object ID of the target you want to comment under / reply to
### Example:
```CSharp
try 
{
    client.comment("Nice post. Sadly it's not about Foxes.", Amino.Types.Comment_Types.Blog, "somePostId");
    Console.WriteLine("Comment posted");
} catch 
{
    Console.WriteLine("Could not comment");
}
```


### delete_comment(string commentId, Amino.Types.Comment_Types type, string objectId) : Task
This function allows you to delete a comment from a post, a users wall or a reply using the current Amino account
- Success: Deletes the target comment and completes the Task successfully
- Error: Throws an Exception
### Values:
- commentId : string : The object ID of the comment that you want to delete
- type : Amino.Types.Comment_Types : The type of comment you're targetting
- objectId : string : The object ID where the target comment has been commented on
### Example:
```CSharp
try 
{
    client.delete_comment("someCommentId", Amino.Types.Comment_Types.Blog, "someBlogPostId");
    Console.WriteLine("Deleted comment");
} catch 
{
    Console.WriteLine("Could not delete comment");
}
```


### like_post(string objectId, Amino.Types.Post_Types type) : Task
This function allows you to like a post using the current Amino account
- Success: Likes the post and completes the task successfully
- Error: Throws an Exception
### Values:
- objectId : string : The ID of the post you want to like
- type : Amino.Types.Post_Types : The type of post that you want to like
### Example:
```CSharp
try 
{
    client.like_post("somePostID", Amino.Types.Post_Types.Blog);
    Console.WriteLine("Liked post");
} catch 
{
    Console.WriteLiine("Could not like post");
}
```


### unlike_post(string objectId, Amino.Types.Post_Types type) : Task
This function allows you to unlike a post using the current Amino account
- Success: Unlikes the post and completes the task successfully
- Error: Throws an Exception
### Values:
- objectId : string : The object ID of the post that you want to unlike
- type : Amino.Types.Post_Types : The type of post that you want to unlike
### Example:
```CSharp
try 
{
    client.unlike_post("somePostId", Amino.Types.Post_Types.Blog);
    Console.WriteLine("Unlikes post");
} catch 
{
    Console.WriteLine("Could not unlike post");
}
```


### get_membership_info() : Amino.Objects.MembershipInfo
This function allows you to get information about the current Amino accounts Amino+ membership
- Success: Gets the membership information and returns it as an Object (Amino.Objects.MembershipInfo)
- Error: Throws an Exception
### Values:
- None
### Example:
```CSharp
try 
{
    var membershipInfo = client.get_membership_info();
    Console.WriteLine("The membership will expire on: " + membershipInfo.Membership.expiredTime);
} catch 
{
    Console.WriteLine("Could not get membership info");
}
```


### get_ta_announcements(Amino.Types.Supported_Languages language, int start, int size) : List<Amino.Objects.Post>
This function allows you to get a list of Team Amino announcements
- Success: Gets the Team Amino announcements and returns them as an Object List (List<Amino.Objects.Post>)
- Error: Throws an Exception
### Values:
- language : Amino.Types.Supported_Languages (default: Amino.Types.Supportedd_Languages.english)
- start : int (default: 0) : Sets the Start index for getting the announcement posts
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.Post> announcementList = client.get_ta_announcements();
    Console.WriteLine("Title of the first post: " + announcementList[0].title);
} catch 
{
    Console.WriteLine("Could not get Team Amino announcement posts!");
}
```


### get_wallet_info() : Amino.Objects.WalletInfo
This function allows you to get the wallet info of the current Amino account
- Success: Gets the wallet info and returns it as an Object (Amino.Objects.WalletInfo)
- Error: Throws an Exception
### Values:
- None
### Example:
```CSharp
try 
{
    var walletInfo = client.get_wallet_info();
    Console.WriteLine("This account has " + walletInfo.totalCoins + " Amino coins");
} catch 
{
    Console.WriteLine("Could not get wallet info");
}
```


### get_wallet_history(int start, int size) : List<Amino.Objects.CoinHistoryEntry>
This function allows you to get the wallet history of the current Amino account
- Success: Gets the wallet history and returns it as an Object List (List<Amino.Objects.CoinHistoryEntry>)
- Error: Throws an Exception
### Values:
- start : int (default: 0) : Sets the Start index for getting the wallet history
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.CoinHistoryEntry> coinHistory = client.get_wallet_history();
    Console.WriteLine("Latest transaction amount: " + coinHistory[0].changedCoins + " paid by " + coinHistory[0].userId);
} catch 
{
    Console.WriteLine("Could not get wallet history.");
}
```


### get_from_deviceId(string deviceId) : string
This function allows you to get a user ID thats linked to a deviceId
- Success: Returns the user ID as a string and completes the task successfully
- Error: Throws an exception
### Values:
- deviceId : string : The device ID that you want to get the userId from
### Example:
```CSharp
try 
{
    Console.WriteLine("Some stuff: " + client.get_from_deviceId("someDeviceId"));
} catch 
{
    Console.WriteLine("Could not get data from userId");
}
```


### get_from_code(string aminoUrl) : Amino.Objects.FromCode
This function allows you to get information about a specific Amino URL (code)
- Success: Gets the information and returns it as an Object (Amino.Objects.FromCode)
- Error: Throws an Exception
### Values:
- aminoUrl : string : The URL of the object that you want to get information about
### Example:
```CSharp
try 
{
    var objectInfo = client.get_from_code("someUrl");
    Console.WriteLine("Target Code: " + objectInfo.targetCode);
} catch 
{
    Console.WriteLine("Could not get object information");
}
```


### get_from_id(string objectId, Amino.Types.Object_Types type, string communityId) : Amino.Objects.FromId
This function allows you to get informations abou tan object using the object ID
- Success: Gets the objects information and returns it as an Object (Amino.Objects.FromId)
- Error: Throws an Exception
### Values:
- objectId : string : The ID of the object that you want to get information of
- type : Amino.Objects.Object_Types : The type of the obejct that you want to get information of
- communityId : string (default: null) : If you want to get information about an object inside of a community you can assign a CommunityId to this function
### Example:
```CSharp
try 
{
    var objectInfo = client.get_from_id("somePostId", Amino.Types.Object_Types.Blog, "123456");
    Console.WriteLine("Path of the object: " + objectInfo.path);
} catch 
{
    Console.WriteLine("Could not get object informations");
}
```


### get_supported_languages() : string[]
This function allows you to get the language codes for each supported language as a strin array
- Success: Gets the supported languages and returns them as a string array
- Error: Throws an Exception
### Values:
- None
### Example:
```CSharp
try 
{
    string[] supportedLanguages = client.get_supported_languages();
    Conmsole.WriteLine("List of supported languages:");
    foreach(string language in supportedLanguages) 
    {
        Console.WriteLine(language);
    }
} catch 
{
    Console.WriteLine("Could not get supported languages");
}
```


### claim_new_user_coupon() : Task
This function allows you to claim the new user coupon for the current Amino account
- Success: Claims the new user coupon and completes the Task successfully
- Error: Throws an Exception
### values:
- None
### Example:
```CSharp
try 
{
    client.claim_new_user_coupon();
    Console.WriteLine("Claimed new user coupon");
} catch 
{
    Console.WriteLine("Could not claim new user coupon");
}
```


### get_all_users(int start, int size) : List<Amino.Obejcts.UserProfile>
This function allows you to get a list of all global Amino users
- Success: Gets the global Amino users and returns them as an Object List (List<Amino.Objects.UserProfile>)
- Error: Throws an Exception
### Values:
- start : int (default: 0) : Sets the Start index for getting user profiles
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Obejcts.UserProfile> users = client.get_all_users();
    Console.WriteLine("List of user info:")
    foreach(Amino.Objects.UserProfile user in users) 
    {
        Console.WriteLine("Name: " + user.nickname.PadRight(16) + " userId: " + user.userId);
    }
} catch 
{
    Console.WriteLine("Could not get users!");
}
```


### accept_host(string chatId, string requestId) : Task
This function allows you to accept host / organizer of a chat room using the current Amino account
- Success: Accepts chat host role and completes the task successfully
- Error: Throws an Exception
### Values:
- chatId : string : The object / chat ID of the chat where you have been requested to be host in
- requestId : string : The object ID of the chat host request
### Example:
```CSharp
try 
{
    client.accept_host("someChatId", "someRequestId");
    Console.WriteLine("Chat host has been accepted");
} catch 
{
    Console.WriteLine("Could not accept chat host");
}
```


### accept_organizer(string chatId, string requestId) : Task
- Refer to `accept_host`.


### link_identify(string inviteCode) : Amino.Obejcts.FromInvite
This function allows you to get information about an Amino invite code and its community
- Success: Gets the invite codes information and returns it as an Object (Amino.Obejcts.FromInvite)
- Error: Throws an Exception
### Values:
- inviteCode : string : The Amino invite code you want to get information from (The inviteCode is **not** the full invite URL)
### Example:
```CSharp
try 
{
    var inviteInformation = client.link_identify("ABCDEF")M;
    Console.WriteLine("InviteId: " + inviteInformation.invitationId + " for community: " + inviteInformation.Community.name);
} catch 
{
    Console.WriteLine("Could not get invite information!");
}
```


### wallet_config(Amino.Types.Wallet_Config_Levels walletLevel) : Task
This function allows you to set the coin wallet configuration using the current Amino account
- Success: Changed the coin wallet configuration and returns the task successfully
- Error: Throws an Exception
### Values:
- walletLevel : Amino.Types.Wallet_Config_Levels : The wallet Level that you want to set
### Example:
```CSharp
try 
{
    client.wallet_config(Amino.Types.Wallet_Config_Levels.lvl_2);
    Console.WriteLine("Set wallet level successfully");
} catch 
{
    Console.WriteLine("Could not set wallet level");
}
```


### get_avatar_frames(int start, int size) : List<Amino.Objects.AvatarFrame>
This function allows you to get a list of Avatar Frames that the current Amino account has unlocked
- Success: Gets the Avatar frames and returns them as an Object List (List<Amino.Obejcts.AvatarFrame>)
- Error: Throws an Exception
### Values:
- start : int (default: 0) : Sets the Start index for getting the Avatar Frames
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
try 
{
    List<Amino.Objects.AvatarFrame> frames = client.get_avatar_frames();
    if(frames.Count > 0) 
    {
        Console.WriteLine("All Frame IDs and Names in current list:");
        foreach(Amino.Obejcts.AvatarFrame frame in frames) 
        {
            Console.WriteLine("FrameID: " + frame.frameId + " FrameName: " + frame.name);
        }
    } else 
    {
        Console.WriteLine("This account does not have any Avatar Frames!");
    }
} catch 
{
    Console.WriteLine("Could not get Avatar Frames");
}
```

## Events
- This library features a number of events that you can subscribe to!
- All events run on an Amino.Client() instance!
- All events will return either a value or an Object.


### onMessage : Amino.Objects.Message
This event fires each time the Client receives a Text message
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


### onImageMessage : Amino.Objects.ImageMessage
This event fires each time the Client receives an Image message
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


### onWebSocketMessage : string
This event fires each time a websocket message has been recevied by the Client
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



## helpers
- The helpers class is an important part of Amino.Net and any other Amino library, as it allows you to get around the Amino API more easily
### Values:
- BaseURL : string : This string represents the base URL to Aminos REST API
## Methods / Functions
### generate_device_id() : string
This function allows you to generate an Amino ready device ID 
### Values:
- None
### Example:
```CSharp
Console.WriteLine("Amino device ID: " + Amino.helpers.generate_device_id());
```


### generate_signiture(sring data) : string
This function allows you to generate an Amino valid request signiture
### Values:
- data : string : The data you want to turn into a signiture hash
### Example:
```CSharp
Console.WriteLine("Amino Signiture: " + Amino.helpers.generate_signiture("{ some JSON data }"));
```


### generate_file_signiture(byte[] data) : string
This function allows you to generate an Amino valid request signiture out of file data
### Values:
- data : byte[] : The file bytes that you want to turn into data
### Example:
```CSharp
Console.WriteLine("Some file signiture: " + Amino.helpers.generate_file_signiture(File.ReadAllBytes("Some_File_Path")));
```


### GetTimestamp() : double
This function allows you to get the current UNIX timestamp, it is **not** Amino ready!
### Values:
- None
### Example:
```CSharp
Console.WriteLine("Current UNIX Timestamp: " + Amino.helpers.GetTimestamp());
Console.WriteLine("Current UNIX Timestamp (Amino ready): " + Amino.helpers.GetTimestamp() * 1000);
```


### get_ObjectTypeID(Amino.Types.Object_Types type) : int
This function allows you to convert a Type into the fitting Amino object ID
### Values:
- type : Amino.Types.Object_Types : The Type enum that you want to convert into a number
### Example:
```CSharp
Console.WriteLine("The Amino ID for Blog posts is: " + Amino.helpers.get_ObjectTypeID(Types.Object_Types.Blog));
```