using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var api = app.MapGroup("/");
api.MapPost("", async (CodeModel model) =>
{
    Console.WriteLine(JsonSerializer.Serialize(model));
    var proc = new Process();
    proc.StartInfo.FileName = "/bin/sh";
    proc.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
    proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
    proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
    proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
    proc.Start();
    
    var order = $"cat>text.{model.Lang} <<EOF {Environment.NewLine} {model.Code} {Environment.NewLine} EOF {Environment.NewLine}";
    order += model.Lang switch
    {
        "c" => "gcc text.c && a.out",
        "cpp" => "g++ -std=c++11 text.cpp && a.out",
        "cs" => "dotnet-exec text.cs",
        "java" => "java text.java",
        "py" => "python3 text.py",
        "py2" => "python2 text.py2",
        _ => null
    };
    Console.WriteLine($"docker exec -i -t ubuntu /bin/sh{Environment.NewLine}{order}");
    await proc.StandardInput.WriteLineAsync("docker exec -i -t ubuntu /bin/bash");
    await proc.StandardInput.WriteLineAsync(order);
    proc.StandardInput.Close();
    var endAsync = await proc.StandardOutput.ReadToEndAsync();
    Console.WriteLine(endAsync);
    proc.Close();
    proc.Dispose();
    return endAsync;
});

api.MapPost("/Order",async (string order) =>
{
    Console.WriteLine(order);
    try
    {
        var proc = new Process();
        proc.StartInfo.FileName = "/bin/sh";
        proc.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
        proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
        proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
        proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
        proc.Start();
        await proc.StandardInput.WriteLineAsync("/bin/bash");
        await proc.StandardInput.WriteLineAsync(order);
        proc.StandardInput.Close();
        var endAsync = await proc.StandardOutput.ReadToEndAsync();
        Console.WriteLine(endAsync);
        proc.Close();
        proc.Dispose();
        return endAsync;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
});

app.Run();

/*var folder = new DirectoryInfo("Code");
folder.Create();*/

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}

[JsonSerializable(typeof(CodeModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;