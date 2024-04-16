using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapPost("", async (CodeModel model) =>
{
    Console.WriteLine(JsonSerializer.Serialize(model));
    var proc = new Process();
    proc.StartInfo.FileName = "/bin/sh";
    proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
    proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
    proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
    proc.Start();

    await using var write = new FileStream($"text.{model.Lang}", FileMode.OpenOrCreate);
    await write.WriteAsync(Encoding.UTF8.GetBytes(model.Code));
    var order = model.Lang switch
    {
        "c" => "gcc text.c && a.out",
        "cpp" => "g++ -std=c++11 text.cpp && a.out",
        "cs" => "dotnet-exec text.cs",
        "java" => "java text.java",
        "py" => "python3 text.py",
        "py2" => "python2 text.py2",
        _ => null
    };
    Console.WriteLine(order);
    await proc.StandardInput.WriteLineAsync(order);
    proc.StandardInput.Close();
    var endAsync = await proc.StandardOutput.ReadToEndAsync();
    Console.WriteLine(endAsync);
    proc.Close();
    proc.Dispose();
    
    File.Delete($"text.{model.Lang}");
    
    return Results.Ok(endAsync);
});

app.MapPost("Order",async (string order) =>
{
    try
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
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.BadRequest();
    }
});

app.Run();

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}

[JsonSerializable(typeof(CodeModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;