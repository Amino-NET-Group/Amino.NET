# Amino.helpers
## This is a seperated documentation file written for the Amino.helpers class!
- This file might not always be up to date right away
- This file contains every Amino.helpers function / method nicely wrapped up
- To see the full general documentation consider reading Readme.md ([Click here](https://github.com/FabioGaming/Amino.NET))

<details>
<summary id="functionName">Amino.helpers</summary>
<p id="functionDescription">The helpers class is an important part of Amino.Net and any other Amino library, as it allows you to get around the Amino API more easily</p>

### Values:
- BaseURL : string : This string represents the base URL to Aminos REST API
</details>

<details>
<summary id="functionName">generate_device_id()</summary>
<p id="functionDescription">This function allows you to generate an Amino ready device ID </p>

### Required Values:
- None
### Example:
```CSharp
Console.WriteLine("Amino device ID: " + Amino.helpers.generate_device_id());
```

### Returns:
- string
</details>

<details>
<summary id="functionName">generate_signiture(sring data)</summary>
<p id="functionDescription">This function allows you to generate an Amino valid request signiture</p>

### Required Values:
- data : string : The data you want to turn into a signiture hash
### Example:
```CSharp
Console.WriteLine("Amino Signiture: " + Amino.helpers.generate_signiture("{ some JSON data }"));
```

### Returns:
- string
</details>

<details>
<summary id="functionName">generate_file_signiture(byte[] data)</summary>
<p id="functionDescription">This function allows you to generate an Amino valid request signiture out of file data</p>

### Required Values:
- data : byte[] : The file bytes that you want to turn into data
### Example:
```CSharp
Console.WriteLine("Some file signiture: " + Amino.helpers.generate_file_signiture(File.ReadAllBytes("Some_File_Path")));
```

### Returns:
- string
</details>

<details>
<summary id="functionName">GetTimestamp()</summary>
<p id="functionDescription">This function allows you to get the current UNIX timestamp, it is **not** Amino ready!</p>

### Required Values:
- None
### Example:
```CSharp
Console.WriteLine("Current UNIX Timestamp: " + Amino.helpers.GetTimestamp());
Console.WriteLine("Current UNIX Timestamp (Amino ready): " + Amino.helpers.GetTimestamp() * 1000);
```

### Returns:
- double
</details>

<details>
<summary id="functionName">get_ObjectTypeID(Amino.Types.Object_Types type)</summary>
<p id="functionDescription">This function allows you to convert a Type into the fitting Amino object ID</p>

### Required Values:
- type : Amino.Types.Object_Types : The Type enum that you want to convert into a number
### Example:
```CSharp
Console.WriteLine("The Amino ID for Blog posts is: " + Amino.helpers.get_ObjectTypeID(Types.Object_Types.Blog));
```

### Returns:
- int
</details>



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