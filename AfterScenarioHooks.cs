﻿using BoDi;
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
//[assembly: Parallelizable(ParallelScope.Fixtures), LevelOfParallelism(2)]

namespace Mercury.UI.Tests.Hooks
{
    [Binding]
    public class AfterScenarioHooks
    {
        private readonly IObjectContainer _objectContainer;
        protected ScenarioContext _scenarioContext;
        protected IPage? _page;        
        protected bool _isDisposed;        
 
        public AfterScenarioHooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;            
            _scenarioContext = scenarioContext;
        }

        [AfterScenario()]        
        public async Task TakeScreenshotOnFailure()
        {            
            if (_scenarioContext.TestError != null)
            {
                string executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                string screenshotsPath = Path.Combine(executingAssemblyPath, "Screenshots" + _scenarioContext.ScenarioInfo.Title + ".png");
                IPage page = _objectContainer.Resolve<IPage>();

                await page.ScreenshotAsync(new()
                {
                    Path = screenshotsPath,
                    FullPage = true,
                });

                TestContext.AddTestAttachment(screenshotsPath);
            }

            if (ConfigurationService.GetTraceSettings().Trace)
            {
                var browserContext = _objectContainer.Resolve<IBrowserContext>();
                await browserContext.Tracing.StopAsync(new()
                {
                    Path = Path.Combine(ConfigurationService.GetTraceSettings().TraceDir!, _scenarioContext.ScenarioInfo.Title + ".zip")
                });
            }
        }
    }
}
