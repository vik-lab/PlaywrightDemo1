using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Tests.Drivers
{
    public interface IDriverInitialiser
    {
        Task<IBrowser> GetChromeDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null);
        Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null);
        Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null);
        Task<IBrowser> GetChromiumDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null);
        Task<IBrowser> GetWebKitDriverAsync(string[]? args = null, bool? headless = true, string? traceDir = null);
    }
}
