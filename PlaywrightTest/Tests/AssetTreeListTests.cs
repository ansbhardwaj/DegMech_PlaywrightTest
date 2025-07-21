using Allure.NUnit.Attributes;
using Allure.NUnit.Core;
using Microsoft.Playwright;
using PlaywrightTest.Pages;
using PlaywrightTest.Utilities;
using PlaywrightTest.Configs;
using Allure.NUnit;

namespace PlaywrightTest.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Asset Tree Tests")]
    public class AssetTreeListTests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private AssetTreeListPage _assetTreePage;
        private IBrowserContext _context;


        [SetUp]
        public async Task SetUp()
        {
            (_playwright, _browser, _page, _context) = await PlaywrightFactory.LaunchBrowserWithVideo();
            _assetTreePage = new AssetTreeListPage(_page, _playwright);

        }


        [Test]
        [AllureTag("Playwright", "Asset Tree List")]
        [AllureOwner("Anshul")]
        [AllureSubSuite("Deg Mech Asset Tree List")]
        public async Task AssetTreeList_AddSubplantAndPipeline_Successfully()
        {
            await _assetTreePage.NavigateToGenerator();
            await _assetTreePage.AddSubPlant("tank pit 2");
            await _assetTreePage.AddUniqueAsset("oxygen tank pit");
            await _assetTreePage.AddPipeline("new co2 pipeline");

        }



        [Test]
        [AllureTag("API", "Verification")]
        [AllureOwner("Anshul")]
        [AllureSubSuite("Deg Mech API Verification")]
        public async Task AssetTreeList_VerifyApiCall()
        {
            await _assetTreePage.VerifyApiCall(Constants.APITestPath);
        }


        [Test]
        [AllureTag("DB", "Validation")]
        [AllureOwner("Anshul")]
        [AllureSubSuite("Deg Mech Database Validation")]
        public async Task AssetTreeList_VerifyDatabaseEntry()
        {
            var result = await DbHelper.GetAssetName("pipeline 1");
            Assert.That(result, Is.EqualTo("pipeline 1"));
        }



        [TearDown]
        public async Task TearDown()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }

    }
}
