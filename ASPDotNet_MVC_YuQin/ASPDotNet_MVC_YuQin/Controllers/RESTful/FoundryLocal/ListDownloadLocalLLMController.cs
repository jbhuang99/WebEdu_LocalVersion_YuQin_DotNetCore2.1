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
            Console.WriteLine(ExecutionLocalLLM.sRUL + ";" + ExecutionLocalLLM.sEndPoint + ";" + ExecutionLocalLLM.sApiKey + ";" + ExecutionLocalLLM.sRunningModelName);
            var CatalogModels = await ExecutionLocalLLM.ListCatalogModelsAsync();
            var model = CatalogModels.FirstOrDefault(m => m.ModelId == modelName);
            if (model != null)
            {
                // Step 1: Download the model if not downloaded
               // if (!model.IsDownloaded && !model.IsDownloading)
               if(true)
                {
                    try
                    {
                       // model.IsDownloading = true;
                        //model.DownloadProgress = 0;
                      //  model.DownloadStatus = "Starting download...";
                        Console.WriteLine($"Downloading model: {model.ModelId}...");

                        await foreach (var progress in ExecutionLocalLLM.DownloadModelAsync(modelName))
                        {
                            var progressValue = progress.Percentage;
                           // model.DownloadProgress = progressValue;
                           // model.DownloadStatus = $"Downloading... {progressValue:F1}%";
                            Console.WriteLine($"Downloading {model.ModelId}: {progressValue:F1}%");
                        }

                        //model.IsDownloaded = true;
                       // model.IsDownloading = false;
                        //model.DownloadProgress = 100;
                       // model.DownloadStatus = "Download complete";
                        Console.WriteLine($"Model downloaded: {model.ModelId}");
                        await LoadModelIntoMemory(model);
                        Console.WriteLine($"本机LLM已运行: {model.ModelId}");

                    }
                    catch (Exception ex)
                    {
                       // model.IsDownloading = false;
                      //  model.DownloadProgress = 0;
                       // model.DownloadStatus = "Download failed";
                        Console.WriteLine($"Error downloading model: {ex.Message}");
                       // return "";
                    }
                }
                // Model is currently downloading or loading - show status
                else
                {
                    /**
                    if (model.IsDownloading)
                    {
                        Console.WriteLine($"Model is downloading: {model.ModelId} ({model.DownloadProgress:F1}%)");
                    }
                    else if (model.IsLoading)
                    {
                        Console.WriteLine($"Model is loading into memory: {model.ModelId}");
                    }
                    **/
                }
            }
            return model.ModelId + "|||" + ExecutionLocalLLM.sRUL;
        }

        private async Task LoadModelIntoMemory(ModelInfo model)
        {
            await ExecutionLocalLLM.LoadModelAsync(model.ModelId);
        }
}

}
