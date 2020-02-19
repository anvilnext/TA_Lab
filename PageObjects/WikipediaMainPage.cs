using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TA_Lab.Additional;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace TA_Lab.PageObjects
{
    class WikipediaMainPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public WikipediaMainPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        [FindsBy(How = How.TagName, Using = "img")]
        private IList<IWebElement> Images;

        public WikipediaMainPage GoToPage()
        {
            Driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            return new WikipediaMainPage();
        }

        public Bitmap TakeScreenshot()
        {
            var scMaker = new ScreenshotMaker();
            var vcd = new VerticalCombineDecorator(scMaker);
            vcd.SetWaitAfterScrolling(TimeSpan.FromMilliseconds(700));
            var Scr = Driver.TakeScreenshot(vcd);
            return Image.FromStream(new MemoryStream(Scr)) as Bitmap;
        }

        public void GetEssentialImagesScreenShot()
        {
            var img = TakeScreenshot();
            for (int i = 0; i < Images.Count; i++)
            {
                if (Images[i].Size.Width >= 120)
                {
                    Bitmap res = img.Clone(new Rectangle(Images[i].Location, Images[i].Size), img.PixelFormat);
                    res.Save(Helper.SetManyWiki(i));
                }
            }
        }
    }
}
