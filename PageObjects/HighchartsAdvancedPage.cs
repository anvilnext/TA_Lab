using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace TA_Lab.PageObjects
{
    class HighchartsAdvancedPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public HighchartsAdvancedPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Google search']")]
        IList<IWebElement> GoogleSearchGraph;

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Revenue']")]
        IList<IWebElement> RevenueGraph;

        [FindsBy(How = How.CssSelector, Using = "path[aria-label*='Highsoft employees']")]
        IList<IWebElement> EmployeesGraph;

        public void CheckCharts()
        {
            
        }
    }
}
