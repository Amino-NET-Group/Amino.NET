# Amino.Net
An unofficial C# wrapper for Aminos REST API to make Amino Bots and Tools

# What can this wrapper do?
Amino.Net has a lot of functionality that allow you to develop Amino tools and bots for multiple types of C# / dotNet projects

## Extras and Credits
- This C# library has been made possible using [Amino.py](https://github.com/Slimakoi/Amino.py) as it is based on [Slimakoi](https://github.com/Slimakoi/)s work
- Some values or Objects might return `null`, this is because the library pulls its data straight from the Amino REST API, if values in the API return `null` there's nothing i can do!

## Important Notice
By using this library you agree that you are aware of the fact that you are breaking the App services Terms of Service - as Team Amino strictly forbids the use of any sort of third party software / scripting to gain an advantage over other members, any activity by third party tools found by Team Amino may result in your account getting banned from their services!

## How to install
You can get Amino.Net straight from [NuGet.org](https://nuget.org) or any NuGet Package manager!


# DOCUMENTATION
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

### Exmaple:
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



