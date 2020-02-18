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

            string path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                path = System.IO.Directory.GetParent(path).FullName;
            }
            string fileLocation = path + "\\Screenshots\\Test1_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";

            Driver.Manage().Window.Maximize();

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreEqual(1, MainGoogle.SearchFirstPage(word));
            MainGoogle.TakeScreenshot(fileLocation);
        }

        [TestMethod]
        public void GoogleAnyPageSearch()
        {
            string query = "furniture";
            string word = "Bobs.com";

            string path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                path = System.IO.Directory.GetParent(path).FullName;
            }
            string fileLocation = path + "\\Screenshots\\Test2_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";

            Driver.Manage().Window.Maximize();

            MainGoogle.GoToPage().InvokeSearch(query);
            Assert.AreNotEqual(0, MainGoogle.SearchPage(word));
            MainGoogle.TakeScreenshot(fileLocation);
        }
    }
}
