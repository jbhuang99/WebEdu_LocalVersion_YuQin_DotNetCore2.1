using AliImageGen.Models;
using AliImageGen.Services;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AliImageGen.Models
{
    // ================= 请求模型 =================
    public class DashScopeImageRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "wanx-v1";

        [JsonPropertyName("input")]
        public DashScopeInput Input { get; set; } = new();

        [JsonPropertyName("parameters")]
        public DashScopeParameters Parameters { get; set; } = new();
    }

    public class DashScopeInput
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = "";

        [JsonPropertyName("negative_prompt")]
        public string? NegativePrompt { get; set; } // 反向提示词（可选）
    }

    public class DashScopeParameters
    {
        [JsonPropertyName("size")]
        public string Size { get; set; } = "1024*1024"; // 支持 1024*1024, 720*1280 等

        [JsonPropertyName("n")]
        public int N { get; set; } = 1; // 生成数量

        [JsonPropertyName("style")]
        public string? Style { get; set; } // 风格（可选，如 "<auto>"）
    }

    // ================= 响应模型 =================
    public class DashScopeTaskResponse
    {
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; } = "";

        [JsonPropertyName("output")]
        public DashScopeOutput Output { get; set; } = new();
    }

    public class DashScopeOutput
    {
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; } = "";

        [JsonPropertyName("task_status")]
        public string TaskStatus { get; set; } = ""; // PENDING, RUNNING, SUCCEEDED, FAILED

        [JsonPropertyName("results")]
        public List<DashScopeResult>? Results { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; } // 失败时的错误信息
    }

    public class DashScopeResult
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = "";
    }
}

namespace AliImageGen.Services
{
    public interface IAliImageGenerationService
    {
        Task<List<string>> GenerateImageAsync(string prompt, string size = "1024*1024", int count = 1);
    }

    public class AliImageGenerationService : IAliImageGenerationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AliImageGenerationService> _logger;

        public AliImageGenerationService(HttpClient httpClient, IConfiguration configuration, ILogger<AliImageGenerationService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            // 配置 HttpClient 基础 Headers
            var apiKey = _configuration["AliDashScope:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _httpClient.DefaultRequestHeaders.Add("X-DashScope-Async", "enable"); // 必须开启异步
        }

        public async Task<List<string>> GenerateImageAsync(string prompt, string size = "1024*1024", int count = 1)
        {
            var baseUrl = _configuration["AliDashScope:BaseUrl"];
            var model = _configuration["AliDashScope:Model"] ?? "wanx-v1";

            // 1. 构建请求体
            var requestPayload = new DashScopeImageRequest
            {
                Model = model,
                Input = new DashScopeInput { Prompt = prompt },
                Parameters = new DashScopeParameters { Size = size, N = count }
            };

            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // 2. 提交任务
            var submitUrl = $"{baseUrl}/services/aigc/image-generation/generation";
            var submitResponse = await _httpClient.PostAsync(submitUrl, httpContent);
            var submitResultStr = await submitResponse.Content.ReadAsStringAsync();

            if (!submitResponse.IsSuccessStatusCode)
                throw new Exception($"提交任务失败: {submitResultStr}");

            var submitResult = Newtonsoft.Json.JsonConvert.DeserializeObject<DashScopeTaskResponse>(submitResultStr);
            var taskId = submitResult?.Output?.TaskId;

            if (string.IsNullOrEmpty(taskId))
                throw new Exception("未能获取 TaskId");

            _logger.LogInformation($"任务已提交，TaskId: {taskId}，开始轮询...");

            // 3. 轮询任务状态
            return await PollTaskResultAsync(baseUrl, taskId);
        }

        private async Task<List<string>> PollTaskResultAsync(string baseUrl, string taskId)
        {
            var queryUrl = $"{baseUrl}/tasks/{taskId}";
            var maxRetries = 60; // 最多轮询 60 次 (约 2 分钟)
            var delaySeconds = 2;

            for (int i = 0; i < maxRetries; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));

                var request = new HttpRequestMessage(HttpMethod.Get, queryUrl);
                // 查询任务状态时不需要 X-DashScope-Async header
                var response = await _httpClient.SendAsync(request);
                var resultStr = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DashScopeTaskResponse>(resultStr);

                var status = result?.Output?.TaskStatus;
                _logger.LogInformation($"轮询状态: {status}");

                if (status == "SUCCEEDED")
                {
                    return result!.Output!.Results!.Select(r => r.Url).ToList();
                }
                else if (status == "FAILED")
                {
                    throw new Exception($"图像生成失败: {result?.Output?.Message}");
                }
                // PENDING 或 RUNNING 则继续循环等待
            }

            throw new TimeoutException("图像生成超时，请稍后通过 TaskId 手动查询或重试。");
        }
    }
}

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Qwen
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ProxyQWenTextToImageController : ControllerBase
    {

        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public String Get()
        {
            return "Try HomeAPIAPI";
        }
    }
    /**
   [ApiController]
   [Route("api/[controller]")]
   public class ProxyQWenTextToImageController : ControllerBase
   {
       private readonly IAliImageGenerationService _imageService;

       public QWenTextToImageController(IAliImageGenerationService imageService)
       {
           _imageService = imageService;
       }
       [HttpPost("generate")]

       public async Task<IActionResult> GenerateImage([FromBody] ImageRequestDto dto)
       {
           try
           {
               // 调用 Service 生成图像
               var imageUrls = await _imageService.GenerateImageAsync(
                   prompt: dto.Prompt,
                   size: dto.Size ?? "1024*1024",
                   count: dto.Count
               );

               return Ok(new { success = true, urls = imageUrls });
           }
           catch (Exception ex)
           {
               return BadRequest(new { success = false, error = ex.Message });
           }
       }       
   }
    **/
    public class ImageRequestDto
    {
        public string Prompt { get; set; } = "一只戴着墨镜的赛博朋克风格的猫";
        public string? Size { get; set; } = "1024*1024";
        public int Count { get; set; } = 1;
    }
}

