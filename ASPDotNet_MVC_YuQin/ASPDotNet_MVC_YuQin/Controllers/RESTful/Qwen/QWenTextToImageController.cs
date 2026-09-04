using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.Net.Http.Headers;
using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.QwenTextToImage
{
    public class QWenTextToImageController : ControllerBase
    {
        //通过API使用通义千问 https://help.aliyun.com/zh/dashscope/developer-reference/use-qwen-by-api?spm=a2c4g.11186623.0.0.33b0f97eu68Rxm
        //开通DashScope 模型服务灵积 https://dashscope.console.aliyun.com/overview
        //前往模型广场，选择模型 https://bailian.console.aliyun.com/#/model-market.

        //DashScope中，前往模型广场，选择模型 https://help.aliyun.com/zh/dashscope/developer-reference/model-square/?disableWebsiteRedirect=true //在此选择如下模型为例：通义千问→大语言模型（面向字符生成）。通义千问→通义万相则是面向图像生成。通义千问→通义千问Audio则是面向音频生成。
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;
        private readonly static String _Model = "wan2.6-t2i";
        private static String _ApiKey = "sk-************962"; //敏感数据（在此*号化了，禁止硬编码在C#源码之中）。开发时必须选用“Secret Manager”的secrets.json文件配置。交付时必须选用软件的appsettings.json文件配置。
        private static String _WorkspaceId = "llm-******************";
        public QWenTextToImageController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            if (iWebHostEnvironment.EnvironmentName == "Development")
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["ApiKey"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
                                                     // Console.Write(iWebHostEnvironment.EnvironmentName+ _ApiKey);
                _WorkspaceId = _iConfiguration["WorkspaceId"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取WorkspaceId。
                                                              // Console.Write(iWebHostEnvironment.EnvironmentName+ _WorkspaceId);
            }
            else
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["ApiKey"]; //从交付时的软件的appsettings.json文件获取ApiKey。
                                                     //  Console.Write(iWebHostEnvironment.EnvironmentName + _ApiKey);
                _WorkspaceId = _iConfiguration["WorkspaceId"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取WorkspaceId。
                                                               // Console.Write(iWebHostEnvironment.EnvironmentName+ _WorkspaceId);
            }
        }
        public async Task<String> Index(String queryString)
        {
            return await CallQWen(queryString);
        }

        private static async Task<String> CallQWen(String prompt)
        {
            var postJSON1 = """
            {
              "model":"wan2.6-t2i",
              "input": {
              "messages":[
              {
                "role":"user", 
                "content": [
                                {
                                    "text": 
            """;

            var postJSON2 = """
            }
                            ]
              }
                           ]
              },
              "parameters": {
                    "prompt_extend": false,
                    "watermark": true,
                    "n": 1,
                    "negative_prompt": "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。Low resolution, low image quality, distorted limbs, deformed fingers, oversaturated colors, waxy look, faces lacking detail, overly smooth, AI-like appearance. Chaotic composition. Blurry, distorted text.", 
                    "size": "1280*1280" 
                }
            }
            """;
            var postJSON = postJSON1 +"\""+ prompt +"\"" + postJSON2;
            Console.WriteLine(postJSON);
            using (var client = new HttpClient())
            {
                String llmUrl = "https://" + _WorkspaceId + ".cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation";
                //Console.WriteLine(llmUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, llmUrl);
                //定义Body
                var content = new StringContent(postJSON.ToLower(), Encoding.UTF8, "application/json");
                request.Content = content;

                //定义header
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("通义千问的回答：");
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    Console.WriteLine("请求失败，状态码："+response.StatusCode.ToString());
                    return response.StatusCode.ToString();
                }
            }
        }

    }
}
