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
[assembly: Parallelizable(ParallelScope.Fixtures), LevelOfParallelism(2)]

namespace Mercury.UI.Tests.Hooks
{
    [Binding]
    public class BeforeTestRunHooks
    {
        //private readonly IObjectContainer _objectContainer;
        //protected ScenarioContext _scenarioContext;
        //protected IPage? _page;        
        //protected bool _isDisposed;        
 
        public BeforeTestRunHooks()
        {
          //  _objectContainer = objectContainer;            
            //_scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        private static void CleanDirectories()
        {
            string executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;            
            string tracesPath = Path.Combine(executingAssemblyPath, ConfigurationService.GetTraceSettings().TraceDir!);
            string videosPath = Path.Combine(executingAssemblyPath, ConfigurationService.GetVideoSettings().VideoDir!);

            if (Directory.Exists(tracesPath))
                Directory.Delete(tracesPath, true);

            Directory.CreateDirectory(tracesPath);

            if (Directory.Exists(videosPath))
                Directory.Delete(videosPath, true);

            Directory.CreateDirectory(videosPath);
        }        
    }
}