/**
namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.Qwen
{
    public class QWenTextToImageController : ControllerBase
    {
        //通过API使用通义千问 https://help.aliyun.com/zh/dashscope/developer-reference/use-qwen-by-api?spm=a2c4g.11186623.0.0.33b0f97eu68Rxm
        //开通DashScope 模型服务灵积 https://dashscope.console.aliyun.com/overview
        //前往模型广场，选择模型 https://bailian.console.aliyun.com/#/model-market.

        //DashScope中，前往模型广场，选择模型 https://help.aliyun.com/zh/dashscope/developer-reference/model-square/?disableWebsiteRedirect=true //在此选择如下模型为例：通义千问→大语言模型（面向字符生成）。通义千问→通义万相则是面向图像生成。通义千问→通义千问Audio则是面向音频生成。
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;
        private readonly static String _RequestUri = "https://dashscope.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis";     //通过DashScope的HTTP方式进行调用，需要配置的完整访问endpoint。POST https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation

        //private readonly static String _Model = "ImageSynthesis.Models.wanx_v1";
        private readonly static String _Model = "wanx-v1";

        //DashScope中，生成API-KEY. https://dashscope.console.aliyun.com/apiKey //在此如下apikey为例。请替换为您的阿里云密钥信息。https://account.aliyun.com/login/login.htm登录申请。https://help.aliyun.com/zh/dashscope/developer-reference/acquisition-and-configuration-of-api-key?spm=a2c4g.11186623.0.0.4df8694e30GuAN申请。

        private static String _ApiKey = "sk-****79316d7b4f3b85014154de41a962"; //敏感数据（在此*号化了，禁止硬编码在C#源码之中）。开发时必须选用“Secret Manager”的secrets.json文件配置。交付时必须选用软件的appsettings.json文件配置。
        public QWenTextToImageController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            if (iWebHostEnvironment.EnvironmentName == "Development")
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["ApiKey"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
            }
            else
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["ApiKey"]; //从交付时的软件的appsettings.json文件获取ApiKey。
            }
        }
        public async Task<Byte[]> Index(String queryString)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // 创建模型类
                QianWenTextToImageRequest qWenRequest = new QianWenTextToImageRequest
                {
                    model = _Model,                    
                    input = new InputTextToImage
                    {
                        prompt = queryString
                    },
                    parameters = new Parameters
                    {
                        style = "<sketch>",
                        size = "1024*1024",
                        n = 4,
                        seed = 42,
                        strength = 0.5F,
                        ref_mode = "repaint"
                    }
                };
                return await CallQWen(queryString);
            }
        }

        private static async Task<Byte[]> CallQWen(String question)
        {
            using (var client = new HttpClient())
            {
                // 创建模型类
                var requestObj = new QianWenTextToImageRequest
                {
                    model = _Model,
                    input = new InputTextToImage
                    {
                        prompt = question
                    },
                    parameters = new Parameters
                    {
                        style="<sketch>", 
                        size= "1024*1024",
                        n=4,
                        seed=42,
                        strength=0.5F,
                        ref_mode="repaint"
                    }
                };

                var settings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                };

                // 将对象序列化为JSON字符串
                string requestJson = JsonConvert.SerializeObject(requestObj, settings);
                Console.WriteLine(requestJson);

                var request = new HttpRequestMessage(HttpMethod.Post, _RequestUri);
                //定义Body
                var content = new StringContent(requestJson.ToLower(), Encoding.UTF8, "application/json");
                request.Content = content;

                //定义header
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                 
                request.Headers.Add("X-DashScope-AsyncValue", "enable");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{_ApiKey}");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsByteArrayAsync();

                    Console.WriteLine("通义万相的回答：");
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    Console.WriteLine($"请求失败，状态码：{response.StatusCode}");
                    var responseBody = await response.Content.ReadAsByteArrayAsync();

                    Console.WriteLine("通义万相的回答：");
                    Console.WriteLine(responseBody);
                    return responseBody;
                    //return response.StatusCode.ToString();
                }
            }
        }


        }
    public class QianWenTextToImageRequest
    {
        public String model { get; set; }
        public InputTextToImage input { get; set; }
        public Parameters parameters { get; set; }

    }

    public class InputTextToImage
    {
        public String prompt { get; set; }
    }

    public class Parameters
    {
        public String style { get; set; } //例如 "<sketch>",
        public String size { get; set; } //例如 "size": "1024*1024",
        public Int32 n { get; set; } //例如 "n":4, 
        public Int32 seed { get; set; }  //例如"seed":42,
        public Single strength { get; set; } //例如"strength": 0.5,
        public String ref_mode { get; set; } // 例如"ref_mode": "repaint"
    }
}
**/
