// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

using var client = new HttpClient();

// https://code.zeabur.app/
// var response = await client.PostAsJsonAsync("http://127.0.0.1:8080/", new CodeModel
// {
//     Code = "def main():\n    print(\"Hello, World!\")\n",
//     Lang = "py"
// });
//
// Console.WriteLine(await response.Content.ReadAsStringAsync());

const string order = """
                     hostname
                     """;
var response = await client.PostAsJsonAsync("https://code.zeabur.app/Order",order);
Console.WriteLine(await response.Content.ReadAsStringAsync());

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}