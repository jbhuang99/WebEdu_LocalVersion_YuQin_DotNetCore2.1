using Azure;
//using AgentSkillDemo.Infrastructure;
using Microsoft.Agents.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNet_MVC_YuQin.Controllers.RESTful.QwenSkillsTry
{
    [Authorize()]
    //目前使用的不是案例云而是硅基流动（SiliconFlow）的通义千问模型，后续会增加对Azure OpenAI服务的支持。硅基流动（SiliconFlow）是阿里云旗下的AI即服务平台，提供了丰富的AIGC模型和工具，开发者可以通过API调用来使用这些模型进行文本生成、图像生成、音频生成等任务。通义千问是硅基流动提供的一款大语言模型，具有强大的自然语言理解和生成能力，可以用于构建智能问答系统、对话系统等应用。
    public class QWenSkillsTryController : ControllerBase
    {
        //通过API使用通义千问 https://help.aliyun.com/zh/dashscope/developer-reference/use-qwen-by-api?spm=a2c4g.11186623.0.0.33b0f97eu68Rxm
        //开通DashScope 模型服务灵积 https://dashscope.console.aliyun.com/overview
        //前往模型广场，选择模型 https://bailian.console.aliyun.com/#/model-market.

        //DashScope中，前往模型广场，选择模型 https://help.aliyun.com/zh/dashscope/developer-reference/model-square/?disableWebsiteRedirect=true //在此选择如下模型为例：通义千问→大语言模型（面向字符生成）。通义千问→通义万相则是面向图像生成。通义千问→通义千问Audio则是面向音频生成。
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        private readonly IConfiguration _iConfiguration;
        private readonly static String _RequestUri = "https://api.siliconflow.cn"; //通过DashScope的HTTP方式进行调用，需要配置的完整访问endpoint。POST https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation


        private readonly static String _Model = "Qwen/Qwen3.5-122B-A10B";

        //DashScope中，生成API-KEY. https://cloud.siliconflow.cn/。

        internal static String _ApiKey = "sk-*********************lnweddntqyrvpfvspcqtlzpfxfg"; //敏感数据（在此*号化了，禁止硬编码在C#源码之中）。开发时必须选用“Secret Manager”的secrets.json文件配置。交付时必须选用软件的appsettings.json文件配置。

        public QWenSkillsTryController(IWebHostEnvironment iWebHostEnvironment, IConfiguration iConfiguration)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iConfiguration = iConfiguration;
            if (iWebHostEnvironment.EnvironmentName == "Development")
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["ApiKey-SiliconFlow"]; //从开发时的本项目的“Secret Manager”的secrets.json文件获取ApiKey。
                //Console.WriteLine(iWebHostEnvironment.EnvironmentName+ _ApiKey);
            }
            else
            {
                //_ApiKey = ConfigurationManager.AppSettings["ApiKey"];
                _ApiKey = _iConfiguration["???????"]; //从交付时的软件的appsettings.json文件获取ApiKey。
                //Console.WriteLine(iWebHostEnvironment.EnvironmentName + _ApiKey);
            }
        }
        public async Task<String> Index(String queryString)
        {
            // Step0 — 创建 ChatClient
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.Secrets.json", optional: true, reloadOnChange: true)
                .Build();
            var openAIProvider = config.GetSection("OpenAI").Get<OpenAIProvider>();
            var chatClient = new OpenAIClient(
                    //new ApiKeyCredential(openAIProvider.ApiKey),
                    new ApiKeyCredential(new OpenAIProvider().ApiKey),
                    new OpenAIClientOptions { Endpoint = new Uri(openAIProvider.Endpoint) })
                .GetChatClient(openAIProvider.ModelId);
            
            // Step1 创建 SkillsProvider — 从文件夹加载 Skills
#pragma warning disable MAAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            FileAgentSkillsProvider skillsProvider = new FileAgentSkillsProvider(
    skillPath: Path.Combine(Directory.GetCurrentDirectory(), "skills")
);
#pragma warning restore MAAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            Console.WriteLine("已从skills文件夹加载Skills");
            Console.WriteLine();

            // Step2 创建 Agent — 注入 SkillsProvider
            AIAgent agent = chatClient.AsAIAgent(new ChatClientAgentOptions
            {
                Name = "SingleSkillAgent",
                ChatOptions = new()
                {
                    Instructions = "你是一个高效的企业助手，使用和用户同样的语言回答用户提出的问题。",
                },
                // 🔑 关键：通过 AIContextProviders 注入 SkillsProvider
                AIContextProviders = [skillsProvider],
            });
            Console.WriteLine("Agent创建成功");
            Console.WriteLine();
            // Step3 开始测试 — 调用Skills来回答问题
            Console.WriteLine(" 测试1：报销政策FAQ");
            Console.WriteLine();
            // 提出关于小费报销的问题
            var question = "小费可以报销吗？我在一次千问打车给了10%的小费，想咨询是否可以报销？";
            Console.WriteLine($"👤 用户: {question}");
            Console.WriteLine();
            // Agent 会自动执行渐进式披露流程：
            // 1. 识别属于 expense-report 领域
            // 2. 调用 load_skill 获取完整规则
            // 3. 调用 read_skill_resource 获取 FAQ
            // 4. 根据 FAQ 内容回答
            var response1 = await agent.RunAsync(question);
            Console.WriteLine($"Agent: {response1.Text}");
            Console.WriteLine();

            Console.WriteLine(" 测试 2：多轮对话生成报销报告");
            Console.WriteLine();
            // 使用 AgentSession 支持多轮对话
            var session = await agent.CreateSessionAsync();
            // 第 1 轮：提供费用信息，要求生成报告草稿
            var request = "我上周有2笔客户用餐费用。请先返回一份报销填单草稿，并想询问我缺失的报销相关细节信息。";
            Console.WriteLine($"用户: {request}");
            Console.WriteLine();
            // Agent 会：
            // 1. 加载 expense-report skill（如果尚未加载）
            // 2. 读取报销模板 assets/expense-report-template.md
            // 3. 根据用户提供的信息填写模板
            // 4. 识别缺失字段并主动询问
            var response2 = await agent.RunAsync(request, session);
            Console.WriteLine($"Agent: {response2.Text}");
            // 第 2 轮：提供详细信息，让 Agent 完善报销单。
            var details = """
以下是详细信息：
- 客户中餐：周三在大排档，150元，3 人（我、A公司的李先生、B公司的张先生）。业务目的：新项目合作交流。
- 客户晚餐：周五在赣菜春天，300元，2 人（我、D公司的黄先生）。业务目的：合同续签交流。
""";
            Console.WriteLine($"用户: {details}");
            Console.WriteLine();
            // Agent 利用 session 中的对话历史，更新报告草稿
            var response3 = await agent.RunAsync(details, session);
            Console.WriteLine($"Agent: {response3.Text}");
            Console.WriteLine();

            Console.WriteLine("再见！");
            return "第1轮提问是："+ question+ "第1轮提问是：" + response1.Text + "\n" + "第2轮提问是："+ request+"第2轮提问的回答是：" +"\n"+ response2.Text+ "第3轮的提问是"+ details+ "\n" + "第3轮提问的回答是：" + response3.Text;
        }
    }
    public sealed class OpenAIProvider
    {
        public string ApiKey { get; init; } = QWenSkillsTryController._ApiKey;
        public string ModelId { get; init; } = string.Empty;
        public string Endpoint { get; init; } = string.Empty;
    }
}
