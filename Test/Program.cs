// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

using var client = new HttpClient();
var url = "https://code.zeabur.app";
// url = "http://localhost:5061";
// https://code.zeabur.app
var response = await client.PostAsJsonAsync(url,
    CodeModel.FromFile(@"C:\Projects\RiderProjects\WebCoding\Test\test.c"));

var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(CodeModel.Result(result));

// response = await  client.PostAsJsonAsync("https://code.zeabur.app/Order", "cd ./code && ls");
// result = await response.Content.ReadAsStringAsync();
//
// Console.WriteLine(CodeModel.Result(result));

[Serializable]
internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";

    public static CodeModel FromFile(string path)
    {
        return new CodeModel() { Code = File.ReadAllText(path), Lang = Path.GetExtension(path).TrimStart('.') };
    }

    public static string Result(string result)
    {
        return result.Replace("\\n", "\n")
            .Replace("\\r", "\r")
            .Replace("\"", "")
            .Replace(@"\\", "\\");
    }
}