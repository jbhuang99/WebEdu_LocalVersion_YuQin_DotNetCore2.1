using DevUIServer.Infrastructure;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.DevUI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
String _ApiKey = "sk-****79316d7b4f3b85014154de41a962"; //敏感数据（在此*号化了，禁止硬编码在C#源码之中）。开发时必须选用“Secret Manager”的secrets.json文件配置。交付时必须选用软件的appsettings.json文件配置。
if (webApplicationBuilder.Environment.IsDevelopment())
{
    //_ApiKey = webApplicationBuilder.Configuration.GetSection("ApiKey-SiliconFlow").Value; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
    _ApiKey = webApplicationBuilder.Configuration.GetSection("ApiKey").Value; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
}
else
{
    //_ApiKey = webApplicationBuilder.Configuration.GetSection("ApiKey-SiliconFlow").Value; //从交付时的软件的appsettings.json文件获取ApiKey。
    _ApiKey = webApplicationBuilder.Configuration.GetSection("ApiKey").Value; //从交付时的软件的appsettings.json文件获取ApiKey。
}
// Step0. Load Configuration
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
    .Build();
//var openAIProvider = config.GetSection("OpenAISiliconFlow").Get<OpenAIProvider>();
var openAIProvider = config.GetSection("OpenAIAliyuncs").Get<OpenAIProvider>();
// Step1. Register one ChatClient
var chatClient = new OpenAIClient(
        //new ApiKeyCredential(openAIProvider.ApiKey),
        new ApiKeyCredential(_ApiKey),
        new OpenAIClientOptions { Endpoint = new Uri(openAIProvider.Endpoint) })
        //new OpenAIClientOptions { BASE_URL = new Uri(openAIProvider.Endpoint) })
    .GetChatClient(openAIProvider.ModelId)
    .AsIChatClient();
Console.WriteLine(_ApiKey + openAIProvider.Endpoint + openAIProvider.ModelId);
webApplicationBuilder.Services.AddChatClient(chatClient);

// Step2. Register some Agents
webApplicationBuilder.AddAIAgent("Assistant", "你是一位乐于助人的助手。回答问题宏观微观迭代准确。");
webApplicationBuilder.AddAIAgent("Poet", "你是一位诗人。使用哲理的诗篇回答所有的请求");
webApplicationBuilder.AddAIAgent("Coder", "你是一位资深的程序员。回答编程问题，并且提供代码示例。");

// Step3. Register one Workflow
var writerAgent = webApplicationBuilder.AddAIAgent("Writer", "你是一位乐于助人的助手，善于宏观微观迭代准确地回答用户提出的问题。");
var reviewerAgent = webApplicationBuilder.AddAIAgent("Reviewer", "你是一位专业评审人员，请协助评审之前的回答。");
webApplicationBuilder.AddWorkflow("TestWorkflow", (sp, key) =>
{
    var aiAgents = new List<IHostedAgentBuilder>()
    {
        writerAgent,
        reviewerAgent
    }
    .Select(hab => sp.GetRequiredKeyedService<AIAgent>(hab.Name));
    return AgentWorkflowBuilder.BuildSequential(
        workflowName: key,
        agents: aiAgents);
}).AddAsAIAgent();

// Step4. Register DevUI related services
webApplicationBuilder.Services.AddOpenAIResponses();
webApplicationBuilder.Services.AddOpenAIConversations();

WebApplication webApplication = webApplicationBuilder.Build();

// Step5. Mapping DevUI related endpoints
webApplication.MapOpenAIResponses();
webApplication.MapOpenAIConversations();
if (webApplication.Environment.IsDevelopment())
{
    // Only use DevUI in development environment
    webApplication.MapDevUI();
}

webApplication.Run();