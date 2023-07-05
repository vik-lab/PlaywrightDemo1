using Microsoft.Playwright;
using NUnit.Framework;
using RestSharp.Serializers;

namespace MercuryWebsite.Tests.UI.Model.Components
{
    public class AddressSearch
    {
        private readonly IPage _page;
        private readonly string _addressSearchTextBox = "#addressSearchTextbox";
        private readonly string _addressListItems = "ul.ui-autocomplete li";

        public AddressSearch(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Types 3 characters to initiate the address search: 
        ///     - a random letter
        ///     - a space
        ///     - then a random number.
        /// Then selects a random address from the list returned.         
        /// </summary>
        /// <returns>Task</returns>
        public async Task EnterRandomAddress()
        {            
            Random random = new Random();
            char randomLetter = (char)random.Next('a', 'z' + 1);

            random = new Random();
            int randomNumber = random.Next(1, 10);

            await _page.Locator(_addressSearchTextBox).FillAsync(randomLetter + " " + randomNumber);            
            await _page.WaitForSelectorAsync(_addressListItems);            

            int addressListItemsCount = await _page.Locator(_addressListItems).CountAsync();

            random = new Random();
            randomNumber = random.Next(1, addressListItemsCount);            

            await _page.Locator(_addressListItems).Nth(randomNumber - 1).ClickAsync();
        }
    }
}
