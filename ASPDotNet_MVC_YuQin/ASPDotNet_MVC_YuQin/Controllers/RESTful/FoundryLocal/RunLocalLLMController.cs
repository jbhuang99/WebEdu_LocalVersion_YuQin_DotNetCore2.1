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
using static IronPython.Modules._ast;

namespace ASPDotNet_MVC_YuQin.Controllers.FoundryLocalDemo
{
    //[Authorize()]
    public class RunLocalLLMController : ControllerBase
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;

        public RunLocalLLMController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
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
            Console.WriteLine(ExecutionLocalLLM.sRUL +";"+ ExecutionLocalLLM.sEndPoint + ";" + ExecutionLocalLLM.sApiKey + ";" + ExecutionLocalLLM.sRunningModelName);
            var cachedModels = await ExecutionLocalLLM.ListCachedModelsAsync();
            var model = cachedModels.FirstOrDefault(m => m.ModelId == modelName);
            //卸载cachedModels中的所有模型
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
            await LoadModelIntoMemory(model);
            // return this.Request.Query["Model"] ;
            return model.ModelId+"|||"+ExecutionLocalLLM.sRUL;

        }

        /**
        private async Task LoadModelIntoMemory(ModelViewModel model)
        {
            String SelectedModelName = this.Request.Query["Model"];
            if (!model.IsDownloaded || model.IsLoading || model.IsLoaded)
                return;

            try
            {
                model.IsLoading = true;

                if (SelectedModelName != null)
                {
                    try
                    {
                        await ExecutionLocalLLM.UnloadModelAsync(SelectedModelName);
                        await Task.Delay(1000);
                    }
                    catch { }
                }


                await ExecutionLocalLLM.LoadModelAsync(model.Name);

                model.IsLoaded = true;
                model.IsLoading = false;
                SelectedModelName = model.Name;
            }
            catch (Exception ex)
            {
                model.IsLoading = false;
            }
        }
        **/
        private async Task LoadModelIntoMemory(ModelInfo model)
        {
            await ExecutionLocalLLM.LoadModelAsync(model.ModelId); //可能是SDK版本的bug，未能真正装载本机LLM。后续语句装载。
            Console.WriteLine(ExecutionLocalLLM.sRUL + "openai/load/" + model.ModelId);
            //await ExecutionLocalLLM.httpClient.GetAsync(ExecutionLocalLLM.sRUL+"openai/unloadall");
            // await ExecutionLocalLLM.httpClient.GetAsync(ExecutionLocalLLM.sRUL+"openai/load/" + model.ModelId); // GET /openai/load/Phi-4-mini-instruct-generic-cpu?ttl=3600&ep=dml //失败，但在浏览器中访问http://localhost:5000/openai/load/Phi-4-mini-instruct-generic-cpu是成功的，只好转向浏览器端实现。
        }
    }

}
