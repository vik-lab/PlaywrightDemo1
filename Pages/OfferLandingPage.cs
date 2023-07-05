using Microsoft.Playwright;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class OfferLandingPage : BasePage<OfferLandingPage>
    {        
        public OfferLandingPage(IPage page) : base(page) 
        {            

        }

        public ILocator GetShowMePricesButtonLocator()
        {
            return _page.GetByRole(AriaRole.Button, new() { NameString = "Show me prices" });
        }
    }
}
