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
        [TestMethod]
        public void GoogleFirstPageSearch()
        {
            GoogleMainPage MainGoogle = new GoogleMainPage();
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
            GoogleMainPage MainGoogle = new GoogleMainPage();
            string query = "furniture";
            string word = "Bobs.com";
            
            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreNotEqual((0 | 1), MainGoogle.SearchAnyPage(word));
            MainGoogle.TakeScreenshot(Helper.SetLocation(3));
        }

        [TestMethod]
        public void GoogleAnyPageNoMatchSearch()
        {
            GoogleMainPage MainGoogle = new GoogleMainPage();
            string query = "furniture stores kherson";
            string word = "EPAM";

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreEqual(true, MainGoogle.SearchPageWithScreenshotNoMatches(word)); 
        }

        [TestMethod]
        public void WikipediaImages()
        {
            WikipediaMainPage MainWiki = new WikipediaMainPage();
            MainWiki.GoToPage().GetEssentialImagesScreenShot();
        }

        [TestMethod]
        public void RozetkaFilter()
        {
            RozetkaMainPage MainRozetka = new RozetkaMainPage();
            int price = 300000;
            string query = "проекторы";

            Assert.AreEqual(true, MainRozetka.GoToPage().InvokeSearch(query).SetMinPrice(price).CheckIfInRange());
        }

        [TestMethod]
        public void HighchartsGreenLine()
        {
            HighchartsMainPage HighMain = new HighchartsMainPage();
            bool result = HighMain.GoToPage().GoToDemos().GoToAdvanced().CheckChartGreen();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void HighchartsGoogle()
        {
            HighchartsMainPage HighMain = new HighchartsMainPage();
            bool result = HighMain.GoToPage().GoToDemos().GoToAdvanced().CheckChartGoogle();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void HighchartsRevenue()
        {
            HighchartsMainPage HighMain = new HighchartsMainPage();
            bool result = HighMain.GoToPage().GoToDemos().GoToAdvanced().CheckChartRevenue();
            Assert.AreEqual(true, result);
        }

        [ClassCleanup]
        public static void Close()
        {
            WebDriverBase.CloseDriver();
        }
    }
}
