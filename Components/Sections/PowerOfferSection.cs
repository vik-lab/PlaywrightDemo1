using Microsoft.Playwright;
using RestSharp.Serializers;
using System.Text.RegularExpressions;

namespace MercuryWebsite.Tests.UI.Model.Components.Sections
{
    public class PowerOfferSection
    {
        private readonly IPage _page;        
        private readonly string _rootLocator = "//div[@class=\"mcyPowerOfferSection\"]//div[@class=\"mcyPowerOfferSection__title\"]";////div[@class=\"mcyPowerOfferSection__title\" and text()=\"1 Year fixed\"]//parent::div//parent::div//button";

        public PowerOfferSection(IPage page)
        {
            _page = page;
        }

        public async Task SelectOffer(string offerTitle)
        {
            ILocator offerButton = _page.Locator(_rootLocator)
                                        .Filter(new() { HasTextString = offerTitle })
                                        .Locator("//parent::div//parent::div//button").Last;            

            if (await offerButton.IsEnabledAsync())
                await offerButton.ClickAsync();                                        
        }
    }    
}