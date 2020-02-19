using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TA_Lab
{
    static class WebDriverBase
    {
        private static IWebDriver Driver;

        public static IWebDriver GetDriver()
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
            }
            return Driver;
        }

        public static void CloseDriver()
        {
            Driver.Quit();
        }
    }
}
