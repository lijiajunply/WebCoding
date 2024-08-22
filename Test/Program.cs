using System.Net.Http.Json;

using var client = new HttpClient();
var url = "https://code.zeabur.app";
// url = "http://localhost:5061";
// https://code.zeabur.app
var response =
    await client.PostAsJsonAsync(url, new CodeModel() { Code = "Console.WriteLine(\"Hello World!\");", Lang = "cs" });
// CodeModel.FromFile(@"C:\Projects\RiderProjects\WebCoding\Test\Program.cs")
var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(CodeModel.Result(result));

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