using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Chrome;
using SeleniumAutomation.Utilities;

namespace SeleniumAutomation.Base
{
    class ChromeBrowser
    {
        private IWebDriver driver;
        private static ILog Log = LogManager.GetLogger("ChromeBrowser");
        private ChromeOptions options = new ChromeOptions();
        AutomationUtilities _autoUtils = new AutomationUtilities();

        public static IWebDriver WebInstance { get; internal set; }
        public static TestContext testContext;
        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        /// <summary>
        ///Set up the chrome driver 
        /// </summary>
        /// <return>Webdriver Instance</returns>
        public IWebDriver Driver
        {
            get
            {
                driver = InitChromeDriver(testContext);
                return driver;
            }
        }

        /// <summary>
        ///Method for initiating ChromeDriver
        /// </summary>
        /// <params>None</params>
        /// <return>Webdriver Instance</returns>

        public IWebDriver InitChromeDriver(TestContext context)
        {
            testContext = context;
            Log.Info("Launching google chrome with new profile..");
            Log.Info("chrome driver initialized..");
            return new ChromeDriver(DriverLocation, Options);
        }


        /// <summary>
        ///  Set up for Chrome Options
        /// </summary>

        private ChromeOptions Options
        {
            get
            {
                options.AddArgument("--disable-background-mode");
                options.AddArgument("--start-maximized");
                return options;
            }
        }

        /// <summary>
        ///Method to retrieve Chrome Driver Path
        /// </summary>
        /// <params>None</params>
        /// <return>Returning the Driver Path as String</returns>

        public string DriverLocation
        {
            get
            {
                return AutomationUtilities.GetProjectLocation() + @"\Drivers";

            }
        }







    }
}
