// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

using var client = new HttpClient();

https://code.zeabur.app/
var response = await client.PostAsJsonAsync("https://code.zeabur.app", new CodeModel
{
    Code = "print(\"Hello, World!\")",
    Lang = "py"
});

Console.WriteLine(await response.Content.ReadAsStringAsync());

const string order = """
                     uname -a
                     """;
response = await client.PostAsJsonAsync("https://code.zeabur.app/Order",order);
Console.WriteLine(await response.Content.ReadAsStringAsync());

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}