using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using OpenCartProject.Dashboard;
using OpenQA.Selenium;
using Selenium_Automation.Base;
using SeleniumAutomation.Utilities;
using System;
using System.Diagnostics;
using System.Reflection;

namespace SeleniumAutomation.Base
{
    [TestClass]
    public class BaseClass
    {
        public static IWebDriver Driver;
        ChromeBrowser chromeBrowser;
        public static AutomationUtilities autoUtilities = new AutomationUtilities();
        public static string TestName, SystemName, OsName;
        public string browser = AutomationUtilities.getProperty("Browser", AutomationUtilities.GetConfigTextFilePath());
        public string url = AutomationUtilities.getProperty("Url", AutomationUtilities.GetConfigTextFilePath());
        public string modeofExecution = AutomationUtilities.getProperty("ExecutionMode", AutomationUtilities.GetConfigTextFilePath());
        public static string token = AutomationUtilities.getProperty("token", AutomationUtilities.GetConfigTextFilePath());
        public static string ws_name= AutomationUtilities.getProperty("ws_name", AutomationUtilities.GetConfigTextFilePath());
        public static string project_name = AutomationUtilities.getProperty("project_name", AutomationUtilities.GetConfigTextFilePath());
        public static string status= AutomationUtilities.getProperty("status", AutomationUtilities.GetConfigTextFilePath());
        
  
        public static TestContext testContext;

        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }
       
        [AssemblyInitialize]
        public static async void SetupGlobalAsync(TestContext context)
        {
            testContext = context;
            await AresDashboard.CreateRunIDDetails(token, ws_name, project_name, status);    
        }
      
        public void FinishSetup(string Browser, string TestClassName, string ModuleName, string startTime, string endTime)
        {
            bool passFlag = false;
            string testCaseName = testContext.TestName.ToString();
            string ScreenshotPathAndName = "", error = string.Empty, errorstacktrace = string.Empty;
            string status = testContext.CurrentTestOutcome.ToString();
            try
            {
                String testDate = DateTime.Now.ToString("yyyy-MM-dd");
                SystemName = Environment.MachineName;
                OsName = Environment.OSVersion.ToString();

                if (status.Equals("Failed"))
                {
                    error = "error";
                    errorstacktrace = "Stack Trace";
                    if (Driver != null)
                    {
                        ScreenshotPathAndName = autoUtilities.getScreenshot(Driver, true);
                    }
                    string ScreenshotPath = ScreenshotPathAndName.Split('#')[0];
                    AresDashboard.PostDataToDashboard(ModuleName, testCaseName, status.ToUpper(), "-", errorstacktrace, Browser, SystemName, ScreenshotPath, "-", "DESKTOP", OsName,
                    testDate, startTime, endTime, project_name, "Author", error, modeofExecution, "-");
                   
                }

                else if (status.Equals("Passed"))
                { 
                    AresDashboard.PostDataToDashboard(ModuleName, testCaseName, status.ToUpper(), "-", "NA", Browser, SystemName, "NA", "NA", "DESKTOP", OsName,
                    testDate, startTime, endTime, project_name, "Author", "NA", modeofExecution, "-");
                }
               
            }
            catch (Exception e)
            {
                if (!passFlag)
                    Console.WriteLine("Test is failed .. please refer to below screenshot and log files attached for more details:" + e);
                else
                    Console.WriteLine("Test Passed .. please refer to below log file attached for detailed logs:" + e);
            }
        }

        /// <summary>
        ///This method selects the required browsers from Config file, instantiates respective browser driver returns the driver.
        /// </summary>
        /// <params>None</params>
        /// <return>Webdriver Instance</returns>
        public IWebDriver InitialSetupWebdriver()
        {
            try
            {
                IWebDriver _driver = SelectBrowser(Driver, browser);
            
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Navigate().GoToUrl(url);
                return _driver;
            }
            catch (WebDriverException E)
            {
                Assert.Fail("Error while Initializing the Webdriver class" + E.Message);
                return null;
            }
        }

        /// <summary>
        ///  Method for selecting Browser by providing browsere type  along the with WebDriver Driver instances . If the browser type is null it will take as ff other than chrome.ie,ff/firefox or null ,  it will provide an error message of invalid browser type. 
        /// </summary>
        /// <params>Driver instance</params>
        /// <return>WebDriver Instance</returns>
        public IWebDriver SelectBrowser(IWebDriver _driver, string browser)
        {
            string sType = browser;
            switch (sType)
            {
                case "chrome":
                    chromeBrowser = new ChromeBrowser();
                    _driver = chromeBrowser.InitChromeDriver(testContext);
                    break;
                case "ff":
                case "firefox":
                    _driver = new FirefoxBrowser().GetFirefoxDriver();
                     break;
            }
            return _driver;
        }

        /// <summary>
        ///This method launches the URL given in the Config file
        /// </summary>
        /// <params>Url</params>
        /// <return>Void</returns>
        public void NavigateToUrl(string URL)
        {
            Driver.Navigate().GoToUrl(URL);
        }

    }
}