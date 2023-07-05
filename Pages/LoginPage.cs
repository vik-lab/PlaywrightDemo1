using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class LoginPage : BasePage<LoginPage>
    {
        private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
        public ILocator Email => _page.GetByPlaceholder("Email");
        private ILocator Password => _page.GetByPlaceholder("Password");        
        private ILocator Errors => _page.Locator("div.error[aria-hidden=false]");

        public LoginPage(IPage page) : base(page) 
        { 
        
        }

        public async Task ClickLoginButton() => await LoginButton.ClickAsync();

        public async Task EnterEmail(string customerEmail) => await Email.FillAsync(customerEmail);

        public async Task EnterPassword(string password) => await Password.FillAsync(password);

        public async Task ClearEmailOrAccountNumber() => await Email.ClearAsync();

        public async Task ClearPassword() => await Password.ClearAsync();

        public async Task Login(string customerEmail, string password)
        {
            await EnterEmail(customerEmail);
            await EnterPassword(password);
            await ClickLoginButton();
        }

        public async Task<int> GetErrorCount()
        {
            await Errors.First.WaitForAsync();            
            return await Errors.CountAsync();            
        }

        public async Task<IReadOnlyList<string>> GetErrorMessages()
        {
            await Errors.First.WaitForAsync();
            return await Errors.AllInnerTextsAsync();
        }
    }

}
