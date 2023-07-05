using Mercury.UI.Tests.Drivers;
using MercuryWebsite.Tests.UI.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Mercury.Tests.Drivers
{
    public class BrowserDriver : IDisposable
    {        
        private readonly DriverInitialiser _driverInitialiser;
        protected readonly Task<IPage> _page;
        protected readonly AsyncLazy<IBrowser> _currentBrowserLazy;
        protected bool _isDisposed;

        public BrowserDriver(DriverInitialiser driverInitialiser)
        {
            _driverInitialiser = driverInitialiser;
            _currentBrowserLazy = new AsyncLazy<IBrowser>(CreateBrowserAsync);
            _page = CreatePageAsync(_currentBrowserLazy);
        }

        private async Task<IPage> CreatePageAsync(AsyncLazy<IBrowser> browser)
        {
            return await (await browser).NewPageAsync();
        }

        public IBrowser Browser => _currentBrowserLazy.Value.Result;

        public Task<IBrowserContext> BrowserContext => Browser.NewContextAsync();

        public IPage Page => _page.Result;

        private async Task<IBrowser> CreateBrowserAsync()
        {
            string? browser = ConfigurationService.GetWebSettings().Browser;
            string[]? args = null; 
            bool headless = ConfigurationService.GetWebSettings().Headless;
            string? traceDir = ConfigurationService.GetTraceSettings().TraceDir;

            return browser switch
            {
                "chrome" => await _driverInitialiser.GetChromeDriverAsync(args, headless, traceDir),
                "firefox" => await _driverInitialiser.GetFirefoxDriverAsync(args, headless, traceDir),
                "msedge" => await _driverInitialiser.GetEdgeDriverAsync(args, headless, traceDir),
                "chromium" => await _driverInitialiser.GetChromiumDriverAsync(args, headless, traceDir),
                "webkit" => await _driverInitialiser.GetWebKitDriverAsync(args, headless, traceDir),
                _ => throw new NotImplementedException($"Support for browser {browser} is not implemented yet"),
            };
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentBrowserLazy.IsValueCreated)
            {
                Task.Run(async delegate
                {
                    await (Browser).CloseAsync();
                    await (Browser).DisposeAsync();
                });
            }

            _isDisposed = true;
        }
    }
}
