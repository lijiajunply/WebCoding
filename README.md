# WebCoding
用C#写的在线编辑器

## 后端
前端代码 -> 调用命令 -> 输出 -> 返回前端
  
代码：
  
```csharp
Process a = new Process();
a.StartInfo.FileName = "/bin/bash";
a.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
a.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
a.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
a.StartInfo.RedirectStandardError = true;//重定向标准错误输出
a.Start();
a.StandardInput.WriteLine("sudo  docker container run hello-world");//指令
a.StandardInput.Close();

Console.WriteLine(a.StandardOutput.ReadToEnd());//输出
```

