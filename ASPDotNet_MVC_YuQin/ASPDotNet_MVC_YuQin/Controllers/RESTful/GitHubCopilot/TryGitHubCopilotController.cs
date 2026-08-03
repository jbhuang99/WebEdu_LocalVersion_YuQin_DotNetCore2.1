using GitHub.Copilot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GitHubCopilot.Controllers.RESTful
{
   
    public class TryGitHubCopilotController : ControllerBase
    {
        
        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public async Task<String> Index()
        {
            // Create and start the Copilot client
            CopilotClient copilotClient = new CopilotClient(
            //When using BYOK, the CLI server may not know which models your provider supports. You can supply a custom onListModels handler at the client level so that client.listModels() returns your provider's models in the standard ModelInfo format. This lets downstream consumers discover available models without querying the CLI.
                new CopilotClientOptions
            {
                OnListModels = (ct) => Task.FromResult<IList<ModelInfo>>(new List<ModelInfo>
    {
        new()
        {
            Id = "my-custom-model",
            Name = "My Custom Model",
            Capabilities = new ModelCapabilities
            {
                Supports = new ModelSupports { Vision = false, ReasoningEffort = false },
                Limits = new ModelLimits { MaxContextWindowTokens = 128000 }
            }
        }
    })
            });
            await copilotClient.StartAsync();

            // Create a session with a specific model
            CopilotSession copilotSession = await copilotClient.CreateSessionAsync(new SessionConfig
            {
                //**********************
                //GitHub-Copilot连接云端LLM
                // Model = "gpt-5 mini",//Your deployment name
                //**********************
                //GitHub-Copilot基于Microsoft Foundry Local连接本机LLM。To get started with Foundry Local(Foundry Local starts on a dynamic port—the port is not fixed. Use foundry service status to confirm the port the service is currently listening on, then use that port in your baseUrl):
                /**
# Windows: Install Foundry Local CLI (requires winget)
winget install Microsoft.FoundryLocal
# List available models
foundry model list

# Run a model (starts the local server automatically)
foundry model run phi-4-mini

# Check the port the service is running on
foundry service status
                **/
                //***********************
                Provider = new ProviderConfig
                {
                    Type = "openai",
                    BaseUrl = "http://127.0.0.1:52412/v1",
                    // No apiKey needed for local Foundry Local
                },
            });

            // Send a prompt and get the response
            //var response = await session.send_and_wait({ "prompt": "解释下量子纠缠"})
            //var response = await copilotSession.SendAsync("Generate a C# method to reverse a string.");
            String response = await copilotSession.SendAsync(
                new MessageOptions
                {
                    //Prompt = "Generate a C# method to reverse a string.",
                    //Prompt = "教育技术定义",
                }
              );

            Console.WriteLine(response);
            return response;
        }
    }
}
