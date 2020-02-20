using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace TA_Lab.PageObjects
{
    class RozetkaMainPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public RozetkaMainPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Window.Maximize();
        }

        [FindsBy(How = How.Name, Using = "search")]
        private IWebElement SearchField;

        public RozetkaMainPage GoToPage()
        {
            Driver.Navigate().GoToUrl("https://rozetka.com.ua/");
            return new RozetkaMainPage();
        }

        public RozetkaSearchResultsPage InvokeSearch(string query)
        {
            SearchField.SendKeys(query);
            SearchField.SendKeys(Keys.Enter);
            return new RozetkaSearchResultsPage();
        }
    }
}
