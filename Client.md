# Amino.Client()
## This is a seperated documentation file written for the Amino.Client()
- This file might not always be up to date right away
- This file contains every Amino.Client() function / method nicely wrapped up
- To see the full general documentation consider reading Readme.md ([Click here](https://github.com/FabioGaming/Amino.NET))



<details>
<summary id="functionName">Client(string deviceId)</summary>
<p id="functionDescription">The Amino.Client() Object is a crucial object to make Bots or tools, as there need to be an instance of it to make the library work</p>

### Object Values:
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

### Extras
- Consider using try catch statements, if a function fails while executing the request, the response code doesn't indicate success or if a custom condition is triggered, it will throw an Exception!

### Required Values:
- deviceId : string : if left empty, it will generate a valid Amino Device ID

### Example:
```CSharp
Amino.Client client = new Amino.Client(); // This client will be used as an Example Client for the rest of the Amino.Client() docuemntations, whenever "client" is being used, its just an instance of Amino.Client()
```

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">login(string email, string password, string secret)</summary>
<p id="functionDescription">You can log into an existing Amino account using this function.</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">logout()</summary>
<p id="functionDescription">You can log out of an Amino account using this function, make sure you are logged into an account to use this function!</p>

### Required Values:
- None
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">request_verify_code(string email, bool resetPassword)</summary>
<p id="functionDescription">You can request an Amino verification code using this function.</p>

### Required Values:
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

### Returns:
- Nothing
</details>


<details>
<summary id="functionName">register(string name, string email, string password, string verificationCode, string deviceId)</summary>
<p id="functionDescription">This function allows you to register an Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">restore_account(string email, string password, string deviceId)</summary>
<p id="functionDescription">This function allows you to restore a deleted Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">delete_account(string password)</summary>
<p id="functionDescription">This function allows you to delete the current Amino account in use.</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">activate_account(string email, string verificationCode, string deviceId)</summary>
<p id="functionDescription">This function allows you to activate an Amino account using a verification Code</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">configure_account(Amino.Types.account_gender gender, int age)</summary>
<p id="functionDescription">
This function allows you to configure an Amino accounts age and gender</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">change_password(string email, string password, string verificationCode)</summary>
<p id="functionDescription">This function allows you to change the password of the current Amino account.</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">get_user_info(string userId)</summary>
<p id="functionDescription">This function allows you to get information about a global Amino Profile</p>

### Required Values:
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

### Returns:
- Amino.Objects.GlobalProfile
</details>

<details>
<summary id="functionName">check_device(string deviceId)</summary>
<p id="functionDescription">This function allows you to check if a device ID is valid or not</p>

### Required Values:
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

### Returns:
- bool
</details>

<details>
<summary id="functionName">get_event_log()</summary>
<p id="functionDescription">This function allows you to get information about the current accounts event log!</p>

### Required Values:
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

### Returns:
- Amino.Objects.EventLog
</details>

<details>
<summary id="functionName">get_subClient_communities(int start, int size)</summary>
<p id="functionDescription">This function allows you to get information about all the Communities where the current Amino account is in</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.Community>
</details>

<details>
<summary id="functionName">get_subClient_profiles(int start, int size)</summary>
<p id="functionDescription">This function allows you to get information about all the community profiles where the current Amino account is in</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.CommunityProfile>
</details>

<details>
<summary id="functionName">get_account_info()</summary>
<p id="functionDescription">This function allows you to get information about the current Amino account</p>

### Required Values:
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

### Returns:
- Amino.Objects.UserAccount
</details>

<details>
<summary id="functionName">get_chat_threads(int start, int size)</summary>
<p id="functionDescription">This function allows you to get all chat threads where the current Amino account is in</p>

### Required Values:
- start : int (default: 0) : Sets the Start index for getting chat list
- size : int (default: 25) : Sets the range between `start` and whatever this is set to
### Example:
```CSharp
{
    List<Amino.Objects.Chat> chatList = client.get_chat_threads();
    Console.WriteLine("Nickname of the owner of the first chat: " + chatList[0].Author.nickname);
} catch 
{
    Console.WriteLine("Could not get chats!");
}
```

### Returns:
- List<Amino.Objects.Chat>
</details>

<details>
<summary id="functionName">get_chat_thread(string chatId)</summary>
<p id="functionDescription">This function allows you to get information about a specific chat thread where the current Amino account is in</p>

### Required Values:
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

### Returns:
- Amino.Objects.Chat
</details>

<details>
<summary id="functionName">get_chat_users(string chatId, int start, int size)</summary>
<p id="functionDescription">This function allows you to get chat member information about a specific chat thread</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.ChatMember>
</details>

<details>
<summary id="functionName">join_chat(string chatId)</summary>
<p id="functionDescription">This function allows you to join a chat thread using the current Amino account.</p>

### Required Values:
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

### Returns:
- None
</details>

<details>
<summary id="functionName">leave_chat(string chatId)</summary>
<p id="functionDescription">This function allows you to leave a chat thread using the current Amino account.</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">invite_to_chat(stiring[] userIds, string chatId)</summary>
<p id="functionDescription">This function allows you to invite one or more members to a chat thread with the current Amino account</p>

### Required Values:

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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">kick_from_chat(string userId, string chatId, bool allowRejoin)</summary>
<p id="functionDescription">This function allows you to kick a user from a chat thread</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">get_chat_messages(string chatId, int size, string pageToken)</summary>
<p id="functionDescription">This function allows you to get a collection of messages in a specific chat thread the current Amino account is in</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.MessageCollection>
</details>

<details>
<summary id="functionName">search_community(string aminoId)</summary>
<p id="functionDescription">This function allows you to search for a Community by its aminoId (**not** and ObjectId) and retrieve information about it</p>

### Required Values:
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

### Returns:
- Amino.Objects.CommunityInfo
</details>

<details>
<summary id="functionName">get_user_following(string userId, int start, int size)</summary>
<p id="functionDescription"></p>

### Required Values:
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

### Returns:
- List<Amino.Objects.UserFollowings>
</details>

<details>
<summary id="functionName">get_user_followers(string userId, int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of users that follow a user</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.UserFollowings>
</details>

<details>
<summary id="functionName">get_user_visitors(string userId, int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of users that have visited a target profile</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.UserVisitor>
</details>

<details>
<summary id="functionName">get_blocked_users(int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of users that the current Amino account has blocked</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.BlockedUser>
</details>

<details>
<summary id="functionName">get_blocker_users(int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of user IDs of the users who have currenty blocked the current Amino account</p>

### Required Values:
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

### Returns:
- List<<string>string>
</details>

<details>
<summary id="functionName">get_wall_comments(string userId, Amino.Types.Sorting_Types type, int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of comments that have been left on a users wall</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.Comment>
</details>

<details>
<summary id="functionName">flag(string reason, Amino.Types.Flag_Types flagType, Amino.Types.Flag_Targets targetType, string targetId, bool asGuest)</summary>
<p id="functionDescription">This function allows you to flag a post / profile on Amino</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">delete_message(string chatId, string messageId, bool asStaff, string reason) </summary>
<p id="functionDescription">This function allows you to delete a specific chat message using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">mark_as_read(string chatId, string messageId)</summary>
<p id="functionDescription">This function allows you to mark a message as read</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">visit(string userId)</summary>
<p id="functionDescription">This function allows you to visit a users Amino profile</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">follow_user(string userId)</summary>
<p id="functionDescription">This function allows you to follow a user using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">unfollow_user(string userId)</summary>
<p id="functionDescription">This function allows you to unfollow a user using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">block_user(string userId)</summary>
<p id="functionDescription">This function allows you to block a user using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">unblock_user(string userId)</summary>
<p id="functionDescription">This function allows you to unblock a user using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">join_community(string communityId, string invitationCode)</summary>
<p id="functionDescription">This function allows you to join a community using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">join_community_request(string communityId, string message)</summary>
<p id="functionDescription">This function allows you to make a join request to a community</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">leave_community(string communityId)</summary>
<p id="functionDescription">This function allows you to leave a comunity using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">flag_community(string communityId, string reason, Amino.Types.Flag_Types flagType, bool asGuest)</summary>
<p id="functionDescription">This function allows you to flag a community</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">upload_media(byte[] file | string filePath, Amino.Types.Upload_File_Types type)</summary>
<p id="functionDescription">This function allows you to upload media directly to the Amino servers, it will return the resulting media URL.</p>

### Required Values:
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

### Returns:
- string
</details>

<details>
<summary id="functionName">edit_profile(string nickname, string content, byte[] icon, string backgroundColor, string backgroundMediaUrl, string defaultChatbubbleId)</summary>
<p id="functionDescription">This function allows you to edit your global Amino profile</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">set_privacy_status(bool isAnonymous, bool getNotifications)</summary>
<p id="functionDescription">This function allows you to set the privacy status of the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">set_amino_id(string aminoId)</summary>
<p id="functionDescription">This function allows you to change your Amino ID, note that you can't do this an unlimited amount of times</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">add_linked_community(int communityId)</summary>
<p id="functionDescription">This function allows you to add a linked community to the profile of the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">remove_linked_community(int communityId)</summary>
<p id="functionDescription">This function allows you to remove a linked community from the profile of the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">comment(string message, Amino.Types.Comment_Types type, objectId)</summary>
<p id="functionDescription">This function allows you to comment below a post, a wall or reply to a comment using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">delete_comment(string commentId, Amino.Types.Comment_Types type, string objectId)</summary>
<p id="functionDescription">This function allows you to delete a comment from a post, a users wall or a reply using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">like_post(string objectId, Amino.Types.Post_Types type)</summary>
<p id="functionDescription">This function allows you to like a post using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">unlike_post(string objectId, Amino.Types.Post_Types type)</summary>
<p id="functionDescription">This function allows you to unlike a post using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">get_membership_info()</summary>
<p id="functionDescription">This function allows you to get information about the current Amino accounts Amino+ membership</p>

### Required Values:
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

### Returns:
- Amino.Objects.MembershipInfo
</details>

<details>
<summary id="functionName">get_ta_announcements(Amino.Types.Supported_Languages language, int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of Team Amino announcements</p>

### Required Values:
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

### Returns:
- Amino.Objects.Post
</details>

<details>
<summary id="functionName">get_wallet_info()</summary>
<p id="functionDescription">This function allows you to get the wallet info of the current Amino account</p>

### Required Values:
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

### Returns:
- Amino.Objects.WalletInfo
</details>

<details>
<summary id="functionName">get_wallet_history(int start, int size)</summary>
<p id="functionDescription">This function allows you to get the wallet history of the current Amino account</p>

### Required Values:
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

### Returns:
- List<Amino.Objects.CoinHistoryEntry>
</details>

<details>
<summary id="functionName">get_from_deviceId(string deviceId)</summary>
<p id="functionDescription">This function allows you to get a user ID thats linked to a deviceId</p>

### Required Values:
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

### Returns:
- string
</details>

<details>
<summary id="functionName">get_from_code(string aminoUrl)</summary>
<p id="functionDescription">This function allows you to get information about a specific Amino URL (code)</p>

### Required Values:
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

### Returns:
- Amino.Objects.FromCode
</details>

<details>
<summary id="functionName">get_from_id(string objectId, Amino.Types.Object_Types type, string communityId)</summary>
<p id="functionDescription">This function allows you to get informations abou tan object using the object ID</p>

### Required Values:
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

### Returns:
- Amino.Objects.FromId
</details>

<details>
<summary id="functionName">get_supported_languages() </summary>
<p id="functionDescription">This function allows you to get the language codes for each supported language as a strin array</p>

### Required Values:
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

### Returns:
- string[]
</details>

<details>
<summary id="functionName">claim_new_user_coupon()</summary>
<p id="functionDescription">This function allows you to claim the new user coupon for the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">get_all_users(int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of all global Amino users</p>

### Required Values:
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

### Returns:
- List<Amino.Obejcts.UserProfile>
</details>

<details>
<summary id="functionName">accept_host(string chatId, string requestId)</summary>
<p id="functionDescription">This function allows you to accept host / organizer of a chat room using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">accept_organizer(string chatId, string requestId)</summary>
<p id="functionDescription">Refer to `accept_host`.</p>
</details>

<details>
<summary id="functionName">link_identify(string inviteCode)</summary>
<p id="functionDescription">This function allows you to get information about an Amino invite code and its community</p>

### Required Values:
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

### Returns:
- Amino.Obejcts.FromInvite
</details>

<details>
<summary id="functionName">wallet_config(Amino.Types.Wallet_Config_Levels walletLevel)</summary>
<p id="functionDescription">This function allows you to set the coin wallet configuration using the current Amino account</p>

### Required Values:
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

### Returns:
- Nothing
</details>

<details>
<summary id="functionName">get_avatar_frames(int start, int size)</summary>
<p id="functionDescription">This function allows you to get a list of Avatar Frames that the current Amino account has unlocked</p>

### Required Values:
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

<!--- JUST A TEMPLATE

<details>
<summary id="functionName"></summary>
<p id="functionDescription"></p>

### Required Values:

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