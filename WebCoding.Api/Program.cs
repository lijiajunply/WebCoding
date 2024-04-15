using System.Diagnostics;
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
    using var proc = new Process();
    proc.StartInfo.FileName = "/bin/bash";
    proc.StartInfo.UseShellExecute = false; //是否使用操作系统shell启动
    proc.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
    proc.StartInfo.RedirectStandardOutput = true; //由调用程序获取输出信息
    proc.StartInfo.RedirectStandardError = true; //重定向标准错误输出
    proc.Start();
    var shell = "cat>" + "text." + model.Lang + @"<<\EOF " + Environment.CommandLine + model.Code +
                Environment.CommandLine + "EOF" + Environment.CommandLine;
    var order = model.Lang switch
    {
        "c" => shell + "gcc text.c && a.out",
        "cpp" => shell + "g++ -std=c++11 text.cpp && a.out",
        "cs" => shell + "dotnet-exec text.cs",
        "java" => shell + "java text.java",
        "py" => shell + "python3 text.py",
        "py2" => shell + "python2 text.py2",
        _ => null
    };
    await proc.StandardInput.WriteLineAsync(order);
    proc.StandardInput.Close();
    return await proc.StandardOutput.ReadToEndAsync();
});

app.Run();

internal record CodeModel
{
    public string Code { get; set; } = "";
    public string Lang { get; set; } = "";
}

[JsonSerializable(typeof(CodeModel))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;