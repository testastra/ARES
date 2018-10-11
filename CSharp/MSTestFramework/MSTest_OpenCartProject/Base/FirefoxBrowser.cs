using System;
using System.IO;
using System.Configuration;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using SeleniumAutomation.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;

namespace Selenium_Automation.Base
{
    class FirefoxBrowser
    {
        FirefoxProfile firefoxProfile = new FirefoxProfile();
        IWebDriver driver;
        private static ILog Log = LogManager.GetLogger("FirefoxBrowser");
        AutomationUtilities _autoUtils = new AutomationUtilities();
        FirefoxOptions options = new FirefoxOptions();

        public static IWebDriver WebInstance { get; internal set; }
        public static TestContext testContext;
        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        /// <summary>
        /// Method to setUp The Firefox Driver
        /// </summary>
        /// <params>None</params>
        /// <return>WebDriver instance</returns>
        public IWebDriver GetFirefoxDriver()
        {
            // get profile path from config.txt if any
            if (IsProfilePresent())
            {
                driver = FirefoxDriver(ProfileLocation, testContext);
            }
            else
            {
                driver = FirefoxDriver(testContext);
            }
            driver.Manage().Window.Maximize();
            return driver;
        }

        /// <summary>
        /// Sets the profile using local profile path
        /// </summary>
        /// <param name="profilePath"></param>
        /// <returns></returns>
        public IWebDriver FirefoxDriver(String profilePath, TestContext context)
        {
            testContext = context;
            var driverService = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");

            firefoxProfile = new FirefoxProfile(profilePath) { EnableNativeEvents = false };
            var options = new FirefoxOptions
            {
                Profile = firefoxProfile
            };

            FirefoxDriver driver = new FirefoxDriver(driverService, options, TimeSpan.FromMinutes(1));

            return driver;
        }

        /// <summary>
        /// Sets the FF profile using preferences
        /// </summary>
        /// <param name="profilePath"></param>
        /// <returns></returns>
        public IWebDriver FirefoxDriver(FirefoxProfile profilePath,TestContext context)
        {
            testContext = context;
            var driverService = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");
            options.Profile = CustomFirefoxProfile;
            FirefoxDriver driver = new FirefoxDriver(driverService, options, TimeSpan.FromMinutes(1));

            return driver;
        }

        /// <summary>
        /// Method for getting the firefox Driver
        /// </summary>
        /// <params>None</params>
        /// <return>WebDriver instance</returns>    
        public IWebDriver FirefoxDriver(TestContext context)
        {
            testContext = context;
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");
            service.HideCommandPromptWindow = true;
            service.SuppressInitialDiagnosticInformation = true;

            // Setting ff profile
            options.Profile = CustomFirefoxProfile;

            if (IsBinaryPathPresent())
                service.FirefoxBinaryPath = BinaryLocation;

            FirefoxDriver driver = new FirefoxDriver(service, options, TimeSpan.FromMinutes(1));

            return driver;
        }

        /// <summary>
        ///Firefox profile set up
        /// </summary>
        /// <return>firefoxProfile</returns>
        public FirefoxProfile CustomFirefoxProfile
        {
            get
            {
                firefoxProfile.AcceptUntrustedCertificates = true;
                firefoxProfile.SetPreference("browser.download.folderList", 2);
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.openFile", "application/pdf, application/x-pdf, application/acrobat, applications/vnd.pdf, text/pdf, text/x-pdf, application/octet-stream, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/x-rar-compressed, application/zip,application/vnd.adobe.pdfxml,application/vnd.adobe.x-mars,application/vnd.fdf,application/vnd.adobe.xfdf,application/vnd.adobe.xdp+xml,application/vnd.adobe.xfd+xml");
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf, application/x-pdf, application/acrobat, applications/vnd.pdf, text/pdf, text/x-pdf, application/octet-stream, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/x-rar-compressed, application/zip,application/vnd.adobe.pdfxml,application/vnd.adobe.x-mars,application/vnd.fdf,application/vnd.adobe.xfdf,application/vnd.adobe.xdp+xml,application/vnd.adobe.xfd+xml");
                firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                firefoxProfile.SetPreference("browser.download.manager.showAlertOnComplete", false);
                firefoxProfile.SetPreference("browser.download.panel.shown", false);
                firefoxProfile.SetPreference("browser.download.manager.closeWhenDone", true);
                firefoxProfile.SetPreference("browser.tabs.remote.force-enable", true);
                firefoxProfile.SetPreference("accessibility.force_disabled", 1);

                return firefoxProfile;
            }
        }


        /// to get the binary Location of the Firefox mentioned in the config.txt file
        public string BinaryLocation
        {
            get
            {
                return AutomationUtilities.getProperty("FirefoxBinaryPath", AutomationUtilities.GetConfigTextFilePath());
            }
        }

        /// to get the Firefox profile location for firefox
        public string ProfileLocation
        {
            get
            {
                return ConfigurationManager.AppSettings["FirefoxProfilePath"];
            }
        }
        
        /// <summary>
        ///Method for checking whether the BinaryPath is present or not
        /// </summary>
        /// <params>none</params>
        /// <return>boolean i.e True/false</returns>
        public bool IsBinaryPathPresent()
        {
            return File.Exists(BinaryLocation);
        }

        /// <summary>
        /// Method for checking whether the profile is present or not 
        /// </summary>
        /// <params>none</params>
        /// <return>boolean i.e True/false</returns>
        public bool IsProfilePresent()
        {
            return File.Exists(ProfileLocation);
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
