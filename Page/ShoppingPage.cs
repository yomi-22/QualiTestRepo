using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Qualitest.Page
{
    public class ShoppingPage
    {
        protected IWebDriver WebDriver;
        protected readonly WebDriverWait Wait;
        protected Actions Action;
        public ShoppingPage(IWebDriver driver)
        {
            Guard.ArgumentNotNull(driver, nameof(driver));
            WebDriver = driver;
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
            Action = new Actions(WebDriver);
        }
        private IList<IWebElement> ProductColumns => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("ul[class='products columns-3'] > li")));
        private IWebElement Cart => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[href='https://cms.demo.katalon.com/cart/']")));
        private IList<IWebElement> ProductsInCart => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("form > table > tbody > tr")));
        private IList<IWebElement> SubtotalPrice => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("td.product-subtotal")));
        private IList<IWebElement> ProductDetails => Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("tr[class=\"woocommerce-cart-form__cart-item cart_item\"]")));
        private IWebElement RemoveItem => Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("td[class=\"product-remove\"]")));
        private By AddToCartElement => By.CssSelector("a.button.product_type_simple.add_to_cart_button.ajax_add_to_cart");


        public void AddItems()
        {
            Random random = new Random();
            random.Next(0, 4);
            foreach (var product in ProductColumns)
            {
                var addToItem = product.FindElement(AddToCartElement);
                Action.MoveToElement(addToItem).Perform();
                Console.WriteLine(product);
                addToItem.Click();
            }
        }

        public int TotalItemInCart()
        {
            var totalItem = ProductsInCart.Count();
            return totalItem - 1;
        }
        public void ViewCart()
        {
            Cart.Click();
        }

        public double GetLowestPriceInCart()
        {
            double getPrice = 0;

            List<double> prices = new List<double>();
            foreach (var subPrice in SubtotalPrice)
            {
                string price = subPrice.Text;

                var subPriceConvert = double.Parse(price);
                prices.Add(subPriceConvert);
                prices.Sort();

                getPrice = prices.Min();
            }
            return getPrice;
        }

        public List<string> GetProductDetails => ProductDetails
            .Where(x => !string.IsNullOrWhiteSpace(x.Text))
            .Select(s => s.Text)
            .ToList();

        public void DeleteLowestItemFromCart()
        {
            GetLowestPriceInCart();
            try
            {
                GetProductDetails.Contains(GetLowestPriceInCart().ToString());
                RemoveItem.Click();
            }
            catch (Exception)
            {

                throw new Exception("Item not found try again");
            }
        }
    }
}
