using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TA_Lab.PageObjects
{
    class RozetkaSearchResultsPage
    {
        private IWebDriver Driver => WebDriverBase.GetDriver();

        public RozetkaSearchResultsPage()
        {
            PageFactory.InitElements(Driver, this);
            Driver.Manage().Window.Maximize();
        }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='min']")]
        private IWebElement MinPriceField;

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='max']")]
        private IWebElement MaxPriceField;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement OKButton;

        private void Wait()
        {
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            //wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public RozetkaSearchResultsPage SetMinPrice(int price)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            MinPriceField.Clear();
            MinPriceField.SendKeys(price.ToString());
            OKButton.Click();
            Thread.Sleep(5000);

            return new RozetkaSearchResultsPage();
        }

        public int GetMinPrice()
        {
            return int.Parse(MinPriceField.GetAttribute("value"));
        }

        public int GetMaxPrice()
        {
            return int.Parse(MaxPriceField.GetAttribute("value"));
        }

        public bool CheckIfInRange()
        {
            IList<IWebElement> Results = Driver.FindElements(By.ClassName("goods-tile__price-value"));
            foreach (IWebElement elem in Results)
            {
                int t = int.Parse(elem.Text.Replace(" ", string.Empty));
                if ((t < GetMinPrice()) & (t > GetMaxPrice()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
