# Convert Minimal APIs to MVC Controller
## Understanding
Move the HTTP endpoint mappings currently declared in `Program.cs` into an MVC controller while preserving all existing routes, response shapes, embedded homepage behavior, serial-port ordering, and automatic browser startup.
## Assumptions
- The existing route URLs must remain unchanged because `wwwroot/index.html` calls them directly.
- `EegService` and the data records can remain in `Program.cs`; only endpoint registration should move to a controller.
- The project should continue targeting .NET 10 without changing package versions or unrelated behavior.
## Approach
Register MVC controllers with dependency injection in `Program.cs`, map controller routes, and create an `EegController` containing the existing GET/POST actions. Use `ControllerBase` for API responses and `Controller` for the embedded HTML response. Preserve status codes and filenames exactly.
## Key Files
- `Program.cs` - replace `MapGet`/`MapPost` endpoint registrations with MVC service and route registration.
- `Controllers/EegController.cs` - new controller implementing homepage, device, session, event, baseline, and export endpoints.
## Risks & Open Questions
- The embedded resource name is currently hard-coded as `EEGWeb.wwwroot.index.html`; preserve it to avoid changing resource behavior.
## Steps
1. Add the MVC controller with all existing endpoint actions.
2. Update `Program.cs` to register MVC and map controllers.
3. Build the project and fix compilation issues caused by the conversion.
4. Verify the resulting controller routes and summarize the change.