using System.Diagnostics;

namespace WebCoding.BlazorServer.Model;
/// <summary>
/// 现在支持的编程语言: C#,C++,C,Java,Python,Python2
/// </summary>
public enum Lang
{
    cs,
    java,
    cpp,
    c,
    py,
    py2
}

public class DebugModel
{
    private Process proc;
    private Lang _lang;
    private string _code;
    private string _codefile;
    /// <summary>
    /// debugmodel
    /// </summary>
    /// <param name="lang">语言</param>
    /// <param name="code">代码</param>
    public DebugModel(Lang lang,string code)
    {
        proc = new Process();
        proc.StartInfo.FileName = "/bin/bash";
        proc.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
        proc.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        proc.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        proc.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        _lang = lang;
        _code = code;
        _codefile = "text."+(_lang.ToString());
    }
    /// <summary>
    /// 运行代码
    /// </summary>
    /// <returns>输出:string</returns>
    public string RunCode()
    {
        proc.Start();
        proc.StandardInput.WriteLine(LangToShell());
        proc.StandardInput.Close();
        return proc.StandardOutput.ReadToEnd();
    }

    public string LangToShell()
    {
        string shell = "docker run -i ubuntu /bin/bash && cat>" + _codefile + @"<<\EOF " + _code +"&& EOF &&";
        switch (_lang)
        {   
            case Lang.c:
                return shell + "gcc text.c && a.out";
            case Lang.cpp:
                return shell + "g++ -std=c++11 text.cpp && a.out";
            case Lang.cs:
                return shell + "dotnet-exec text.cs";
            case Lang.java:
                return shell + "java text.java";
            case Lang.py:
                return shell + "python3 text.py";
            case Lang.py2:
                return shell + "python2 text.py2";
            default:
                return null;
        }
    }
}