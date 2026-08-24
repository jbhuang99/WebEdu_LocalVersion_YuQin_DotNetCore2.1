using DotLLM.Engine;
using DotLLM.Server;
/**
if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: DotLLM.Sample.Server <model.gguf> [--port 8080]");
    Console.Error.WriteLine("  model.gguf  Path to a GGUF model file");
    Console.Error.WriteLine("  --port N    Port to listen on (default: 8080)");
    return 1;
}

string modelPath = args[0];
int port = 8080;
for (int i = 1; i < args.Length - 1; i++)
{
    if (args[i] == "--port" && int.TryParse(args[i + 1], out var p))
        port = p;
}
**/
String modelPath = "C:\\Users\\1\\.dotllm\\models\\Qwen2.5-1.5B-Instruct-Q8_0.gguf"; // Path to a GGUF model file
Int32 port = 1234; // Port to listen on
ServerOptions serverOptions = new ServerOptions
{
    Model = modelPath,
    Port = port,
    Warmup = WarmupOptions.Disabled,
};

Console.WriteLine($"Loading model: {modelPath}");
//var resolvedPath = ServerStartup.ResolveModelPath(serverOptions.Model, serverOptions.Quant) ?? modelPath;
String? resolvedPath = ServerStartup.ResolveModelPath(serverOptions.Model, serverOptions.Quant) ?? modelPath;
ServerState? serverState = ServerStartup.LoadModel(resolvedPath, serverOptions);
WebApplication? webApplication = ServerStartup.BuildApp(serverState, args, serveUi: true);
String? url = $"http://{serverOptions.Host}:{serverOptions.Port}";
//String? url = $"http://{serverOptions.Host}:{serverOptions.Port}/WebServerFormedYuQinLocalLLMEntry";
Console.WriteLine($"Model: {serverState.Config!.Architecture}, {serverState.Config.NumLayers} layers");
Console.WriteLine($"Server listening on {url}");
Console.WriteLine("Endpoints: /DotLLM/v1/chat/completions, /DotLLM/v1/completions, /DotLLM/v1/models");

webApplication.Run(url);
serverState.Dispose();
return 0;
