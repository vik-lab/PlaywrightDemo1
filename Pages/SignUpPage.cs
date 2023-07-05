using FluentAssertions.Execution;
using MercuryWebsite.Tests.UI.Model.Data;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class SignUpPage : BasePage<SignUpPage>
    {
        private readonly string _firstName = "#firstName";
        private readonly string _lastName = "#lastName";
        private readonly string _contactNumber = "#contactNumber";
        private readonly string _dateOfBirth = "#dateOfBirth";
        private readonly string _emailAddress = "#emailAddress";
        private readonly string _driverLicenseButton = "#identificationOptionLicense";
        private readonly string _driverLicenseNumber = "#driverLicenseNumber";
        private readonly string _driverLicenseVersion = "#driverLicenseVersion";
        private readonly string _preferredStartDateButton = "#preferredDateASAP";
        private readonly string _alreadyLiveHereButton = "#currentAddressYes";
        private readonly string _movingToAddressButton = "#currentAddressNo";
        private readonly string _vulnerablePersonYesButton = "#vulnerablePersonYes";
        private readonly string _vulnerablePersonNoButton = "#vulnerablePersonNo";
        private readonly string _medicalEquipmentYesButton = "#medicalEquipmentYes";
        private readonly string _medicalEquipmentNoButton = "#medicalEquipmentNo";
        private readonly string _dogOnPropertyYesButton = "#isThereADogYes";
        private readonly string _dogOnPropertyNoButton = "#isThereADogNo";

        public SignUpPage(IPage page) : base(page) 
        { 
        
        }

        public async Task EnterPersonalDetails(PersonalDetailsData personalDetails)
        {
            await EnterFirstName(personalDetails.FirstName);
            await EnterLastName(personalDetails.LastName);
            await EnterContactNumber(personalDetails.ContactNumber);
            await EnterDateOfBirth(personalDetails.DateOfBirth);
            await EnterEmailAddress(personalDetails.EmailAddress);
        }

        private async Task EnterFirstName(string? firstName)
        {
            await _page.Locator(_firstName).FillAsync(firstName!);
        }

        private async Task EnterLastName(string? lastName)
        {
            await _page.Locator(_lastName).FillAsync(lastName!);
        }

        private async Task EnterContactNumber(string? contactNumber)
        {
            await _page.Locator(_contactNumber).FillAsync(contactNumber!);
        }

        private async Task EnterDateOfBirth(string? dateOfBirth)
        {
            await _page.Locator(_dateOfBirth).FillAsync(dateOfBirth!);
        }

        private async Task EnterEmailAddress(string? emailAddress)
        {
            await _page.Locator(_emailAddress).FillAsync(emailAddress!);
        }

        public async Task EnterDriversLicenceDetails(DriverLicenseDetailsData driversLicenseDetails)
        {
            await _page.Locator(_driverLicenseButton).ClickAsync();

            await EnterLicenceNumber(driversLicenseDetails.LicenceNumber);
            await EnterLicenceVersion(driversLicenseDetails.LicenceVersion);
        }

        private async Task EnterLicenceNumber(string? licenceNumber)
        {
            await _page.Locator(_driverLicenseNumber).FillAsync(licenceNumber!);
        }

        private async Task EnterLicenceVersion(string? licenceVersion)
        {
            await _page.Locator(_driverLicenseVersion).FillAsync(licenceVersion!);
        }

        public async Task EnterPropertyDetails(PropertyDetailsData propertyDetails)
        {
            await SelectPreferredStartDate(propertyDetails.PreferredStartDate);
            await SelectIsCurrentAddress(propertyDetails.CurrentAddress);
            await SelectVulnerablePerson(propertyDetails.VulnerablePerson);
            await SelectMedicalEquipment(propertyDetails.MedicalEquipment);
            await SelectDogOnProperty(propertyDetails.Dog);
        }        

        private async Task SelectPreferredStartDate(string? preferredStartDate)
        {
            if (preferredStartDate!.ToLower() == "asap")
                await _page.Locator(_preferredStartDateButton).ClickAsync();
            else
                Execute.Assertion.FailWith("ERROR: Logic missing for preferred start date other than 'ASAP' - update test.");
        }

        private async Task SelectIsCurrentAddress(string? currentAddress)
        {
            if (currentAddress!.ToLower() == "yes")
                await _page.Locator(_alreadyLiveHereButton).ClickAsync();
            else
                await _page.Locator(_movingToAddressButton).ClickAsync();
        }

        private async Task SelectVulnerablePerson(string? vulnerablePerson)
        {
            if (vulnerablePerson!.ToLower() == "yes")
                await _page.Locator(_vulnerablePersonYesButton).ClickAsync();
            else
                await _page.Locator(_vulnerablePersonNoButton).ClickAsync();
        }

        private async Task SelectMedicalEquipment(string? medicalEquipment)
        {
            if (medicalEquipment!.ToLower() == "yes")
                await _page.Locator(_medicalEquipmentYesButton).ClickAsync();
            else
                await _page.Locator(_medicalEquipmentNoButton).ClickAsync();
        }

        private async Task SelectDogOnProperty(string? dog)
        {
            if (dog!.ToLower() == "yes")
                await _page.Locator(_dogOnPropertyYesButton).ClickAsync();
            else
                await _page.Locator(_dogOnPropertyNoButton).ClickAsync();
        }

        public async Task ConsentToCreditCheck()
        {
            await _page.Locator("#creditCheckbox span").First.ClickAsync();            
        }

        public async Task AcceptTermsAndConditions()
        {            
            await _page.Locator("#termsAndConditionsCheckbox span").First.ClickAsync();
        }

        public async Task SignUp()
        {
            await ClickButton("Sign me up");
        }
    }
}
