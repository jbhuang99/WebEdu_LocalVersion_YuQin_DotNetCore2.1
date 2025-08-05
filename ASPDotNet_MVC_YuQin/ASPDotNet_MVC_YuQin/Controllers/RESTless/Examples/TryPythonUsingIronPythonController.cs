// TryPythonUsingIronPythonController.cs
//IronPython是一个.NET平台之上运行的Python语言平台/Python语言解释器。官方网站（https://ironpython.net/）。可以NuGet包管理IronPython。
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using IronPython.Hosting;
using IronPython.Runtime;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21.Controllers.Examples
{
    public class TryPythonUsingIronPythonController : Controller
    {
        

        public ActionResult Index()
        {
            // 创建Python引擎
            Microsoft.Scripting.Hosting.ScriptEngine pythonEngine = Python.CreateEngine();

            // 执行Python的JIT的源码
            Microsoft.Scripting.Hosting.ScriptScope pythonScope = pythonEngine.CreateScope();
            pythonEngine.ExecuteFile(@"D:\WebEdu_LocalVersion_YuQin\About-Binary_ASM_CPP_CS-FourPlatformsExpansionLanguages(Examples)\Binary_ASM_CPP_Python(Python_ConsoleApplicationK12Edu)\TryWaitForCSCalling.py", pythonScope);

            // 调用Python函数
            dynamic pythonFunction = pythonScope.GetVariable("multiplication");
            dynamic result = pythonFunction(1, 2);

            // 处理Python函数的返回结果
           
            return this.Content(result.ToString()); //来自Python源码的计算结果：2
        }
    }

}
/**上述TryWaitForCSCalling.py的具体Python源码如下：
 #TryWaitForCSCalling.py

def multiplication(a,b):
	return "来自Python源码的计算结果："+str(a*b)

print(multiplication(1,2))

 **/
