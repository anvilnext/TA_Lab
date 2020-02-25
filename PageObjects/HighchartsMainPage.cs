using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace TA_Lab.PageObjects
{
    class HighchartsMainPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public HighchartsMainPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public HighchartsMainPage GoToPage()
        {
            Driver.Navigate().GoToUrl("https://www.highcharts.com/");
            return new HighchartsMainPage();
        }

        [FindsBy(How = How.XPath, Using = "//a[text()='Demo ']")]
        private IWebElement DemoButton;

        [FindsBy(How = How.XPath, Using = "//a[text()='Highcharts demos']")]
        private IWebElement AdvancedDemoButton; 

        public HighchartsDemosPage GoToDemos()
        {
            DemoButton.Click();
            AdvancedDemoButton.Click();
            return new HighchartsDemosPage();
        }
    }
}
