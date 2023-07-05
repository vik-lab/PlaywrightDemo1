using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Settings
{
    public sealed class WebSettings
    {
        public string? BaseUrl { get; set; }
        public string? Browser { get; set; }
        public BrowserSettings? Chrome { get; set; }
        public int WaitForElementTimeout { get; set; }
        public int WaitForNavigationTimeout { get; set; }
        public bool Headless { get; set; }
        public int SlowMo { get; set; }
    }
}
