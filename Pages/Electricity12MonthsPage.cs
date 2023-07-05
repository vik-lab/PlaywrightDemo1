using MercuryWebsite.Tests.UI.Model.Components.Sections;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class Electricity12MonthsPage : BasePage<Electricity12MonthsPage>
    {        
        public Electricity12MonthsPage(IPage page) : base(page) 
        {

        }

        public async Task SelectUsageType(string usageType)
        {
            ILocator usageTypeButton = _page.GetByRole(AriaRole.Button, new() { NameString = usageType });

            if (await usageTypeButton.IsEnabledAsync())             
                await usageTypeButton.ClickAsync();            
        }
    }
}
