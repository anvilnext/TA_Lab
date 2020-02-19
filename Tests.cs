using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TA_Lab.PageObjects;
using TA_Lab.Additional;

namespace TA_Lab
{
    [TestClass]
    public class Tests
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();
        private GoogleMainPage MainGoogle = new GoogleMainPage();
        private WikipediaMainPage MainWiki = new WikipediaMainPage();

        [TestMethod]
        public void GoogleFirstPageSearch()
        {
            string query = "furniture";
            string word = "Amazon";

            Driver.Manage().Window.Maximize();
            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreEqual(1, MainGoogle.SearchFirstPage(word));
            MainGoogle.TakeScreenshot(Helper.SetLocation(1));
        }

        [TestMethod]
        public void GoogleAnyPageSearch()
        {
            string query = "furniture";
            string word = "Bobs.com";

            Driver.Manage().Window.Maximize();
            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreNotEqual(0, MainGoogle.SearchPage(word));
            MainGoogle.TakeScreenshot(Helper.SetLocation(2));
        }

        [TestMethod]
        public void WikipediaImages()
        {
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            MainWiki.GoToPage().GetAllScreenshots(3);
        }

        [ClassCleanup]
        public static void Close()
        {
            WebDriverBase.CloseDriver();
        }
    }
}
