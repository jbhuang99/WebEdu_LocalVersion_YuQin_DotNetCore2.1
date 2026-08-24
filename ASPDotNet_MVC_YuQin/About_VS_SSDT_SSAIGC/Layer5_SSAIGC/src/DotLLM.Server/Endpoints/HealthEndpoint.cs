using DotLLM.Server.Models;

namespace DotLLM.Server.Endpoints;

/// <summary>
/// GET /DotLLM/health and /DotLLM/ready — health and readiness probes.
/// </summary>
public static class HealthEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/DotLLM/health", () => Results.Ok(new StatusResponse { Status = "ok" }));

        app.MapGet("/DotLLM/ready", (ServerState state) =>
            state.IsReady
                ? Results.Ok(new StatusResponse { Status = "ready" })
                : Results.StatusCode(503));
    }
}
