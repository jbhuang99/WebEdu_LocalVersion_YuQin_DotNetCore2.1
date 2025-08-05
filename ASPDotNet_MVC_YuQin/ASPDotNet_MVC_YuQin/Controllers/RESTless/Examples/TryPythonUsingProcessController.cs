using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{
    public class TryPythonUsingProcessController : Controller
    {


            public ActionResult Index()
        {
            //String pythonExeFile = @"C:\Python\Python38\python.exe"; // Python平台的路径
            String pythonExeFile = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python39_64\python.exe"; // Python平台的路径
            //String PythonSorceCodeFile = @"D:\temp\tryCalledByDotnetCS.py"; // Python源码的路径
           // String PythonSorceCodeFile = @"D:\WebEdu_LocalVersion_YuQin\About-Binary_ASM_CPP_CS-FourPlatformsExpansionLanguages(Examples)\Binary_ASM_CPP_Python(Python_ConsoleApplicationK12Edu)\K12Edu.py"; // Python源码的路径
            String PythonSorceCodeFile = @"D:\WebEdu_LocalVersion_YuQin\About-Binary_ASM_CPP_CS-FourPlatformsExpansionLanguages(Examples)\Binary_ASM_CPP_Python(Python_ConsoleApplicationK12Edu)\TryWaitForCSCalling.py"; // Python源码的路径
            Process process = new Process();

            process.StartInfo.FileName = pythonExeFile; // 指定Python解释器的路径
            process.StartInfo.Arguments = PythonSorceCodeFile; // 指定要执行的Python脚本的路径
            process.StartInfo.UseShellExecute = false; // 不使用Shell执行
            process.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
            process.StartInfo.CreateNoWindow = true; // 不创建新窗口

            process.Start(); // 启动进程

            String output = process.StandardOutput.ReadToEnd(); // 读取标准输出
            process.WaitForExit(); // 等待进程执行完毕
            Int32 processExitCode = process.ExitCode; // 获取进程的退出码
                                    
            return this.Content(output+";"+"进程退出返回代号："+ processExitCode.ToString());
        }
    }

}
