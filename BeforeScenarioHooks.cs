using BoDi;
using Mercury.Tests.Drivers;
using Mercury.Web.Tests.Support.Utils;
using MercuryWebsite.Tests.UI.Model.Components.Sections;
using MercuryWebsite.Tests.UI.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.UI.Tests.Hooks
{
    [Binding]
    public class BeforeScenarioHooks
    {
        private readonly IObjectContainer _objectContainer;
        protected ScenarioContext _scenarioContext;
        protected IPage? _page;                
 
        public BeforeScenarioHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;            
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario()]
        public async Task InitialisePlaywrightMercuryWeb(BrowserDriver browserDriver)
        {
            var browserContext = await browserDriver.Browser.NewContextAsync(GetBrowserNewContextOptions(browserDriver));
            browserContext.SetDefaultNavigationTimeout(ConfigurationService.GetWebSettings().WaitForNavigationTimeout);
            browserContext.SetDefaultTimeout(ConfigurationService.GetWebSettings().WaitForElementTimeout);

            if (ConfigurationService.GetTraceSettings().Trace)
                await StartTracing(browserContext);            

            var page = await browserContext.NewPageAsync();           

            _page = page;

            _objectContainer.RegisterInstanceAs(browserContext);
            _objectContainer.RegisterInstanceAs(page);            
        }

        private BrowserNewContextOptions GetBrowserNewContextOptions(BrowserDriver browserDriver)
        {
            BrowserNewContextOptions options = new BrowserNewContextOptions();

            options.BaseURL = ConfigurationService.GetWebSettings().BaseUrl!;            

            if (ConfigurationService.GetVideoSettings().Video)
                options.RecordVideoDir = ConfigurationService.GetVideoSettings().VideoDir!;

            return options;
        }

        private async Task StartTracing(IBrowserContext browserContext)
        {
            await browserContext.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }
    }
}
