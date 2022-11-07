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
### request_verify_code(string email, bool resetPassword)
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

### login(string email, string password, string secret)
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

### logout()
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
    Console.WriteLine("Could not log out!")
}
```

### register(string name, string email, string password, string verificationCode, string deviceId)
This function allows you to register an Amino account
- Success: The account will be created and the Task completes Successfully
- Error: Throws an Exception

