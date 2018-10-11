using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCartProject.Dashboard;
using OpenQA.Selenium;
using SeleniumAutomation.Base;
using System;

namespace OpenCartProject.TestSuite
{
    [TestClass]
    public class AccountpageTests_Opencart:BaseClass
    {
        private static string startTime, endTime, ModuleName, SuiteName;     
        public static BaseClass _baseclass = new BaseClass();

        [ClassInitialize]
        public static void before_Class(TestContext context)
        {
            SuiteName = context.FullyQualifiedTestClassName.ToString();
            ModuleName = SuiteName.Substring(SuiteName.LastIndexOf(".") + 1);
            startTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss.fff");
            AresDashboard.PostModuleDetails(ModuleName, startTime, 2, "started");
        }


        [TestInitialize]
        public void beforeTest()
        {
            Driver = _baseclass.InitialSetupWebdriver();
        }

        [TestMethod]
        public void LoginToOpencartAndVerifyMyAccountOptions()
        {
            
            IWebElement myAccountDropdown = Driver.FindElement(By.XPath("//li[@class='dropdown']/a[@title='My Account']"));
            myAccountDropdown.Click();
            
            IWebElement loginLink = Driver.FindElement(By.XPath("//ul[contains(@class, 'dropdown-menu')]//a[text()='Login']"));
            loginLink.Click();
            String loginPageTitle = Driver.Title;

            Assert.IsTrue(loginPageTitle.Equals("Account Login"), "User has not navigated to Opencart login page");

            IWebElement emailAddressTextbox = Driver.FindElement(By.Id("input-email"));
            emailAddressTextbox.SendKeys("test1@gmail.com");

            IWebElement passwordTextbox = Driver.FindElement(By.Id("input-password"));
            passwordTextbox.SendKeys("12345");

            IWebElement loginBtn = Driver.FindElement(By.CssSelector("input[value='Login']"));
            loginBtn.Click();

            String myAccountPageTitle = Driver.Title;
            Assert.IsTrue(myAccountPageTitle.Equals("My Account"), "User is unable to login to Opencart, check your login credentials once");
        }

        [TestMethod]
        public void VerifyRewardPointsFromRegisterAccountPage()
        {
            IWebElement myAccountDropdown = Driver.FindElement(By.XPath("//li[@class='dropdown']/a[@title='My Account']"));
            myAccountDropdown.Click();
            String registerAccountPageTitle = Driver.Title;
            Assert.IsTrue(registerAccountPageTitle.Equals("Register Account"), "Register account page is not navigated from my account dropdown");
            IWebElement rewardPointsLinkt = Driver.FindElement(By.XPath("//div[@class='list-group']//a[text()='Reward Points']"));
            rewardPointsLinkt.Click();

            String accountLoginPage = Driver.Title;
            Assert.IsTrue(accountLoginPage.Equals("Account Login"), "Login page is not opened upon clicking on reward points link in Register account page");

            IWebElement emailAddressTextbox = Driver.FindElement(By.Id("input-email"));
            emailAddressTextbox.SendKeys("tester@gmail.com");

            IWebElement passwordTextbox = Driver.FindElement(By.Id("input-password"));
            passwordTextbox.SendKeys("tester@123");

            IWebElement loginBtn = Driver.FindElement(By.CssSelector("input[value='Login']"));
            loginBtn.Click();

            String yourRewardPointsPageTitle = Driver.Title;
            Assert.IsTrue(yourRewardPointsPageTitle.Equals("Your Reward Points"), "Reward points page is not opened");
            Console.WriteLine("Verified Reward points page from Register account page");
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
