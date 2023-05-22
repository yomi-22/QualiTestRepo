using BoDi;
using OpenQA.Selenium;
using Qualitest.Helper;
using System.ComponentModel;
using TechTalk.SpecFlow;

namespace Qualitest.Hook
{
    [Binding]
    public sealed class GlobalHooks : SeleniumDefinition
    {
        private readonly IObjectContainer _container;
        private const string chromeBrowser = "Chrome";

        public GlobalHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            WebDriver = new WebDriverBrowserFactory().CreateWebBrowser(chromeBrowser);
            _container.RegisterInstanceAs<IWebDriver>(WebDriver);

        }

        [AfterScenario]
        public void AfterScenario()
        {
            WebDriver = _container.Resolve<IWebDriver>();
            if (WebDriver != null)
            {
                WebDriver.Quit();
            }
        }
    }
}