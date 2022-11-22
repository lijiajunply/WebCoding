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
虽然现在还有bug（应该是docker的原因，我在windows上试过了，到/bin/bash这里是没问题的），但是我并不想写下去了，累了，麻木了[doge]

如果要写下去，就先把上面那个bug改了，还要用一下codemirror,那个东西我包装了一下，在https://gitee.com/luckyfishisdashen/CodeMirror.Blazor

上，使用那个就自己去看一下[doge]

现在的问题好像在传输上，我真的懒得改了，下个学期再说吧，我累了(我在windows上也试过，但因为是wasm项目，不会告诉我是哪里出问题了，所以我懒得改了，知道的话就告诉我一声，谢谢)
  

