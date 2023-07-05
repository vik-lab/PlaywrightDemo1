using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Tests.Drivers
{
    public interface IDriverFactory
    {
        public Task<IBrowser> GetBrowserAsync();

        public BrowserTypeLaunchOptions GetOptions(string[]? args = null, float? timeout = null, bool? headless = true, float? slowmo = null, string? traceDir = null);

        public static Task<IBrowser> BuildDriver(string browser, bool headless = true)
        {
            var factoryType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.GetInterface(typeof(IDriverFactory).ToString()) != null)
                .FirstOrDefault(type => type.Name.ToLower().Contains(browser))
                ?? throw new Exception($"Browser {browser} not suported");

            var driverFactory = Activator.CreateInstance(factoryType) as IDriverFactory;
            return driverFactory!.GetBrowserAsync();
        }
    }
}