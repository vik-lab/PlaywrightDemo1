using BoDi;
using FluentAssertions.Execution;
using MercuryWebsite.Tests.UI.Model.Components.Sections;
using MercuryWebsite.Tests.UI.Settings;

namespace Mercury.UI.Tests.StepDefinitions
{
    [Binding]
    public class HomePageSteps : BaseSteps
    {        
        private readonly BannerSection _bannerSection;

        public HomePageSteps(IObjectContainer objectContainer) : base(objectContainer) 
        {            
            _bannerSection = new BannerSection(_page);
        }

        [Given(@"a user is on the Mercury website home page")]
        public void GivenAUserIsOnTheMercuryWebsiteHomePage() 
        {
            _page.GotoAsync("/");
        }

        [Given(@"they click on banner link '(.*)'")]
        [When(@"they click on banner link '(.*)'")]
        public async Task ClickBannerLink(string linkText)
        {            
            await _bannerSection.ClickBannerLink(linkText);
        }

        [Then(@"^they should be taken to the Gas page$")]
        public async Task ThenIShouldBeTakenToTheJoinPage()
        {
            using (new AssertionScope())
            {
                string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;
                
                string? breadcrumb = await _page.Locator(".mcy-BreadCrumb__item").Last.InnerTextAsync();
                breadcrumb!.Trim().ToLower().Should().Be("gas");

                _page.Url.Should().MatchRegex($"{baseUrl}/gas$");
            }
        }

        [Then(@"^they should be taken to the Electricity page$")]
        public async Task ThenIShouldBeTakenToTheElectricityPage()
        {
            using (new AssertionScope())
            {
                string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;

                string? breadcrumb = await _page.Locator(".mcy-BreadCrumb__item").Last.InnerTextAsync();
                breadcrumb!.Trim().ToLower().Should().Be("electricity");

                _page.Url.Should().MatchRegex($"{baseUrl}/electricity$");
            }
        }

        [Then(@"^they should be taken to the Broadband page$")]
        public async Task ThenIShouldBeTakenToTheBroadbandPage()
        {
            using (new AssertionScope())
            {
                string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;

                string? breadcrumb = await _page.Locator(".mcy-BreadCrumb__item").Last.InnerTextAsync();
                breadcrumb!.Trim().ToLower().Should().Be("broadband");

                _page.Url.Should().MatchRegex($"{baseUrl}.*/broadband$");
            }
        }

        [Then(@"^they should be taken to the Mobile page$")]
        public async Task ThenIShouldBeTakenToTheMobilePage()
        {
            using (new AssertionScope())
            {
                string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;

                string? breadcrumb = await _page.Locator(".mcy-BreadCrumb__item").Last.InnerTextAsync();
                breadcrumb!.Trim().ToLower().Should().Be("mobile");

                _page.Url.Should().MatchRegex($"{baseUrl}/mobile$");
            }
        }
    }
}