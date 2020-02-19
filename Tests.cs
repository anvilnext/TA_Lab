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
        private GoogleMainPage MainGoogle = new GoogleMainPage();
        private WikipediaMainPage MainWiki = new WikipediaMainPage();

        [TestMethod]
        public void GoogleFirstPageSearch()
        {
            string query = "furniture";
            string word = "Amazon";

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreEqual(1, MainGoogle.SearchFirstPage(word));
            MainGoogle.TakeScreenshot(Helper.SetLocation(1));
            MainGoogle.TakeScreenshotWithJS(Helper.SetLocation(2));
        }

        [TestMethod]
        public void GoogleAnyPageSearch()
        {
            string query = "furniture";
            string word = "Bobs.com";
            
            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreNotEqual((0 | 1), MainGoogle.SearchPage(word, false));
            MainGoogle.TakeScreenshot(Helper.SetLocation(3));
            MainGoogle.TakeScreenshotWithJS(Helper.SetLocation(4));
        }

        [TestMethod]
        public void GoogleAnyPageNoMatchSearch()
        {
            string query = "furniture";
            string word = "EPAM";

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreNotEqual(0, MainGoogle.SearchPage(word, true)); 
        }

        [TestMethod]
        public void WikipediaImages()
        {
            MainWiki.GoToPage().GetEssentialImagesScreenShot();
        }

        [ClassCleanup]
        public static void Close()
        {
            WebDriverBase.CloseDriver();
        }
    }
}
