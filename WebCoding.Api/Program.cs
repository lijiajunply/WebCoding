using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapPost("", async ([FromBody]CodeModel model) =>
{
    Console.WriteLine(JsonSerializer.Serialize(model));
    var proc = new Process();
    proc.StartInfo.FileName = "/bin/sh";
    proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
    proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
    proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
    proc.Start();

    Directory.CreateDirectory("code");
    File.WriteAllText($"code/text.{model.Lang}", model.Code);
    var order = model.Lang switch
    {
        "c" => ["gcc text.c","a.out"],
        "cpp" => ["g++ -std=c++11 text.cpp ","a.out"],
        "cs" => ["dotnet-exec text.cs"],
        "java" => ["java text.java"],
        "py" => ["python3 text.py"],
        "py2" => ["python2 text.py2"],
        _ => Array.Empty<string>()
    };
    Console.WriteLine(order);
    await proc.StandardInput.WriteLineAsync("cd ./code");
    foreach (var s in order)
        await proc.StandardInput.WriteLineAsync(s);
    proc.StandardInput.Close();
    var endAsync = await proc.StandardOutput.ReadToEndAsync();
    Console.WriteLine(endAsync);
    proc.Close();
    proc.Dispose();

    Directory.Delete("code",true);

    return Results.Ok(endAsync);
});

app.MapPost("Order", async ([FromBody]string order) =>
{
    Console.WriteLine(order);
    var proc = new Process();
    proc.StartInfo.FileName = "/bin/sh";
    proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
    proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
    proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
    proc.Start();
    await proc.StandardInput.WriteLineAsync(order);
    proc.StandardInput.Close();
    var endAsync = await proc.StandardOutput.ReadToEndAsync();
    Console.WriteLine(endAsync);
    proc.Close();
    proc.Dispose();
    return Results.Ok(endAsync);
});

app.MapPost("File", ([FromBody] IFormFile file) =>
{
    using var f = new FileStream(file.FileName,FileMode.OpenOrCreate);
    f.CopyToAsync(file.OpenReadStream());
    return Results.Ok(file.FileName);
});

app.Run();

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}

[JsonSerializable(typeof(CodeModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;