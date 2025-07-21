using Microsoft.Playwright;
using PlaywrightTest.Configs;

namespace PlaywrightTest.Pages
{
    public class AssetTreeListPage
    {
        private readonly IPage _page;
        private readonly IPlaywright _playwright;

        public AssetTreeListPage(IPage page, IPlaywright playwright)
        {
            _page = page;
            _playwright = playwright;
        }

        public async Task NavigateToGenerator()
        {
            await _page.GotoAsync(Constants.GeneratorUrl);
        }


        public async Task AddSubPlant(string name)
        {
            await _page.ClickAsync("#subplantbtn");
            await _page.FillAsync("#popup-model-input", "");
            await _page.ClickAsync(".add-btn");
            await _page.FillAsync("#popup-model-input", name);
            await _page.ClickAsync(".add-btn");
            await _page.ClickAsync(".Okbtn");
        }


        public async Task AddUniqueAsset(string baseName)
        {
            int i = 0;
            string currentName = baseName;
            while (true)
            {
                await _page.FillAsync("#popup-model-input", currentName);
                await _page.ClickAsync(".add-btn");

                var popup = _page.Locator(".Okbtn");
                if (await popup.IsVisibleAsync())
                {
                    string screenshotPath = $"Screenshots/{currentName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });

                    Allure.Commons.AllureLifecycle.Instance.AddAttachment(
                        $"Screenshot for {currentName}",
                        "image/png",
                        screenshotPath
                    );
                    await popup.ClickAsync();
                    currentName = $"{baseName}_{++i}";
                }
                else break;
            }
        }



        public async Task AddPipeline(string pipelineName)
        {
            var allSubplants = _page.Locator("div.node-label.child-label");
            int count = await allSubplants.CountAsync();
            await allSubplants.Nth(count - 1).Locator("button#pipelinebtn").ClickAsync();
            await _page.FillAsync("#popup-model-input", pipelineName);
            await _page.ClickAsync(".add-btn");
        }



        public async Task VerifyApiCall(string apiPath)
        {
            var apiContext = await _playwright.APIRequest.NewContextAsync(new() { IgnoreHTTPSErrors = true });
            var response = await apiContext.GetAsync(apiPath);

            var responseBody = await response.TextAsync();
            Console.WriteLine(responseBody);

            if (!response.Ok)
                throw new Exception("API call failed.");
        }
    }
}