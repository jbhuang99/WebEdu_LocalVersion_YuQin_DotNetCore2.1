using Microsoft.AI.Foundry.Local;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using OpenAI;
using System.ClientModel;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;


namespace ASPDotNet_MVC_YuQin.FoundryLocalDemo;

public static class ExecutionLogic
{
    /**

    internal static System.Configuration.Configuration Configuration => _config;
    private static readonly System.Configuration.Configuration _config;
    internal static ILogger Logger => _logger;
    private static readonly ILogger _logger;
    **/
   // private static FoundryLocalManager foundryLocalManager = new FoundryLocalManager(Configuration, Logger);
    private static FoundryLocalManager foundryLocalManager = new FoundryLocalManager();

    public static async Task StartServiceAsync()
    {
        await foundryLocalManager.StartServiceAsync();
    }

    public static Task<List<ModelInfo>> ListCatalogModelsAsync()
    {
        return foundryLocalManager.ListCatalogModelsAsync();
    }

    public static Task<List<ModelInfo>> ListCachedModelsAsync()
    {
        return foundryLocalManager.ListCachedModelsAsync();
    }

    public static IAsyncEnumerable<ModelDownloadProgress> DownloadModelAsync(string modelId)
    {
        return foundryLocalManager.DownloadModelWithProgressAsync(modelId);
    }

    public static Task LoadModelAsync(string modelId)
    {
        return foundryLocalManager.LoadModelAsync(modelId);
    }

    public static Task UnloadModelAsync(string modelId)
    {
        return foundryLocalManager.UnloadModelAsync(modelId);
    }

    public static async IAsyncEnumerable<StudentProfileUpdate> ParseStudentProfileStreamingAsync(string modelId, string userMessage, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var chatClient = new ChatClientBuilder(
                new OpenAIClient(new ApiKeyCredential(foundryLocalManager.ApiKey), new OpenAIClientOptions
                {
                    Endpoint = foundryLocalManager.Endpoint
                })
                .GetChatClient(modelId)
                .AsIChatClient())
            .Build();

        // Create a custom schema with string enums instead of using JSchemaGenerator
        var schema = CreateStudentProfileSchema();

        string prompt = "Parse the provided user text into a JSON object that matches the provided JSON schema. If a property isn't found from the user text, leave it null. Be sure to set the HighSchoolStatus and CitizenshipStatus fields exactly as their enum string values are specified in the schema, do not add spaces or punctuation. Respond ONLY with the JSON object.";
        prompt += "\n\n";
        prompt += "JSON SCHEMA:\n\n";
        prompt += schema;
        prompt += "\n\nUSER TEXT:\n\n";
        prompt += userMessage;

        var streamingResp = chatClient.GetStreamingResponseAsync(prompt, cancellationToken: cancellationToken);

        var respText = "";
        await foreach (var streamResp in streamingResp)
        {
            respText += streamResp.Text;

            if (streamResp.FinishReason == null)
            {
                yield return new StudentProfileUpdate
                {
                    Text = streamResp.Text,
                   // StudentProfile = null // Not finalized yet
                };
            }
        }

       // StudentProfile? parsedProfile = null;
       /**
        try
        {
            // Configure JsonSerializerOptions to handle string enums during deserialization
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            parsedProfile = JsonSerializer.Deserialize<StudentProfile>(ExtractJson(respText), options);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to parse response:\n\n" + respText);
        }

        if (parsedProfile != null)
        {
            yield return new StudentProfileUpdate
            {
                Text = "",
                StudentProfile = parsedProfile
            };
        }
       **/
    }

    /// <summary>
    /// Helper method to ensure clean ouput of JSON from the response text before trying to deserialize it.
    /// </summary>
    /// <param name="respText">Input text from the response.</param>
    /// <returns>Extracted JSON object.</returns>
    private static string ExtractJson(string respText)
    {
        // Handle cleaning up the response to get to the JSON
        respText = respText.Trim();
        if (respText.StartsWith("<think>"))
        {
            respText = respText.Substring("<think>".Length);
            respText = respText.Substring(respText.IndexOf("</think>") + "</think>".Length);
            respText = respText.Trim();
        }
        if (respText.StartsWith("```json"))
        {
            respText = respText.Substring("```json".Length);
            respText = respText.Substring(0, respText.Length - 3);
            respText = respText.Trim();
        }
        if (respText.LastIndexOf("{") != 0)
        {
            // Handle when it returns a nested object
            respText = respText.Substring(1, respText.Length - 2);
            respText = respText.Trim();
        }

        return respText;
    }

    /// <summary>
    /// JSON scheme for the StudentProfile object that we want returned.
    /// </summary>
    /// <returns>string containing JSON schema</returns>
    private static string CreateStudentProfileSchema()
    {
        return """
            {
              "type": "object",
              "properties": {
                "FirstName": {
                  "type": ["string", "null"]
                },
                "LastName": {
                  "type": ["string", "null"]
                },
                "CitizenshipStatus": {
                  "type": ["string", "null"],
                  "enum": [null, "USCitizen", "PermanentResident", "NonResidentAlien", "Other"]
                },
                "SSN": {
                  "type": ["string", "null"]
                },
                "HighSchoolStatus": {
                  "type": ["string", "null"],
                  "enum": [null, "Graduated", "NotGraduated", "GED", "Other"]
                },
                "HasFederalLoanIssues": {
                  "type": ["boolean", "null"]
                },
                "GPA": {
                  "type": ["number", "null"]
                }
              }
            }
            """;
    }


}

/// <summary>
/// Intermediary result of the parsing operation.
/// </summary>
public record StudentProfileUpdate
{
    /// <summary>
    /// The text of this update
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// The final student profile. This will be null if the update is not yet finalized.
    /// </summary>
    //public StudentProfile? StudentProfile { get; set; }
}
