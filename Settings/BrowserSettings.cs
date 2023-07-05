using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Settings
{
    public class BrowserSettings
    {
        public int PageLoadTimeout { get; set; } = 3000;
        public int ScriptTimeout { get; set; } = 1000;
        public int ArtificialDelayBeforeAction { get; set; } = 0;
    }
}
