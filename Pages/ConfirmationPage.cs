using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public class ConfirmationPage : BasePage<ConfirmationPage>
    {
        public string ConfirmationMessageTitle = ".confirmMessage__title";

        public ConfirmationPage(IPage page) : base(page) 
        { 
        
        }        
    }
}
