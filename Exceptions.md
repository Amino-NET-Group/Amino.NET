# Exceptions & Troubleshooting
## This is a seperated documentation file written for Amino.Nets Exception and Troubleshooting!
- This file might not always be up to date right away
- This file contains every Amino.Net Exception wrapped up
- To see the full general documentation consider reading Readme.md ([Click here](https://github.com/FabioGaming/Amino.NET))


<details>
<summary id="functionName">ErrorCode: 0: Client not logged in</summary>
<p id="functionDescription">Occurs when the Current Amino.Client() is not logged into any account when trying to call an account dependend function</p>

### Exception Type:
- Amino.Net Exception
### Fix:
- the login() function of Amino.Client

### Note:
- None
</details>


<details>
<summary id="functionName">start cannot be lower than 0</summary>
<p id="functionDescription">Occurs when trying to set an integer range below 0</p>

### Exception Type:
- Amino.Net Exception
### Fix:
- Set the start value to 0 or above

### Note:
- None
</details>

<details>
<summary id="functionName">Too many Requests</summary>
<p id="functionDescription">Occurs when an Amino endpoint has been called too often in too little time</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Consider using a delay on the function you got this API response from

### Note:
- None
</details>

<details>
<summary id="functionName">403 Access Denied</summary>
<p id="functionDescription">Occurs when a Server gets too many requests by the same IP address</p>

### Exception Type:
- HTTP Response
### Fix:
- Consider putting a delay to your code

### Note:
- 403 Access denied is a default HTTP response and cannot be fixed by code.
</details>


<details>
<summary id="functionName">Invalid Email</summary>
<p id="functionDescription">Occurs when an Invalid email address is given</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Put a valid email address

### Note:
- None
</details>

<details>
<summary id="functionName">Invalid password</summary>
<p id="functionDescription">Occurs when the account password is formatted in the wrong way</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Read the API Response and put the password accordingly

### Note:
- None
</details>


<details>
<summary id="functionName">Your device is currently not supported</summary>
<p id="functionDescription">Occurs when the accounts device ID is invalid</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Update your device ID to the latest possible ID type

### Note:
- None
</details>


<details>
<summary id="functionName">This code or link is invalid</summary>
<p id="functionDescription">Occurs when the given Amino URL / Code is invalid or outdated</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Put a valid Code / URL

### Note:
- None
</details>


<details>
<summary id="functionName">Failed to establish connection / Timed out</summary>
<p id="functionDescription">Occurs when the Request is taking too long</p>

### Exception Type:
- HTTP Response
### Fix:
- Don't use a proxy / VPN or try again later with a mmore stable connection

### Note:
- None
</details>


<details>
<summary id="functionName">The requested data no longer exists</summary>
<p id="functionDescription">Occurs when the object you want to target doesn't exist anymore</p>

### Exception Type:
- Amino REST Exception
### Fix:
- Get a new Target ID / URL

### Note:
- None
</details>


<details>
<summary id="functionName">You are Banned</summary>
<p id="functionDescription">Occurs when the current Amino account has been banned from a community they want to target</p>

### Exception Type:
- Amino REST Exception
### Fix:
- There is no fix

### Note:
- None
</details>


<details>
<summary id="functionName">Bad Request</summary>
<p id="functionDescription">Occurs when the request data is wrong</p>

### Exception Type:
- HTTP Response
### Fix:
- None, please contact the Author of the library or open a GitHub issue as soon as possible

### Note:
- None
</details>


<details>
<summary id="functionName">any NullException</summary>
<p id="functionDescription">Occurs when a target value is null</p>

### Exception Type:
- C# / Amino.Net Exception
### Fix:
- If you're sure that your data is valid, please consider contacting the Author of the library or open a GitHub issue as soon as possible

### Note:
</details>


<!--- JUST A TEMPLATE

<details>
<summary id="functionName"></summary>
<p id="functionDescription"></p>

### Exception Type:
- 
### Fix:
- 

### Note:
</details>
--->