using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Qualitest.Helper
{
    public class WebDriverBrowserFactory
    {
        public IWebDriver CreateWebBrowser(string driverType)
        {
            switch (driverType)
            {
                case "Chrome":
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("start-maximized");
                        options.AddArgument("--disable-gpu");
                        var driver = new ChromeDriver(options);
                        return driver;
                    }

                case "Firefox":
                    {
                        FirefoxOptions options = new FirefoxOptions();
                        options.AddArgument("start-maximized");
                        options.AddArgument("--disable-gpu");
                        var driver = new FirefoxDriver(options);
                        return driver;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(driverType),
                        $"{driverType} is not a recognised browser type.");
            }
        }
    }
}

