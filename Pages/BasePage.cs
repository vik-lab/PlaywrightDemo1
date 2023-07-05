using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Model.Pages
{
    public abstract class BasePage<T> where T : BasePage<T>
    {
        protected readonly IPage _page;

        protected BasePage(IPage page)
        {
            _page = page;
        }

        public async Task ClickLink(string linkText, bool exact = true)
        {
            if (exact)
            {
                await _page.GetByRole(AriaRole.Link, new() { Name = linkText, Exact = true }).ClickAsync();
            }
            else
            {
                await _page.GetByRole(AriaRole.Link, new() { NameString = linkText }).ClickAsync();
            }
        }

        public async Task ClickButton(string buttonText)
        {
            await _page.GetByRole(AriaRole.Button, new() { NameString = buttonText }).ClickAsync();
        }
    }
}