using System.Threading.Tasks;
using Microsoft.Playwright;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        // Open Zhihu article create page
        await page.GotoAsync("https://www.zhihu.com/creator/manage/creation/article");

        // Wait for some visible element
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        System.Console.WriteLine("Opened Zhihu article editor page. Please sign in manually if required.");

        // Keep browser open for manual interaction
        System.Console.WriteLine("Press Enter to close browser...");
        System.Console.ReadLine();

        await browser.CloseAsync();
        return 0;
    }
}
