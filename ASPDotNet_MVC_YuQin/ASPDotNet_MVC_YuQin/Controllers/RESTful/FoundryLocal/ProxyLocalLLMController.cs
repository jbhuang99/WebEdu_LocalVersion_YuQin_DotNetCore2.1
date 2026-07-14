using ASPDotNet_MVC_YuQin.FoundryLocalDemo;
using Azure;
using DocumentFormat.OpenXml.ExtendedProperties;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AI.Foundry.Local;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Chat;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;  

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.LocalLLM
{
    [Authorize()]
    public class ProxyLocalLLMController : ControllerBase
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;
       //private readonly static String _RequestUri =""; 
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
       // private static String _Model = "qwen2.5-0.5b-instruct-generic-cpu:4";
        private  static String _Model = ExecutionLocalLLM.sRunningModelName;//不是"OPENAI_API_KEY"，"OPENAI_API_KEY"是APIKEY;由浏览器端返回
        private readonly static String _RequestUri = ExecutionLocalLLM.sRUL;

        // 请求和响应的强类型模型
        public record ChatRequest(List<Message> Messages);
        public record Message(string Role, string Content);

                
        public ProxyLocalLLMController(IHttpClientFactory httpClientFactory, IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            _httpClientFactory = httpClientFactory;
            _Model = Request.Query["Model"];
        }

        public async Task<String> Index(String queryString)
        {
            //_Model = Request.Query["Model"];
            _Model = queryString;
            using (HttpClient httpClient = new HttpClient())
            {
                // 创建模型类
                QianWenRequest qWenRequest = new QianWenRequest
                {
                    Model = _Model,
                    Input = new Input
                    {
                        Prompt = queryString
                    }
                };
                return await CallQWen(queryString, _RequestUri);
            }
            ////return """{ "output":{ "finish_reason":"stop","text":"教育技术的定义随着时间和研究的发展而有所变化，但通常它指的是利用各种技术和工具来提高教学和学习的效果。根据美国教育传播与技术协会（AECT, Association for Educational Communications and Technology）在2005年给出的一个广泛接受的定义，教育技术是“通过创建、使用和管理适当的技术过程和资源来促进学习，并提高绩效的研究与合乎伦理的实践”。\n\n这个定义强调了几个关键点：\n- **创造**：开发新的工具或方法以支持教育。\n- **使用**：有效地应用这些工具和技术于实际的教学过程中。\n- **管理**：确保技术被正确地整合到教育体系中，并且能够持续改进。\n- **促进学习**：最终目标是为了增强学生的学习体验和成果。\n- **提高绩效**：不仅限于学生的学业成绩，还包括教师的教学效率以及整个教育机构的运作效能。\n\n此外，教育技术还涵盖了从传统的教具（如黑板、书籍）到现代信息技术（如互联网、多媒体、虚拟现实等）的应用。随着科技的进步，这一领域也在不断地扩展和发展，比如人工智能、大数据分析等新兴技术正在被越来越多地应用于个性化学习路径设计等方面。"},"usage":{ "input_tokens":13,"output_tokens":248,"prompt_tokens_details":{ "cached_tokens":0},"total_tokens":261},"request_id":"126ff4b5-0493-93da-8898-40646334047b"}""";  
        }

        private static async Task<String> CallQWen(String question,String _RequestUri)
        {
            
            using (var client = new HttpClient())
            {
                // 创建模型类
                var requestObj = new QianWenRequest
                {
                    Model = _Model,
                    Input = new Input
                    {
                        Prompt = question
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

                //var request = new HttpRequestMessage(HttpMethod.Post, _RequestUri);
                var request = new HttpRequestMessage(HttpMethod.Post, _RequestUri);
                //定义Body
                var content = new StringContent(requestJson.ToLower(), Encoding.UTF8, "application/json");
                request.Content = content;

                //定义header
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "OPENAI_API_KEY");

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
                    Console.WriteLine($"请求失败，状态码：{response.StatusCode}");
                    return response.StatusCode.ToString();
                }
            }
        }

    
    public class ProxyQWenQianWenRequest
    {
        public String Model { get; set; }
        public Input Input { get; set; }
    }

    public class ProxyQWenInput
    {
        public string Prompt { get; set; }
    }
        //private CancellationTokenSource? _currentCancellationTokenSource;
        //_currentCancellationTokenSource?.Dispose();
        // _currentCancellationTokenSource = new CancellationTokenSource();
        // var cancellationToken = _currentCancellationTokenSource.Token;
        // ParseStudentProfileStreamingAsync("ModelId","Prompt", cancellationToken);
        
        /**在此已经启动了ExecutionLocalLLM服务，并且已经加载了指定的模型到内存中，所以不需要再重复加载模型。
        var cachedModels = await ExecutionLocalLLM.ListCachedModelsAsync();
        var model = cachedModels.FirstOrDefault(m => m.ModelId == modelName);
        await LoadModelIntoMemory(model);
        **/
                /// <summary>
        /// 处理前端发来的聊天请求，并流式返回 Foundry Local 的回复
        /// </summary>
        [HttpPost]
        public async Task Chat([FromBody] ChatRequest request)
        {
            // 1. 设置 SSE 响应头
            Response.ContentType = "text/event-stream";
            Response.Headers.CacheControl = "no-cache";
            Response.Headers.Connection = "keep-alive";
            // 2. 从配置文件读取 Foundry Local 地址
            String foundryLocalUrl = ExecutionLocalLLM.sEndPoint + "/chat/completions";
            // 3. 构造发给 Foundry Local 的请求体
            var foundryRequestBody = new
            {
                model = Request.Query["Model"], // 替换为你本地加载的模型别名
                messages = request.Messages,
                stream = true
            };

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(foundryRequestBody);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                // 4. 发起请求
                var client = _httpClientFactory.CreateClient();
                using var foundryResponse = await client.PostAsync(foundryLocalUrl, httpContent);

                if (!foundryResponse.IsSuccessStatusCode)
                {
                    var errorContent = await foundryResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Foundry Local Error: {foundryResponse.StatusCode}, {errorContent}");
                }

                // 5. 读取并转发流式数据
                await using var stream = await foundryResponse.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    if (line.StartsWith("data: "))
                    {
                        var dataJson = line.Substring("data: ".Length);
                        if (dataJson.Trim() == "[DONE]") break;

                        try
                        {
                            using var doc = JsonDocument.Parse(dataJson);
                            var root = doc.RootElement;
                            if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                            {
                                var delta = choices[0].GetProperty("delta");
                                if (delta.TryGetProperty("content", out var content))
                                {
                                    var contentValue = content.GetString();
                                    var responseData = System.Text.Json.JsonSerializer.Serialize(new { content = contentValue });
                                    await Response.WriteAsync($"data: {responseData}\n\n");
                                    await Response.Body.FlushAsync();
                                }
                            }
                        }
                        catch (System.Text.Json.JsonException) { /* 忽略解析错误 */ }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorResponse = System.Text.Json.JsonSerializer.Serialize(new { error = ex.Message });
                await Response.WriteAsync($"data: {errorResponse}\n\n");
                await Response.Body.FlushAsync();
            }
        }
    }
}
   
