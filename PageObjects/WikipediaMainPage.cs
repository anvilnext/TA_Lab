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

namespace TA_Lab.PageObjects
{
    class WikipediaMainPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public WikipediaMainPage()
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//img")]
        private IList<IWebElement> Images;

        public WikipediaMainPage GoToPage()
        {
            Driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            return new WikipediaMainPage();
        }

        public void GetAllScreenshots(int num)
        {
            int k = 0;

            foreach (IWebElement element in Images)
            {
                try
                {
                    var img = GetElementScreenShot(Driver, element);
                    if (img != null)
                        img.Save(Helper.SetMany(num, k));
                    k++;
                }
                catch (OutOfMemoryException)
                {
                    continue;
                }
            } 
        }

        public Bitmap GetElementScreenShot(IWebDriver Driver, IWebElement element)
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element).Perform();

            Screenshot Sc = ((ITakesScreenshot)Driver).GetScreenshot();

            var img = Image.FromStream(new MemoryStream(Sc.AsByteArray)) as Bitmap;
            return img.Clone(new Rectangle(element.Location, element.Size), img.PixelFormat);
        }
    }
}
