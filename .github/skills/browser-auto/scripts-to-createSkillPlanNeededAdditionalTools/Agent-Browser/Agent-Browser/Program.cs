using Microsoft.Playwright;
using System.Threading.Tasks;
using System.Xml;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. 创建 Playwright 实例
        using var playwright = await Playwright.CreateAsync();

        // 2. 启动浏览器（Headless=false 表示显示浏览器界面，方便调试）
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 500 // 慢动作演示，每个操作延迟500毫秒
        });

        // 3. 创建独立的浏览器上下文（无痕模式）
        var context = await browser.NewContextAsync();

        // 4. 打开新页面
        var page = await context.NewPageAsync();

        // 5. 核心操作：跳转、定位、输入、点击
        await page.GotoAsync("https://www.baidu.com");

        // 使用 Locator 定位元素（推荐，自带自动等待）
        var searchInput = page.Locator("#kw"); // 百度搜索框
        var searchBtn = page.Locator("#su");   // 百度一下按钮

        await searchInput.FillAsync("C# Playwright 自动化");
        await searchBtn.ClickAsync();

        // 等待网络空闲，确保搜索结果加载完成
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // 6. 截图保存
        await page.ScreenshotAsync(new PageScreenshotOptions { Path = "result.png", FullPage = true });

        // 7. 释放资源（using 语句会自动处理，也可手动关闭）
        await browser.CloseAsync();
    }
}