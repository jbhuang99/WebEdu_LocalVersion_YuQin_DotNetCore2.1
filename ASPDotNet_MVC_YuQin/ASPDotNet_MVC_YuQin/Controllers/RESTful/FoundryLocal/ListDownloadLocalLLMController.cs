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

namespace ASPDotNet_MVC_YuQin.Controllers.FoundryLocalDemo
{
    //[Authorize()]
    public class ListDownloadLocalLLMController : ControllerBase
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;

        public ListDownloadLocalLLMController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
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
            //ExecutionLocalLLM.sRunningModelName = modelName;//已经不需要从浏览器端传入，保存在服务端ExecutionLocalLLM了。
            ExecutionLocalLLM.sRunningModelName = modelName;

            await ExecutionLocalLLM.StartServiceAsync();
            // Console.WriteLine(ExecutionLocalLLM.sRUL + ";" + ExecutionLocalLLM.sEndPoint + ";" + ExecutionLocalLLM.sApiKey + ";" );
            Console.WriteLine(ExecutionLocalLLM.sRUL + ";" + ExecutionLocalLLM.sEndPoint + ";" + ExecutionLocalLLM.sApiKey + ";" + ExecutionLocalLLM.sRunningModelName);
            var CatalogModels = await ExecutionLocalLLM.ListCatalogModelsAsync();
            var model = CatalogModels.FirstOrDefault(m => m.ModelId == modelName);
            //卸载cachedModels中的所有模型
            /**
            foreach (var cachedModel in cachedModels)
            {
                if (cachedModel.ModelId != modelName)
                {
                    try
                    {
                        await ExecutionLocalLLM.UnloadModelAsync(cachedModel.ModelId);
                        await Task.Delay(1000);
                    }
                    catch { }
                }
            }  
            **/
            // return this.Request.Query["Model"] ;
            return model.ModelId + "|||" + ExecutionLocalLLM.sRUL;

        }
    }

}
