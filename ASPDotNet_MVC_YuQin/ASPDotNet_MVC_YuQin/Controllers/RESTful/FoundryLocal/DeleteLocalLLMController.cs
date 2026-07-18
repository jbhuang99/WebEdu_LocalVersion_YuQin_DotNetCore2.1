using ASPDotNet_MVC_YuQin.FoundryLocalDemo;
using Azure;
using DocumentFormat.OpenXml.Wordprocessing;
using FoundryLocalDemo;
using Microsoft.AI.Foundry.Local;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ASPDotNet_MVC_YuQin.Controllers.FoundryLocalDemo
{
    //[Authorize()]
    public class DeleteLocalLLMController : ControllerBase
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;

        public DeleteLocalLLMController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            if (iWebHostEnvironment.EnvironmentName == "Development")
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                //_ApiKey = _iConfiguration["ApiKey"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
                // Console.Write(iWebHostEnvironment.EnvironmentName+ _ApiKey);
            }
            else
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                // _ApiKey = _iConfiguration["ApiKey"]; //从交付时的软件的appsettings.json文件获取ApiKey。
                //  Console.Write(iWebHostEnvironment.EnvironmentName + _ApiKey);
            }
        }
        public async Task<string> Index()
        {
            String modelName = this.Request.Query["Model"];
            String localLLMDirPath = this.Request.Query["localLLMDirPath"];
            String sReturnedTemp = "";
            Console.WriteLine(modelName + " localLLMDirPath=" + localLLMDirPath);
            //ExecutionLocalLLM.sRunningModelName = modelName;//已经不需要从浏览器端传入，保存在服务端ExecutionLocalLLM了。

            string rootPath = localLLMDirPath;//例如 @"C:\Users\1\.foundry\cache\models";
            string targetFolderName = modelName;//例如"qwen2.5-coder-0.5b-instruct-generic-cpu:4";

            try
            {
                // 1. 验证根目录是否存在
                if (!Directory.Exists(rootPath))
                {
                    sReturnedTemp = $"❌ 根目录不存在: {rootPath}";
                    Console.WriteLine(sReturnedTemp);
                    return sReturnedTemp;
                }

                // 2. 获取所有子目录（仅第一层，不递归）
                //DirectoryInfo rootDir = new DirectoryInfo(rootPath);
                //DirectoryInfo[] subDirs = rootDir.GetDirectories();
                // 2.也可以使用以下方式获取所有子目录（递归）：
                DirectoryInfo rootDir = new DirectoryInfo(rootPath);
                DirectoryInfo[] subDirs = rootDir.GetDirectories("*", SearchOption.AllDirectories);
                bool found = false;
                foreach (DirectoryInfo subDir in subDirs)
                {
                    // 3. 精确匹配文件夹名称
                    if (subDir.Name == targetFolderName)
                    {
                        sReturnedTemp = $"🔍 找到目标文件夹: {subDir.FullName}";
                        Console.WriteLine(sReturnedTemp);
                        found = true;

                        // 4. 删除文件夹（true 表示递归删除子文件和子目录）
                        subDir.Delete(true);
                        sReturnedTemp = $"✅ 已删除: {subDir.FullName}";    
                        Console.WriteLine(sReturnedTemp);
                    }
                }

                if (!found)
                {
                    sReturnedTemp = $"⚠️ 未找到名为 \"{targetFolderName}\" 的文件夹";
                    Console.WriteLine(sReturnedTemp);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                sReturnedTemp = $"🔒 权限不足，无法访问或删除: {ex.Message}";
                Console.WriteLine(sReturnedTemp);
            }
            catch (IOException ex)
            {
                sReturnedTemp = $"💾 IO 错误: {ex.Message}";
                Console.WriteLine(sReturnedTemp);
            }
            catch (Exception ex)
            {
                sReturnedTemp = $"❌ 未知错误: {ex.Message}";
                Console.WriteLine(sReturnedTemp);
            }
            //return model.ModelId + "|||" + localLLMDirPath;
            return (sReturnedTemp);

        }
    }

}
