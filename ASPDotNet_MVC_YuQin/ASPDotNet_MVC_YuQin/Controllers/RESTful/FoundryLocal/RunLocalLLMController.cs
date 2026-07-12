using ASPDotNet_MVC_YuQin.FoundryLocalDemo;
using Azure;
using DocumentFormat.OpenXml.Wordprocessing;
using FoundryLocalDemo;
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
            return "已成功运行当前本机LLM，等候客户端Prompt...";
        }
    }
}
