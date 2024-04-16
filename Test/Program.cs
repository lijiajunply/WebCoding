// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

using var client = new HttpClient();

https://code.zeabur.app/
var response = await client.PostAsJsonAsync("https://code.zeabur.app", new CodeModel
{
    Code = File.ReadAllText(@"C:\Projects\RiderProjects\WebCoding\Test\test.py"),
    Lang = "py"
});

var result = await response.Content.ReadAsStringAsync();

Console.WriteLine(result);

response = await client.PostAsJsonAsync("https://code.zeabur.app/Order", "ls");
Console.WriteLine(await response.Content.ReadAsStringAsync());

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}