using Microsoft.Playwright;

namespace PlaywrightTest.Utilities
{
    public static class PlaywrightFactory
    {
        public static async Task<(IPlaywright, IBrowser, IPage, IBrowserContext)> LaunchBrowserWithVideo()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "chrome",
                Headless = false,
                SlowMo = 2500,
                Timeout = 5000
            });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                RecordVideoDir = "videos/", // folder to save video files
                RecordVideoSize = new() { Width = 1280, Height = 720 }
            });

            var page = await context.NewPageAsync();

            return (playwright, browser, page, context);
        }

    }
}
