using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class JoinPage : BasePage<JoinPage>
    {
        public JoinPage(IPage page) : base(page) 
        { 
        
        }

        public async Task ClickJoinUs()
        {
            await _page.Locator("a[href*='electricity-12-months']").ClickAsync();
        }
    }
}
