using BoDi;
using FluentAssertions.Execution;
using Mercury.Web.Tests.Support.Utils;
using MercuryWebsite.Tests.UI.Model.Pages;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;

namespace Mercury.UI.Tests.StepDefinitions
{
    [Binding]
    public class JoinPageSteps : BaseSteps
    {
        private readonly JoinPage _joinPage;
        
        public JoinPageSteps(IObjectContainer objectContainer) : base(objectContainer) 
        {
            _joinPage = new JoinPage(_page);            
        }

        [Given(@"they click on 'Join Us'")]
        public async Task GivenTheyClickOn()
        {
            await _joinPage.ClickJoinUs();
        }

    }
}