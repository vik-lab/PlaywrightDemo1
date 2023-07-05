using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.UI.Tests.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected readonly IPage _page;        
        protected IObjectContainer _objectContainer;

        protected BaseSteps(IObjectContainer objectContainer)
        {
            _page = objectContainer.Resolve<IPage>();            
            _objectContainer = objectContainer;
        }
    }

}
