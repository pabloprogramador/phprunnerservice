# phprunnerservice
https://github.com/pabloprogramador/phprunnerservice

## How to use
```csharp
// get user id = 1
Models.Users user = await service.Get<Models.Users>(1);
Console.WriteLine(user.Name);
```

## How to set
```csharp
PhpRunnerService.Settings.ServerApi = "http://[YOUR SERVER PHPRUNNER]/api";

PhpRunnerService service = new PhpRunnerService();
```

## How to model

Example:
```csharp
using System.Text.Json.Serialization;
using static PhpRunnerService.Converters.JsonCustomConverters;

public class Users
{
    [JsonConverter(typeof(IntConverter))]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
```
