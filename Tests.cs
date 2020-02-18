using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TA_Lab.PageObjects;

namespace TA_Lab
{
    [TestClass]
    public class Tests
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();
        private GoogleMainPage MainGoogle = new GoogleMainPage();

        [TestMethod]
        public void GoogleFirstPageSearch()
        {
            string query = "furniture";
            string word = "Amazon";
            string fileLocation = "D:\\Screenshots\\Test1.png";

            Driver.Manage().Window.Maximize();

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreEqual(1, MainGoogle.SearchFirstPage(word));
            MainGoogle.TakeScreenshot(fileLocation);

            WebDriverBase.CloseDriver();
        }
    }
}
