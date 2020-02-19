using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using TA_Lab.Additional;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace TA_Lab.PageObjects
{
    class GoogleMainPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();
        private string XPathBase = "//h3[contains(text(),'{0}')]";

        public GoogleMainPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Window.Maximize();
        }

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement SearchField;

        public GoogleMainPage GoToPage()
        {
            Driver.Navigate().GoToUrl("https://www.google.com");
            return new GoogleMainPage();
        }

        public void InvokeSearch(string query)
        {
            SearchField.SendKeys(query);
            SearchField.SendKeys(Keys.Enter);
        }

        public int SearchFirstPage(string word)
        {
            IWebElement Res = Driver.FindElement(By.XPath(string.Format(XPathBase, word)));
            return int.Parse(Driver.FindElement(By.ClassName("cur")).Text);
        }

        public int SearchPage(string word, bool makeScr)
        {
            string[] lang = new string[] { "Next", "Следующая", "Уперед" };
            int p = 1;

            for (int i = 0; i < lang.Length; i++)
            {
                while (Driver.FindElements(By.XPath(string.Format("//span[text()='{0}']", lang[i]))).Count != 0)
                {
                    int cur_page = int.Parse(Driver.FindElement(By.ClassName("cur")).Text);
                    if (IsPresent(word) == true)
                    {
                        return cur_page;
                    }
                    else
                    {
                        if (makeScr == true)
                            TakeScreenshot(Helper.SetManyGoogle(cur_page));
                        Driver.FindElement(By.XPath(string.Format("//a[@aria-label='Page {0}']", p + 1))).Click();
                        p++;
                    }
                }
            }
            return 0;
        }

        public bool IsPresent(string word)
        {
            return Driver.FindElements(By.XPath(string.Format(XPathBase, word))).Count > 0;
        }

        public void TakeScreenshot(string fileLocation)
        {
            byte[] Scr = Driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker().RemoveScrollBarsWhileShooting()));
            Bitmap img = Image.FromStream(new MemoryStream(Scr)) as Bitmap;
            img.Save(fileLocation);  
        }

        public void TakeScreenshotWithJS(string fileLocation)
        {
            IJavaScriptExecutor js = Driver as IJavaScriptExecutor;
            Image finalImage;
            // get the full page height and current browser height
            string getCurrentBrowserSizeJS =
                @"
                window.browserHeight = (window.innerHeight || document.body.clientHeight);
                window.headerHeight= document.getElementById('searchform').clientHeight;;
                window.fullPageHeight = document.body.scrollHeight;
            ";
            js.ExecuteScript(getCurrentBrowserSizeJS);


            // * This is async operation. So we have to wait until it is done.
            string getSizeHeightJS = @"return window.browserHeight;";
            int contentHeight = 0;
            while (contentHeight == 0)
            {
                contentHeight = Convert.ToInt32(js.ExecuteScript(getSizeHeightJS));
                if (contentHeight == 0) System.Threading.Thread.Sleep(10);
            }

            string getContentHeightJS = @"return window.headerHeight;";
            int siteHeaderHeight = 0;
            while (siteHeaderHeight == 0)
            {
                siteHeaderHeight = Convert.ToInt32(js.ExecuteScript(getContentHeightJS));
                if (siteHeaderHeight == 0) System.Threading.Thread.Sleep(10);
            }

            string getFullPageHeightJS = @"return window.fullPageHeight";
            int fullPageHeight = 0;
            while (fullPageHeight == 0)
            {
                fullPageHeight = Convert.ToInt32(js.ExecuteScript(getFullPageHeightJS));
                if (fullPageHeight == 0) System.Threading.Thread.Sleep(10);
            }

            if (contentHeight == fullPageHeight)
            {
                TakeScreenshotCurrentPage(fileLocation);
            }
            else
            {
                int scollEachHeight = contentHeight - siteHeaderHeight;
                int shadowAndBorder = 3;
                int scollCount = 0;
                int existsIf = (fullPageHeight - siteHeaderHeight) % scollEachHeight;
                bool cutIf = true;

                if (existsIf == 0)
                {
                    scollCount = (fullPageHeight - siteHeaderHeight) / scollEachHeight;
                    cutIf = false;
                }
                else
                {
                    scollCount = (fullPageHeight - siteHeaderHeight) / scollEachHeight + 1;
                    cutIf = true;
                }


                // back to top start screenshot
                string scollToTopJS = "window.scrollTo(0, 0)";
                js.ExecuteScript(scollToTopJS);

                byte[] imageBaseContent = ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray;
                Image imageBase;
                using (var ms = new MemoryStream(imageBaseContent))
                {
                    imageBase = Image.FromStream(ms);
                }

                finalImage = imageBase;

                string scrollBar = @"window.scrollBy(0, window.browserHeight-window.headerHeight);";
                for (int count = 1; count < scollCount; count++)
                {

                    js.ExecuteScript(scrollBar);
                    Thread.Sleep(500);
                    Byte[] imageContentAdd = ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray;
                    Image imageAdd;
                    using (var msAdd = new MemoryStream(imageContentAdd))
                    {
                        imageAdd = Image.FromStream(msAdd);
                    }

                    imageAdd.Save(fileLocation, ImageFormat.Png);
                    Bitmap source = new Bitmap(imageAdd);
                    int a = imageAdd.Width;
                    int b = imageAdd.Height - siteHeaderHeight;
                    PixelFormat c = source.PixelFormat;


                    // cut the last screen shot if last screesshot override with sencond last one
                    if ((count == (scollCount - 1)) && cutIf)
                    {

                        Bitmap imageAddLastCut =
                            source.Clone(new System.Drawing.Rectangle(0, contentHeight - existsIf, imageAdd.Width, existsIf), source.PixelFormat);

                        finalImage = combineImages(finalImage, imageAddLastCut);

                        source.Dispose();
                        imageAddLastCut.Dispose();
                    }
                    //cut the site header from screenshot
                    else
                    {
                        Bitmap imageAddCutHeader =
                            source.Clone(new System.Drawing.Rectangle(0, (siteHeaderHeight + shadowAndBorder), imageAdd.Width, (imageAdd.Height - siteHeaderHeight - shadowAndBorder)), source.PixelFormat);
                        finalImage = combineImages(finalImage, imageAddCutHeader);

                        source.Dispose();
                        imageAddCutHeader.Dispose();
                    }

                    imageAdd.Dispose();

                }

                finalImage.Save(fileLocation, ImageFormat.Png);
                imageBase.Dispose();
                finalImage.Dispose();

            }
        }

        //combine two pictures
        public static Bitmap combineImages(Image image1, Image image2)
        {
            Bitmap bitmap = new Bitmap(image1.Width, image1.Height + image2.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, 0, image1.Height);
            }

            return bitmap;
        }

        private void TakeScreenshotCurrentPage(string fileLocation)
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Screenshot S1 = ((ITakesScreenshot)Driver).GetScreenshot();
            S1.SaveAsFile(fileLocation, ScreenshotImageFormat.Png);
        }
    }
}
