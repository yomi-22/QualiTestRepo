using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Qualitest.Page;
using System;

namespace Qualitest.Helper
{
    [Binding]
    public class SeleniumDefinition : WebDriverBrowserFactory
    {

        protected IWebDriver WebDriver;
        protected Actions Action;

        protected ShoppingPage ShoppingPage { get; set; }
    }
}
