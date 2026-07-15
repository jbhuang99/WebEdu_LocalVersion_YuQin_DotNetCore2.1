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
    public static String sRUL = foundryLocalManager.ServiceUri.ToString(); //目前无法自己指定，可能提示：Service URI is not set. Call StartServiceAsync() first.
    public static String sEndPoint = foundryLocalManager.Endpoint.ToString();// public Uri Endpoint => new Uri(ServiceUri, "/v1");

    public static String sApiKey = foundryLocalManager.ApiKey;//public string ApiKey { get; internal set; } = "OPENAI_API_KEY";
    //public static HttpClient httpClient = foundryLocalManager.HttpClient;//private HttpClient? _serviceClient;
    **/
    public static async Task StartServiceAsync()
    {
        await foundryLocalManager.StartServiceAsync();
        sRUL = foundryLocalManager.ServiceUri.ToString(); /**【获取sURL后，例如http://127.0.0.1:64572/，可以Get/POST实现端点功能如下：
        GET /openai/status(例如http://127.0.0.1:64572/openai/status）、
        GET /foundry/list
        GET /openai/models
        GET /openai/load/{name}
        GET /openai/unload/{name}
        GET /openai/unloadall
        GET /openai/loadedmodels
        GET /openai/etgpudevic
        GET /openai/setgpudevice /{ deviceId}
        POST /v1/chat/completions
        POST /v1/audio/transcriptions
        POST /openai/download
        POST /v1/chat/completions/tokenizer/encode/count】
        例如：POST /v1/chat/completions【例如http://127.0.0.1:64572/v1/chat/completions这一endpoint processes chat completion requests.
It's fully compatible with the OpenAI Chat Completions API.】：
        Request body如下：
        {
    "model": "qwen2.5-0.5b-instruct-generic-cpu",
    "messages": [
      {
        "role": "user",  //The role of the message, always "user" for user messages.The message sender's role. Must be system, user, or assistant.
        "content": "Hello, how are you?" //The content of the message.The actual text of the message.
      }
    ],
    "temperature": 0.7, //(optional)What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
    "top_p": 1, //(optional)An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    "n": 1, //(optional)How many chat completion choices to generate for each input message.
    "stream": false, //(optional)If set, partial message deltas will be sent, like in ChatGPT. Tokens will be sent as data-only server-sent events as they become available, with the stream terminated by a data: [DONE] message.When true, sends partial message responses as server-sent events, ending with a data: [DONE] message.
    "stop": null, //(optional)Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
    "max_tokens": 100, //(optional)The maximum number of tokens to generate in the chat completion. The total length of input tokens and generated tokens is limited by the model's context length.
    "presence_penalty": 0, //(optional)Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.
    "frequency_penalty": 0, //(optional)Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    "logit_bias": {}, //(optional)Modify the likelihood of specified tokens appearing in the completion. Accepts a json object that maps tokens (specified by their token ID in the tokenizer) to an associated bias value from -100 to 100. You can use this to influence the model's output, e.g., to make it more likely to say certain words or phrases.
    "user": "user_id_123", //(optional)A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. Learn more.
    "functions": [],  //(optional)A list of functions the model may generate JSON inputs for. See the function calling guide for more details.
    "function_call": null, //(optional)Controls how the model responds to function calls. "none" means the model does not call any functions, "auto" means the model can decide to call a function, and "function_name" means the model will call the specified function.
    "metadata": {} //(optional)A JSON object containing arbitrary metadata that will be included in the response. This can be used to pass additional information to the model or to track requests and responses.
    //可能还会包含ep (string, optional)、functions、function_call、
Overwrite the provider for ONNX models. Supports: "dml", "cuda", "qnn", "cpu", "webgpu".
  }
     Response body如下：
        {
    "id": "chatcmpl-1234567890",
    "object": "chat.completion", //he object type, always "chat.completion".
    "created": 1677851234, //The Unix timestamp (in seconds) when the completion was created.
    "model": "qwen2.5-0.5b-instruct-generic-cpu",
    "choices": [ //A list of completion choices, each containing a message and other information.
      {
        "index": 0, //The index of the choice in the list of choices.
        "message": {
          "role": "assistant", //The role of the message, always "assistant" for the assistant's response.
          "content": "I'm doing well, thank you! How can I assist you today?" //The content of the assistant's response.The actual generated text.
        },
        "finish_reason": "stop" //The reason why the completion finished. Possible values are "stop" (the model stopped generating text), "length" (the model reached the maximum token limit), or "content_filter" (the model's content filter stopped the generation).Why generation stopped (e.g., "stop", "length", "function_call").
      }
    ],
    "usage": {
      "prompt_tokens": 10,
      "completion_tokens": 20,
      "total_tokens": 30
    }
  }

            **/
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
    //删除模型文件,FoundryLocalManager目前无法实现？Model实现？
    /**
    public static Task DeleteModelAsync(string modelId)
    {
        return foundryLocalManager.DeleteModelAsync(modelId);
    }
    **/
}