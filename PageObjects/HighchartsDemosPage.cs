using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TA_Lab.PageObjects
{
    class HighchartsDemosPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public HighchartsDemosPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [FindsBy(How = How.XPath, Using = "//img[@alt='Advanced timeline']")]
        private IWebElement AdvancedButton;

        [FindsBy(How = How.Id, Using = "CybotCookiebotDialogBodyLevelButtonAccept")]
        private IWebElement CookiesButton;

        public HighchartsAdvancedPage GoToAdvanced()
        {
            Thread.Sleep(2000);
            if (CookiesButton.Displayed)
                CookiesButton.Click();
            AdvancedButton.Click();
            return new HighchartsAdvancedPage();
        }
    }
}