/**
 // <complete_code>
// <imports>
using Microsoft.AI.Foundry.Local;
using Betalgo.Ranul.OpenAI.ObjectModels.RequestModels;
using Microsoft.Extensions.Logging;
// </imports>

// <init>
CancellationToken ct = CancellationToken.None;

var config = new Configuration
{
    AppName = "foundry_local_samples",
    LogLevel = Microsoft.AI.Foundry.Local.LogLevel.Information
};

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
});
var logger = loggerFactory.CreateLogger<Program>();

// Initialize the singleton instance
await FoundryLocalManager.CreateAsync(config, logger);
var mgr = FoundryLocalManager.Instance;

// Download and register all execution providers.
var currentEp = "";
await mgr.DownloadAndRegisterEpsAsync((epName, percent) =>
{
    if (epName != currentEp)
    {
        if (currentEp != "") Console.WriteLine();
        currentEp = epName;
    }
    Console.Write($"\r  {epName.PadRight(30)}  {percent,6:F1}%");
});
if (currentEp != "") Console.WriteLine();

// Select and load a model from the catalog
var catalog = await mgr.GetCatalogAsync();
var model = await catalog.GetModelAsync("qwen2.5-0.5b")
    ?? throw new Exception("Model not found");

await model.DownloadAsync(progress =>
{
    Console.Write($"\rDownloading model: {progress:F2}%");
    if (progress >= 100f) Console.WriteLine();
});

await model.LoadAsync();
Console.WriteLine("Model loaded and ready.");

// Get a chat client
var chatClient = await model.GetChatClientAsync();
// </init>

// <system_prompt>
// Start the conversation with a system prompt
var messages = new List<ChatMessage>
{
    new ChatMessage
    {
        Role = "system",
        Content = "You are a helpful, friendly assistant. Keep your responses " +
                  "concise and conversational. If you don't know something, say so."
    }
};
// </system_prompt>

Console.WriteLine("\nChat assistant ready! Type 'quit' to exit.\n");

// <conversation_loop>
while (true)
{
    Console.Write("You: ");
    var userInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(userInput) ||
        userInput.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
        userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    // Add the user's message to conversation history
    messages.Add(new ChatMessage { Role = "user", Content = userInput });

    // <streaming>
    // Stream the response token by token
    Console.Write("Assistant: ");
    var fullResponse = string.Empty;
    var streamingResponse = chatClient.CompleteChatStreamingAsync(messages, ct);
    await foreach (var chunk in streamingResponse)
    {
        var content = chunk.Choices[0].Message.Content;
        if (!string.IsNullOrEmpty(content))
        {
            Console.Write(content);
            Console.Out.Flush();
            fullResponse += content;
        }
    }
    Console.WriteLine("\n");
    // </streaming>

    // Add the complete response to conversation history
    messages.Add(new ChatMessage { Role = "assistant", Content = fullResponse });
}
// </conversation_loop>

// Clean up - unload the model
await model.UnloadAsync();
Console.WriteLine("Model unloaded. Goodbye!");
// </complete_code>

 * */
