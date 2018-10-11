using log4net;
using OpenQA.Selenium;
using SeleniumAutomation.Base;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SeleniumAutomation.Utilities
{
    public class AutomationUtilities
    {
        public int count = 0;
        public static ILog log = LogManager.GetLogger("UtilityMethods");

        /// <summary>
        ///  Method for getting the key value from config file
        /// </summary>
        /// <params>section name and keyname</params>
        /// <return>String </returns
        public string GetKeyValue(string sectionname, string keyname)
        {
            var section = (ConfigurationManager.GetSection(sectionname) as NameValueCollection);
            return section[keyname];
        }

        /// <summary>
        ///  Method for Getting the Project location
        /// </summary>
        /// <params>No params</params>
        /// <return>String </returns

        public static string GetProjectLocation()
        {
            string sDirectory = Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            if (Environment.StackTrace.Contains("MSTestFramework"))
                sDirectory = sDirectory + @"\MSTest_OpenCartProject";
            Console.WriteLine("Full project path:" + sDirectory);
            return sDirectory;
        }
      
        public string getScreenLocation()
        {
            string sDirectory = GetAutomationReportsLocation();
            string PathToscreenshot = Path.Combine(sDirectory, "screenshots\\");
            string today = DateTime.Now.ToString("MM-dd-yyyy");
            if (!Directory.Exists(PathToscreenshot))
            {
                Directory.CreateDirectory(PathToscreenshot);
            }
            foreach (string dir in Directory.GetDirectories(PathToscreenshot))
            {
                if (!dir.Contains(today))
                {
                    Directory.Delete(dir, true);
                }
            }
            PathToscreenshot = Path.Combine(PathToscreenshot, today);
            if (!Directory.Exists(PathToscreenshot))
            {
                Directory.CreateDirectory(PathToscreenshot);
                log.Info("===>Directory created:=>" + PathToscreenshot);
            }
            return PathToscreenshot;
        }

        /// <summary>
        ///   Method to get the Current Directory Loaction
        /// </summary>
        /// <params>No params</params>
        /// <return>String </returns

        public static string GetCurrentDirectoryPath()
        {
            return Environment.CurrentDirectory;
        }
               
        /// <summary>
        ///  Method for taking screen shot 
        /// </summary>
        /// <params>driver,screenshotPath as Stringg</params>
        /// <return>Boolan i.e True/false</returns>
        public static bool TakeScreenshot(IWebDriver driver, string screenshotPath)
        {
            bool flag;
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ///Use it as you want now
                string screenshot = ss.AsBase64EncodedString;
                byte[] screenshotAsByteArray = ss.AsByteArray;
                ss.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png); //use any of the built in image formating
                ss.ToString();///same as string screenshot = ss.AsBase64EncodedString
                flag = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                flag = false;
            }
            return flag;
        }


        /// <summary>
        ///  getScreenshot
        /// </summary>
        /// Purpose: Capture the screenshot and merge with Gallio Report
        /// <return>Bitmap</returns>

        public string getScreenshot(IWebDriver _driver, Boolean flag = false)
        {
            string fileName = null, simpleFileName = "";
            string testName = BaseClass.testContext.TestName.ToString();
            try
            {
                string[] initialSuiteName = testName.Split('(');
                string PathToscreenshot = getScreenLocation();
                if (testName.Contains("("))
                {
                    string trimmedSuite = RemoveSpecialCharacters(testName);
                    trimmedSuite = trimmedSuite.Replace(".", string.Empty);
                    if (trimmedSuite.Length <= 60)
                    {
                        fileName = PathToscreenshot + "\\" + trimmedSuite + ".png";
                        simpleFileName = trimmedSuite;
                    }
                    else
                    {
                        trimmedSuite = trimmedSuite.Substring(0, 60);
                        fileName = PathToscreenshot + "\\" + trimmedSuite + ".png";
                        simpleFileName = trimmedSuite;
                    }
                }
                else
                {
                    fileName = PathToscreenshot + "\\" + testName + ".png";
                    simpleFileName = testName;
                }

                FileInfo f = new FileInfo(fileName);
                if (f.Exists)
                {
                    f.Delete();
                }
                ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                using (var stream = new MemoryStream(screenshot.AsByteArray))
                {
                    var image = new Bitmap((stream));
                }
                Console.WriteLine("Screenshot: {0}", new Uri(fileName));
            }
            catch (ArgumentNullException e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                _driver.Quit();
                throw new Exception("Failed to take screenshot..." + e.Message);
            }

            if (flag)
                return fileName + "#" + simpleFileName;
            else
                return fileName;
        }

        /// <summary>
        /// Gets data from text file based on key
        /// </summary>
        /// <params> filepath, key  </params>
        /// <return>string</returns>
        public static string GetPropertyValueFromTextFile(string filePath, string property)
        {
            string val = null;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {

                    if (line.Contains(property))
                    {
                        val = line.Split('=')[1];
                        return val.Trim();
                    }
                }
                if (val == null)
                {
                    log.Error(property + " property/key doenot exist in specified file");
                }
                return val;
            }
            catch (FileNotFoundException e)
            {
                log.Error("File is not present in " + filePath + " location" + e.Message);
                throw new FileNotFoundException("File is not present in " + filePath + " location");
            }
        }

        /// <summary>
        /// write property(data) to text file
        /// </summary>
        /// <params>  filepath, property to write </params>
        /// <return>bool</returns>
        public static bool writetDataToTextFile(string filePath, string property)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, true);

                sw.WriteLine("\n" + property);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }
        }


        /// <summary>
        /// Method for writing/Updating to the Text File
        /// </summary>
        /// <params>   Boolean </params>
        /// <return>Boolean</returns>
        public static bool WriteORUpdateTextFile(string filePath, string property)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string fileContent = sr.ReadToEnd();
                string propertyName = property.Split('=')[0];
                string propertyVal = GetPropertyValueFromTextFile(filePath, propertyName);
                sr.Close();

                if (fileContent.Contains(property))
                {

                }
                else if (fileContent.Contains(propertyName))
                {

                    StreamWriter sw1 = new StreamWriter(filePath, false);
                    fileContent = fileContent.Replace(propertyName + "=" + propertyVal, property);
                    sw1.WriteLine(fileContent);
                    sw1.Close();
                }
                else
                {

                    StreamWriter sw2 = new StreamWriter(filePath, true);
                    sw2.WriteLine(property);
                    sw2.Close();

                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }

        }

        /// <summary>
        /// Gets the key from the properties file
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string getProperty(string Key, string FilePath)
        {         
            string[] lines = System.IO.File.ReadAllLines(FilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string FileKey = Regex.Split(lines[i], "=")[0].Trim();
                if (FileKey == Key)
                    return Regex.Split(lines[i], "=")[1].Trim();
            }
            return "-";
        }


        public static string GetBasePath()
        {
            string BaseDirToProjects = Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            return BaseDirToProjects;
        }

        public static string GetFilePath(string fileName)
        {
            return GetBasePath() + @"\" + fileName + ".txt";
        }

        public static string GetConfigTextFilePath()
        {
            return GetBasePath()+ @"\MSTest_OpenCartProject\Config.txt";
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

          
        public string GetScreenshotRelativePath(string testname)
        {
           string imagepath = @".\..\screenshots\" + System.DateTime.Today.ToString("MM-dd-yyyy") + "/" + testname + ".png";
            return imagepath;
        }
  
        /// <summary>
        ///   Method to get AutomationReports location
        /// </summary>
        /// <param></param>
        /// <returns>string</returns>
        public string GetAutomationReportsLocation()
        {
            string path = Directory.GetParent(AutomationUtilities.GetProjectLocation()).ToString();
            path = Path.Combine(path, "Automation_Report");
            return path;
        }
    }

}