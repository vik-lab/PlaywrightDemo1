using BoDi;
using FluentAssertions.Execution;
using Mercury.Web.Tests.Support.Utils;
using MercuryWebsite.Tests.UI.Model.Components;
using MercuryWebsite.Tests.UI.Model.Components.Sections;
using MercuryWebsite.Tests.UI.Model.Data;
using MercuryWebsite.Tests.UI.Model.Pages;
using MercuryWebsite.Tests.UI.Settings;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Assist;
using static Microsoft.Playwright.Assertions;

namespace Mercury.UI.Tests.StepDefinitions
{
    [Binding]
    public class SignUpSteps : BaseSteps
    {
        private readonly JoinPage _joinPage;
        private readonly BannerSection _bannerSection;
        private readonly AddressSearch _addressSearch;
        private readonly Electricity12MonthsPage _electricity12MonthsPage;
        private readonly PowerOfferSection _powerOfferSection;
        private readonly SummaryPage _summaryPage;
        private readonly SignUpPage _signUpPage;
        private readonly ConfirmationPage _confirmationPage;
        private readonly OfferLandingPage _offerLandingPage;

        public SignUpSteps(IObjectContainer objectContainer) : base(objectContainer) 
        {
            _joinPage = new JoinPage(_page); 
            _bannerSection = new BannerSection(_page);
            _addressSearch = new AddressSearch(_page);
            _electricity12MonthsPage = new Electricity12MonthsPage(_page);
            _powerOfferSection = new PowerOfferSection(_page);
            _summaryPage = new SummaryPage(_page);
            _signUpPage = new SignUpPage(_page);
            _confirmationPage = new ConfirmationPage(_page);
            _offerLandingPage = new OfferLandingPage(_page);
        }

        [Given(@"I elect to sign up for Electricity only")]
        public async Task GivenIElectToSignUpForElectricityOnly()
        {
            await _page.GotoAsync("/");
            // The below is a temporary hack to handle diffs between QA and UAT environments
            string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;

            if (baseUrl.ToLower().Contains("uat"))
            {
                await _bannerSection.ClickBannerLink("Join");
                await _joinPage.ClickJoinUs();                
            }
            else
            {
                await _joinPage.ClickLink("Click here to advance to the Offers Page");
                await _joinPage.ClickLink("Electricity only", false);//.GetByText(" Electricity only ").ClickAsync();
            }
        }

        [Given(@"I enter a valid supply address")]
        public async Task GivenIEnterAValidSupplyAddress()
        {
            await _addressSearch.EnterRandomAddress();

            await _offerLandingPage.GetShowMePricesButtonLocator().WaitForAsync(new LocatorWaitForOptions { Timeout = 15000 });

            await _offerLandingPage.ClickButton("Show me prices");
        }

        [Given(@"I have selected usage type '([^']*)'")]
        public async Task GivenIHaveSelectedUsageType(string usageType)
        {
            await _electricity12MonthsPage.SelectUsageType(usageType);            
        }

        [Given(@"I have chosen the '([^']*)' offer")]
        public async Task GivenIHaveChosenTheOffer(string offer)
        {
            await _powerOfferSection.SelectOffer(offer);

            await _electricity12MonthsPage.ClickButton("Next step");

            await _summaryPage.ClickButton("Next step");
        }

        [Given(@"I have entered valid personal details")]
        public async Task GivenIHaveEnteredValidPersonalDetails()
        {
            PersonalDetailsData personalDetails = new PersonalDetailsData();
            personalDetails.FirstName = Utils.GenerateRandomString(6);
            personalDetails.LastName = Utils.GenerateRandomString(8);
            personalDetails.ContactNumber = "021" + Utils.GenerateRandomNumber(7);
            personalDetails.DateOfBirth = "23/07/1995";
            personalDetails.EmailAddress = Utils.GenerateRandomString(4) + "." + Utils.GenerateRandomString(8) + "@testautomation.co.nz";

            await _signUpPage.EnterPersonalDetails(personalDetails);
        }

        [Given(@"I have entered valid driver licence details")]
        public async Task GivenIHaveEnteredValidDriverLicenseDetails()
        {
            DriverLicenseDetailsData driverLicenseDetails = new DriverLicenseDetailsData();
            driverLicenseDetails.LicenceNumber = "DL" + Utils.GenerateRandomNumber(6);
            driverLicenseDetails.LicenceVersion = Utils.GenerateRandomNumber(3);

            await _signUpPage.EnterDriversLicenceDetails(driverLicenseDetails);
        }

        [Given(@"I have entered the following property details:")]
        public async Task GivenIHaveEnteredTheFollowingPropertyDetails(Table table)
        {
            PropertyDetailsData propertyDetails = table.CreateInstance<PropertyDetailsData>();

            await _signUpPage.EnterPropertyDetails(propertyDetails);
        }

        [Given(@"I have consented to credit checks")]
        public async Task GivenIHaveConsentedToCreditChecks()
        {
            await _signUpPage.ConsentToCreditCheck();
        }

        [Given(@"I have accepted the Terms and Conditions")]
        public async Task GivenIHaveAcceptedTheTermsAndConditions()
        {
            await _signUpPage.AcceptTermsAndConditions();
        }

        [When(@"I click to sign up")]
        public async Task WhenIClickToSignUp()
        {
            await _signUpPage.SignUp();            
            
            await _page.Locator("input[type=file]").SetInputFilesAsync(new[] { "./TestData/PhotoId.png" });

            await _page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        }

        [Then(@"I should be signed up successfully")]
        public async Task ThenTheyShouldBeSignedUpSuccessfully()
        {
            using (new AssertionScope())
            {  
                // .IsVisibleAsync() is deprecated - using .IsEnabledAsync() instead
                bool signUpConfirmationMessageIsVisible = await _page.Locator(_confirmationPage.ConfirmationMessageTitle).IsEnabledAsync();
                signUpConfirmationMessageIsVisible.Should().BeTrue();

                string baseUrl = ConfigurationService.GetWebSettings().BaseUrl!;
                _page.Url.Should().MatchRegex(new Regex($"{baseUrl}/signup/confirmation"));                
            }
        }
    }
}