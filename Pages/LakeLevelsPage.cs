using Microsoft.Playwright;
using System.Reflection.Metadata;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class LakeLevelsPage : BasePage<LakeLevelsPage>
    {
        public LakeLevelsPage(IPage page) : base(page)
        {
            
        }
        public async Task GetLakeLevelsUrl()
        {
            await _page.Locator("//a[contains(text(),'LakeLevels')]").ClickAsync();        
            
        }
    }
}
