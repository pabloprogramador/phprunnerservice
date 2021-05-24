# phprunnerservice
https://github.com/pabloprogramador/phprunnerservice

## How to use

Models.Users user = await service.Get<Models.Users>(1); //id: 1
Console.WriteLine(user.Name);


## How to set

PhpRunnerService.Settings.ServerApi = "http://[YOUR SERVER PHPRUNNER]/api";

PhpRunnerService service = new PhpRunnerService();

## How to model
Example:

using System.Text.Json.Serialization;
using static PhpRunnerService.Converters.JsonCustomConverters;
namespace Quyccky.Models
{
    public class Users
    {
        [JsonConverter(typeof(IntConverter))]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
