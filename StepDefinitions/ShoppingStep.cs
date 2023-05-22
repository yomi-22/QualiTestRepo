using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Qualitest.Helper;
using Qualitest.Page;

namespace Qualitest.StepDefinitions
{
    [Binding]
    public sealed class ShoppingStep : SeleniumDefinition
    {
        public ShoppingStep(IWebDriver driver)
        {
            WebDriver = driver;
        }

        [BeforeScenario]
        public void Setup()
        {
            ShoppingPage = new ShoppingPage(WebDriver);
        }

        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            WebDriver.Navigate().GoToUrl("https://cms.demo.katalon.com/");
            ShoppingPage.AddItems();
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            ShoppingPage.ViewCart();
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            int expectedTotalItem = 4;
            ShoppingPage.TotalItemInCart().Should().Be(expectedTotalItem);
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {
            ShoppingPage.GetLowestPriceInCart();
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            ShoppingPage.DeleteLowestItemFromCart();
        }

        [Then(@"I am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            ShoppingPage.ViewCart();
            ShoppingPage.TotalItemInCart();
        }
    }
}