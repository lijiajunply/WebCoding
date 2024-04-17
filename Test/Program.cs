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

Console.WriteLine(CodeModel.Result(result));

response = await client.PostAsJsonAsync("https://code.zeabur.app/Order","python3 --version");
result = await response.Content.ReadAsStringAsync();
Console.WriteLine(CodeModel.Result(result));

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";

    public static string Result(string result)
    {
        return result.Replace("\"", "").Replace("\\n", "\n");
    }
}