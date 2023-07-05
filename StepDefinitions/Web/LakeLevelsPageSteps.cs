using BoDi;
using Mercury.UI.Tests.StepDefinitions;
using MercuryWebsite.Tests.UI.Model.Pages;
using System.Security.Cryptography;

namespace MercuryWebsite.Tests.UI.StepDefinitions.Web
{
    [Binding]
    public class LakeLevelsPageSteps : BaseSteps
    {
        private readonly HomePage _homePage;
        private readonly LakeLevelsPage _lakeLevelsPage;

        public LakeLevelsPageSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _lakeLevelsPage = new LakeLevelsPage(_page);
            _homePage = new HomePage(_page);
        }
        [Given(@"a user is on the homePage")]
        public async Task GivenAUserIsOnTheHomePage()
        {
            await _page.GotoAsync("https://www.dexwebsiteqa.tpower.net.nz/");

        }

        [When(@"they click the link to navigate to the lake levels page")]
        public async Task WhenTheyClickTheLinkToNavigateToTheLakeLevelsPage()
        {
            await _lakeLevelsPage.GetLakeLevelsUrl();

        }

        [Then(@"they should be able to sucessfully open the lake levels page")]
        public async Task ThenTheyShouldBeAbleToSucessfullyOpenTheLakeLevelsPage()
        {
            var lakesPageTitle = _page.Url.ToString();
            lakesPageTitle.Should().Be("https://www.dexwebsiteqa.tpower.net.nz/lake-levels");
            
        }

    }
}
