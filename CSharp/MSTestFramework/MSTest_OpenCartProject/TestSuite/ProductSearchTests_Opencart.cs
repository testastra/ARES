using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCartProject.Dashboard;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAutomation.Base;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;


namespace OpenCartProject.TestSuite
{
  [TestClass]
  public class ProductSearchTests_Opencart : BaseClass
    {
        private static string startTime, endTime, ModuleName;
        String itemName = "Nikon";
        string username = AutomationUtilities.getProperty("UserName", AutomationUtilities.GetConfigTextFilePath());
        string password = AutomationUtilities.getProperty("Password", AutomationUtilities.GetConfigTextFilePath());

        public static BaseClass _baseclass = new BaseClass();

        [ClassInitialize]
        public static void before_Class(TestContext context)
        {
            ModuleName = context.FullyQualifiedTestClassName.ToString();
            ModuleName = ModuleName.Substring(ModuleName.LastIndexOf(".") + 1);
            //Posting Module Details to DashBoard
            startTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss.fff");
            AresDashboard.PostModuleDetails(ModuleName, startTime, 3, "started");
        }

        [TestInitialize]
        public void beforeTest()
        {
            Driver = _baseclass.InitialSetupWebdriver();
        }


        [TestMethod]
        public void AddItemToCartAndVerify()
        {
            Driver.FindElement(By.XPath("//li/a[text()='Cameras']")).Click();
            Driver.FindElement(By.XPath("//div[contains(@class, 'product-layout')]//h4//a[contains(text(), 'Nikon')]")).Click();

            IWebElement productCostElement = Driver.FindElement(By.XPath("//div[@id='content']//div[@class='col-sm-4']//h2"));
            String productCost = productCostElement.Text;
            IWebElement addToCartEle = Driver.FindElement(By.XPath("//div[@id='content']//button[@id='button-cart']"));

            addToCartEle.Click();
            IWebElement itemAddedSuccessfullyAlert = Driver.FindElement(By.XPath("//div[contains(@class, 'alert-success')]"));
            Assert.IsTrue(itemAddedSuccessfullyAlert.Displayed, "Item is not added to cart upon Click on add to cart button");

            IWebElement shoppingCartLink = Driver.FindElement(By.XPath("//div[contains(@class, 'alert-success')]//a[contains(@href, 'route=checkout/cart')]"));
            shoppingCartLink.Click();
            
            IWebElement shoppingCartPage = Driver.FindElement(By.Id("checkout-cart"));
            Assert.IsTrue(shoppingCartPage.Displayed, "Shopping cart page is not displayed");

            IWebElement addedItemRemovebtn = Driver.FindElement(By.XPath("(//div[@id='content']//table[contains(@class, 'table-bordered')])[1]//td//a[contains(text(), '" + itemName + "')]/parent::td//following::button[@data-original-title='Remove']"));
            addedItemRemovebtn.Click();
           
        }

        [TestMethod]
        public void WriteReviewOnProductAndVerifyErrorMessage()
        {
            IWebElement laptopsAndNotebooksEle = Driver.FindElement(By.XPath("//li[@class='dropdown']//a[text()='Laptops & Notebooks']"));
            Actions action = new Actions(Driver);
            action.MoveToElement(laptopsAndNotebooksEle).Click(Driver.FindElement(By.XPath("//a[text()='Show All Laptops & Notebooks']"))).Build().Perform();
           
            Driver.FindElement(By.XPath("//div[contains(@class, 'product-layout')]//h4//a[contains(text(), 'HP LP3065')]")).Click();

            IWebElement reviewsTab = Driver.FindElement(By.XPath("//ul[contains(@class, 'nav-tabs')]//a[contains(text(), 'Reviews')]"));
            reviewsTab.Click();

            IWebElement yourNameTextboxInReviewform = Driver.FindElement(By.Id("input-name"));
            yourNameTextboxInReviewform.SendKeys("testing");

            IWebElement reviewContinueBtn = Driver.FindElement(By.Id("button-review"));
            reviewContinueBtn.Click();
           
            IWebElement reviewAlertErrorMssg = Driver.FindElement(By.XPath("//div[contains(@class, 'alert-danger')]"));
            Assert.IsTrue(reviewAlertErrorMssg.Displayed, "Review form error message is not displayed if user is continuing without giving review rating");

        }

        [TestMethod]
        public void VerifyItemsAddedToWhishlistFromHomepageFooterSection()
        {

            IWebElement wishlistLink = Driver.FindElement(By.XPath("//li//a[text()='Wish List' and contains(@href, 'account/wishlist')]"));
            wishlistLink.Click();
         
            IWebElement emailAddressTextbox = Driver.FindElement(By.Id("input-email"));
            emailAddressTextbox.SendKeys("tester@gmail.com");

            IWebElement passwordTextbox = Driver.FindElement(By.Id("input-password"));
            passwordTextbox.SendKeys("tester@123");

            IWebElement loginBtn = Driver.FindElement(By.CssSelector("input[value='Login']"));
            loginBtn.Click();

            IList<IWebElement> itemsInWishlist = Driver.FindElements(By.XPath("//table[contains(@class, 'table table-bordered')]/tbody/tr"));

            Console.WriteLine("Number of items in wishlist : " + itemsInWishlist.Count);

            IList<IWebElement> itemnamesInWishlist = Driver.FindElements(By.XPath("(//table[contains(@class, 'table table-bordered')]/tbody/tr//td)[2]/a"));

            foreach (IWebElement itemname in itemnamesInWishlist)
            {
                Console.WriteLine("Item name : " + itemname);
            }

        }

        [TestCleanup]
        public void TearDown()
        {
            endTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss.fff");
            string TestClassName = this.GetType().Name;
            browser = _baseclass.browser;
            FinishSetup(browser, TestClassName, ModuleName, startTime, endTime);
            Driver.Quit();
        }
    }
}
