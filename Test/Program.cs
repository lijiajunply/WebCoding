// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

using var client = new HttpClient();

var response = await client.PostAsJsonAsync("https://code.zeabur.app", new CodeModel
{
    Code = "Console.WriteLine(\"Hello, World!\")",
    Lang = "cs"
});

var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(CodeModel.Result(result));

response = await  client.PostAsJsonAsync("https://code.zeabur.app/Order", "dotnet");
result = await response.Content.ReadAsStringAsync();

Console.WriteLine(CodeModel.Result(result));

[Serializable]
internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";

    public static string Result(string result)
    {
        return result.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\"","");
    }
}