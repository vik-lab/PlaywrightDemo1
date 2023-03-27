using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace PlaywrightDemo1
{
    [TestClass]
    public class PlaywrightTests
    {
        [TestMethod]
        [SetUp]
        public void Setup()
        {
        }
        [TestMethod]
        [Test]
        public async Task Test()
        {
            //playwright
            using var playwright = await Playwright.CreateAsync();

            //open the browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            //page
            var page = await browser.NewPageAsync();
            await page.GotoAsync(url: "http://www.mercury.co.nz");
            await page.CheckAsync(selector: "text = gas");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "mercuryGas.jpg"
            });
        }
    }
}