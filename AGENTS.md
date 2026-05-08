# AGENTS.md

## Purpose

This file provides focused, actionable instructions for automated coding agents working on this repository. It complements the human README by listing project locations, commands, and conventions agents need.

## Project snapshot (relevant projects)

- ASPDotNet_MVC_YuQin - web application (ASP.NET Core MVC + Razor Pages)
  - path: ASPDotNet_MVC_YuQin\ASPDotNet_MVC_YuQin.csproj
- Blazor_WebAssembly_PWA_YuQin - Blazor WebAssembly PWA (client-side)
  - path: ASPDotNet_MVC_YuQin\Blazor_WebAssembly_PWA_YuQin\Blazor_WebAssembly_PWA_YuQin.csproj
- SharedRazor_SharedCSHTML - Razor class library
  - path: ASPDotNet_MVC_YuQin\SharedRazor_SharedCSHTML\SharedRazorClassLibrary\SharedRazorCS.csproj
- SharedCS - shared C# helpers
  - path: ASPDotNet_MVC_YuQin\SharedCS\SharedCS\SharedCS.csproj

Primary technologies: C# 14, .NET 10, ASP.NET Core, Blazor WebAssembly, Razor Pages, Entity Framework Core

## Environment and prerequisites

- Required SDK: .NET 10. Verify with: `dotnet --list-sdks` and `dotnet --version`
- Recommended IDE: Microsoft Visual Studio Community 2026 (18.5.2) for full debugging support
- Preferred shell for scripts/commands: PowerShell (powershell.exe)

## Setup (commands agents can run)

- From repository root:
  - Restore dependencies: `dotnet restore`
  - Build solution: `dotnet build -clp:Summary`

- To validate SDK and environment:
  - `dotnet --info`

## Run / Debug

- Open solution in Visual Studio 2026 and press F5. Set appropriate startup project(s):
  - For server-side MVC: set `ASPDotNet_MVC_YuQin` as startup project
  - For Blazor WASM (if standalone): run the Blazor project directly

- Run from command line (examples):
  - Run MVC app: `cd ASPDotNet_MVC_YuQin && dotnet run`
  - Run Blazor WASM project: `cd ASPDotNet_MVC_YuQin/Blazor_WebAssembly_PWA_YuQin && dotnet run`

Notes: Some Blazor setups are hosted (server + client). Inspect Program.cs in each project to determine whether the WASM project is standalone or hosted.

## Tests

- No test projects detected in the current workspace. If tests are added, run: `dotnet test`

## Code style and quality

- Use `dotnet format` to apply formatting conventions.
- Prefer existing project conventions (naming, file layout). Keep UI changes within the matching UI framework:
  - .razor files -> Blazor projects
  - .cshtml files -> Razor Pages / MVC projects

## Packaging and deployment

- Publish a project for production:
  - `dotnet publish <project.csproj> -c Release -o ./publish`

## Key files and places to look

- Blazor startup: ASPDotNet_MVC_YuQin\Blazor_WebAssembly_PWA_YuQin\Program.cs
- MVC startup: ASPDotNet_MVC_YuQin\Program.cs
- Shared utilities: ASPDotNet_MVC_YuQin\SharedCS
- Controllers and API: ASPDotNet_MVC_YuQin\Controllers

## Troubleshooting tips

- If build/runtime fails, confirm SDK version matches target framework (net10.0). Use `dotnet --info`.
- If Blazor static assets 404, ensure `wwwroot` content is available and that the correct startup project serves the client files.

## Guidance for agents

- When editing UI code, match the project's UI technology. Do not convert .razor to .cshtml or vice versa without a clear migration plan.
- Add or update unit/integration tests when changing business logic.
- Keep changes minimal and focused; run `dotnet build` after edits and fix compilation errors before proposing pull requests.

## Example commands (copy/paste)

```powershell
dotnet --info
dotnet restore
dotnet build -clp:Summary
cd ASPDotNet_MVC_YuQin && dotnet run
cd ASPDotNet_MVC_YuQin\Blazor_WebAssembly_PWA_YuQin && dotnet run
```

## Contact / repository notes

Repository remote: https://github.com/jbhuang99/WebEdu_LocalVersion_YuQin_DotNetCore2.1

Keep AGENTS.md updated when new projects, tests, or CI files are added.
