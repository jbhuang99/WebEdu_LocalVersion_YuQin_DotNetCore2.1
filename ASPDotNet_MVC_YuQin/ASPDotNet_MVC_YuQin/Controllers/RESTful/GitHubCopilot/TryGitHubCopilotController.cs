using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using GitHub.Copilot;
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
            CopilotClient copilotClient = new CopilotClient();
            await copilotClient.StartAsync();

            // Create a session with a specific model
            CopilotSession copilotSession = await copilotClient.CreateSessionAsync(new SessionConfig
            {
                Model = "gpt-5 mini"
            });

            // Send a prompt and get the response
            //var response = await session.send_and_wait({ "prompt": "解释下量子纠缠"})
            String response = await copilotSession.SendAsync("Generate a C# method to reverse a string.");

            Console.WriteLine(response);
            return response;
        }
    }
}
