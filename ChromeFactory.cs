using FluentAssertions.Equivalency.Tracing;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mercury.Tests.Drivers
{
    public class ChromeFactory : IDriverFactory
    {
        public async Task<IBrowser> GetBrowserAsync()
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright.Chromium.LaunchAsync(GetOptions());
        }

        public BrowserTypeLaunchOptions GetOptions(string[]? args = null, float? timeout = null, bool? headless = true, float? slowmo = null, string? traceDir = null)
            => new()
            { Args = args, Timeout = timeout * 1000, Headless = headless, SlowMo = slowmo, TracesDir = traceDir, Channel = "chrome" };

    }
}
