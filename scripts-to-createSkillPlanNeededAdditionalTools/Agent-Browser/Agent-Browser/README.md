This minimal Playwright .NET agent opens Zhihu's article creation page to allow manual login and interaction.

Usage:
1. Build/run: dotnet run --project scripts-to-createSkillPlanNeededAdditionalTools/Agent-Browser/Agent-Browser/Agent-Browser.csproj
2. After the first run, download Playwright browsers with the generated script (PowerShell):
   pwsh bin/Debug/net10.0/playwright.ps1 install

Note: This project launches Chromium (not headless) so you can sign in interactively.
