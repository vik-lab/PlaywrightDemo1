using Microsoft.Playwright;
using RestSharp.Serializers;

namespace MercuryWebsite.Tests.UI.Model.Components.Sections
{
    public class BannerSection
    {
        private readonly IPage _page;
        private readonly ILocator _rootLocator;
        public readonly ILocator ProfileDropdownButton;

        public BannerSection(IPage page)
        {
            _page = page;
            _rootLocator = _page.GetByRole(AriaRole.Banner);
            ProfileDropdownButton = _rootLocator.Locator("pl-profile-dropdown button");
        }

        public async Task ClickBannerLink(string linkText) => await _rootLocator.GetByRole(AriaRole.Link, new() { NameString = linkText }).ClickAsync();
    }
}
