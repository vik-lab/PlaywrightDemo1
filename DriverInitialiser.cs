using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Tests.Drivers
{
    public class DriverInitialiser : IDriverInitialiser
    {
        public async Task<IBrowser> GetChromeDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null)
        {
            var options = GetOptions(args, headless, traceDir);
            options.Channel = "chrome";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null)
        {
            var options = GetOptions(args, headless, traceDir);

            return await GetBrowserAsync(BrowserType.Firefox, options);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null)
        {
            var options = GetOptions(args, headless, traceDir);
            options.Channel = "msedge";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null)
        {
            var options = GetOptions(args, headless, traceDir);

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetWebKitDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null)
        {
            var options = GetOptions(args, headless, traceDir);

            return await GetBrowserAsync(BrowserType.Webkit, options);
        }

        private BrowserTypeLaunchOptions GetOptions(string[]? args = null, bool? headless = true, string? traceDir = null)
            => new()
            { Args = args, Headless = headless, TracesDir = traceDir };

        private async Task<IBrowser> GetBrowserAsync(string browserType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Playwright.CreateAsync();
            
            return await playwright[browserType].LaunchAsync(options);
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}
