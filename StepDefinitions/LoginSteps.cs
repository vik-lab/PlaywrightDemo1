using BoDi;
using FluentAssertions.Execution;
using Mercury.UI.Tests.StepDefinitions;
using Mercury.Web.Tests.Support.Utils;
using MercuryWebsite.Tests.UI.Model.Components.Sections;
using MercuryWebsite.Tests.UI.Model.Data;
using MercuryWebsite.Tests.UI.Model.Pages;
using MercuryWebsite.Tests.UI.Settings;

namespace MercuryWebsite.Tests.UI.StepDefinitions
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        private readonly LoginPage _loginPage;
        private readonly HomePage _homepage;
        private readonly BannerSection _bannerSection;

        public LoginSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _loginPage = new LoginPage(_page);
            _homepage = new HomePage(_page);
            _bannerSection = new BannerSection(_page);
        }

        [Given(@"I am on the login page")]
        public async Task GivenIAmOnTheLoginPage()
        {
            await _page.GotoAsync("/");
            await _bannerSection.ClickBannerLink("Login");            
        }

        [When(@"they attempt to log in without entering any credentials")]
        public async Task WhenTheyAttemptToLogInWithoutEnteringAnyCredentials()
        {
            await _loginPage.ClearEmailOrAccountNumber();
            await _loginPage.ClearPassword();
            await _loginPage.ClickLoginButton();
        }

        [Then(@"they should see errors for the missing email and password")]
        public async Task ThenTheyShouldSeeErrorsForTheMissingEmailAndPassword()
        {
            int errorCount = await _loginPage.GetErrorCount();
            IReadOnlyList<string> errorMessages = await _loginPage.GetErrorMessages();           

            using (new AssertionScope())
            {
                errorCount.Should().Be(2, "there should be an error for both the missing email and missing password");
                errorMessages.First().Trim().Should().MatchRegex("(?i)(.*enter.*email)");
                errorMessages.Last().Trim().Should().MatchRegex("(?i)(.*enter.*password)");                
            }
        }

        [Given(@"I am an unregistered My Account user")]
        public void GivenThatIAmAnUnregisteredUser()
        {
            string email = Utils.GenerateRandomEmail();
            string password = Utils.GenerateRandomString(8);

            _objectContainer.RegisterInstanceAs(new LoginData(email, password), "unregisteredUser");
        }

        [Then(@"I should see an error message")]
        public async Task ThenIShouldSeeAnErrorMessage()
        {
            int errorCount = await _loginPage.GetErrorCount();
            IReadOnlyList<string> errorMessages = await _loginPage.GetErrorMessages();

            errorCount.Should().Be(1, "there should be error displayed informing the user that the account cannot be found");                       
            errorMessages.First().Trim().Should().MatchRegex("(?i)(.*can't.*find.*account)");            
        }

        [Then(@"I should not be logged in")]
        public async Task ThenIShouldNotBeLoggedIn()
        {
            bool emailTextboxIsEnabled = await _loginPage.Email.IsEnabledAsync();
            
            emailTextboxIsEnabled.Should().BeTrue();            
        }

        [Given(@"I am a registered My Account user")]
        public void GivenThatIAmARegisteredUser()
        {
            string email = ConfigurationService.GetTestUser().Email;
            string password = ConfigurationService.GetTestUser().Password;

            _objectContainer.RegisterInstanceAs(new LoginData(email, password), "registeredUser");
        }

        [When(@"I log in with (.*) credentials")]
        public async Task WhenILogInWithValidCredentials(string credentials)
        {                        
            LoginData loginData;

            if (credentials.ToLower() == "invalid")
                loginData = _objectContainer.Resolve<LoginData>("unregisteredUser");
            else
                loginData = _objectContainer.Resolve<LoginData>("registeredUser");
                        
            await _loginPage.EnterEmail(loginData.Email);
            await _loginPage.EnterPassword(loginData.Password);
            await _loginPage.ClickLoginButton();
        }

        [Then(@"I should be logged in")]
        public async Task ThenIShouldBeLoggedIn()
        {
            bool userProfileDropdownButtonIsEnabled = await _bannerSection.ProfileDropdownButton.IsEnabledAsync();

            userProfileDropdownButtonIsEnabled.Should().BeTrue();            
        }

        [When(@"they attempt to log in with an email address but no password")]
        public async Task WhenTheyAttemptToLogInWithAnEmailAddressButNoPassword()
        {
            await _loginPage.ClearPassword();
            await _loginPage.EnterEmail(Utils.GenerateRandomEmail());
            await _loginPage.ClickLoginButton();
        }

        [Then(@"they should see an error for the missing email address")]
        public async Task ThenTheyShouldSeeAnErrorForTheMissingEmailAddress()
        {
            int errorCount = await _loginPage.GetErrorCount();
            IReadOnlyList<string> errorMessages = await _loginPage.GetErrorMessages();

            errorCount.Should().Be(1, "there should be an error displayed as email address is missing");
            errorMessages.First().Trim().Should().MatchRegex("(?i)(.*enter.*email)");
        }

        [When(@"they attempt to log in with a password but no email address")]
        public async Task WhenTheyAttemptToLogInWithAPasswordButNoEmailAddress()
        {
            await _loginPage.ClearEmailOrAccountNumber();
            await _loginPage.EnterPassword(Utils.GenerateRandomString(8));
            await _loginPage.ClickLoginButton();
        }

        [Then(@"they should see an error for the missing password")]
        public async Task ThenTheyShouldSeeAnErrorForTheMissingPassword()
        {
            int errorCount = await _loginPage.GetErrorCount();
            IReadOnlyList<string> errorMessages = await _loginPage.GetErrorMessages();

            errorCount.Should().Be(1, "there should be an error displayed as password is missing");
            errorMessages.First().Trim().Should().MatchRegex("(?i)(.*enter.*password)");
        }

    }
}