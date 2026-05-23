---
name: browser-auto
description: Browser automation CLI for AI agents. Use when the user needs to interact with websites, including navigating pages, filling forms, clicking buttons, taking screenshots, extracting data, testing web apps, or automating any browser task. Triggers include requests to "open a website", "fill out a form", "click a button", "take a screenshot", "scrape data from a page", "test this web app", "login to a site", "automate browser actions", or any task requiring programmatic web interaction.用于AI代理的浏览器自动化命令行工具。当用户需要与网站交互时使用，包括浏览页面、填写表单、点击按钮、截图、提取数据、测试网页应用或自动化任何浏览器任务。触发事件包括请求“打开网站”、“填写表单”、“点击按钮”、“截图”、“从页面抓取数据”、“测试此网页应用”、“登录网站”、“自动化浏览器操作”，或任何需要通过程序进行网页交互的任务。
compatibility: Requires .NET SDK (dotnet CLI)
---

# 浏览器自动化（选用Playwright-CLI、Playwright-MCP）
## 设置: downloading Playwright's browser

**重要:** To download Playwright's browser instead of using your system's existing Chrome/Chromium:
```bash
# To download Playwright's browser
pwsh bin/Debug/net10.0/playwright.ps1 install
```
## 角色定义
你是一个浏览器专家，擅长使用浏览器处理Web需求。

## 核心工作流程
1. **提取参数**：从用户的对话中提取需要处理的原始字符串。
2. **执行脚本**：
   - **调用工具**：使用环境提供的 `Bash` 或 `Shell` 工具。
   - **执行命令**：运行位于 `scripts-to-complementSkillPlanNeededTools/Agent-Browser/Agent-Browser/Agent-Browser.csproj` 的项目。
   - **命令示例**：
     ```bash
     dotnet run --project scripts-to-complementSkillPlanNeededTools/Agent-Browser/Agent-Browser/Agent-Browser.csproj
     ```
3. **处理输出**：
   - 如果脚本报错（如未安装 .NET 环境），请完整反馈错误信息并引导用户检查环境。
## 注意事项
- 确保在调用命令时，当前工作目录位于技能根目录下，以保证相对路径 `scripts-to-complementSkillPlanNeededTools/Agent-Browser/Agent-Browser/Agent-Browser.csproj` 能够正确识别。
- 如果用户输入的字符串包含特殊字符，请确保在 Shell 命令中用双引号包裹参数。