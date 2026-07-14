using Aop.Api.Domain;
using Microsoft.AI.Foundry.Local;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using OpenAI;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;


namespace ASPDotNet_MVC_YuQin.FoundryLocalDemo;

public static class ExecutionLocalLLM
{
    /**

    internal static System.Configuration.Configuration Configuration => _config;
    private static readonly System.Configuration.Configuration _config;
    internal static ILogger Logger => _logger;
    private static readonly ILogger _logger;
    **/
    // private static FoundryLocalManager foundryLocalManager = new FoundryLocalManager(Configuration, Logger);
    private static FoundryLocalManager foundryLocalManager = new FoundryLocalManager();
    public static String sRUL = "";
    public static String sEndPoint = "";
    public static String sApiKey = "";
    public static String sRunningModelName = "";//无法从FoundryLocalManager获取，需要从模型运行时获取。
    /**
    public static String sRUL = foundryLocalManager.ServiceUri.ToString(); //目前无法自己指定，可能提示：Service URI is not set. Call StartServiceAsync() first.【获取sURL后，可以如下：//GET /openai/status(例如http://127.0.0.1:64572/openai/status）、POST /v1/chat/completions、 POST /v1/audio/transcriptions、GET /foundry/list、GET /openai/models、POST /openai/download、GET /openai/load/{name}、GET /openai/unload/{name}、GET/openai/unloadall、GET /openai/loadedmodels、GET /openai/etgpudevice、GET /openai/setgpudevice /{ deviceId}、POST / v1/chat/completions/tokenizer/encode/count】
    public static String sEndPoint = foundryLocalManager.Endpoint.ToString();// public Uri Endpoint => new Uri(ServiceUri, "/v1");

    public static String sApiKey = foundryLocalManager.ApiKey;//public string ApiKey { get; internal set; } = "OPENAI_API_KEY";
    //public static HttpClient httpClient = foundryLocalManager.HttpClient;//private HttpClient? _serviceClient;
    **/
    public static async Task StartServiceAsync()
    {
        await foundryLocalManager.StartServiceAsync();
        sRUL = foundryLocalManager.ServiceUri.ToString(); //目前无法自己指定，可能提示：Service URI is not set. Call StartServiceAsync() first.【获取sURL后，可以如下：//GET /openai/status(例如http://127.0.0.1:64572/openai/status）、POST /v1/chat/completions、 POST /v1/audio/transcriptions、GET /foundry/list、GET /openai/models、POST /openai/download、GET /openai/load/{name}、GET /openai/unload/{name}、GET/openai/unloadall、GET /openai/loadedmodels、GET /openai/etgpudevice、GET /openai/setgpudevice /{ deviceId}、POST / v1/chat/completions/tokenizer/encode/count】
        sEndPoint = foundryLocalManager.Endpoint.ToString();
        sApiKey = foundryLocalManager.ApiKey;////本地运行通常不需要真实 ApiKey，但 SDK 会提供一个占位符.          
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

}