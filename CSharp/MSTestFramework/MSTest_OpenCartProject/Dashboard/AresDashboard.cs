using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenCartProject.Dashboard
{
    class AresDashboard
    {
        private static string pathToPropertiesFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Rundetails.txt";
        private static string pathToJsonFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\file.json";
        private static JArray jArray = new JArray();
        private static string dashboard_url= AutomationUtilities.getProperty("Dashboard_url", AutomationUtilities.GetConfigTextFilePath());
        private static string createrunid_url= AutomationUtilities.getProperty("CreateRunIDUrl", AutomationUtilities.GetConfigTextFilePath());
        private static string addmoduledata_url = AutomationUtilities.getProperty("AddMedhokDataUrl", AutomationUtilities.GetConfigTextFilePath());
        private static string testdetails_url = AutomationUtilities.getProperty("TestDetailsUrl", AutomationUtilities.GetConfigTextFilePath());
        private static string project_key = AutomationUtilities.getProperty("project_id", AutomationUtilities.GetConfigTextFilePath());
        private static string user_key = AutomationUtilities.getProperty("token", AutomationUtilities.GetConfigTextFilePath());

        public static async Task CreateRunIDDetails(string userKey, string wsName, string projectName, string status)
        {
            JObject obj = new JObject();
            obj.Add("token", userKey);
            obj.Add("ws_name", wsName);
            obj.Add("project_name", projectName);
            obj.Add("status", status);
            var serializedObj = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serializedObj.ToString(), Encoding.UTF8, "application/json");
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(dashboard_url);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(createrunid_url),
                        Method = HttpMethod.Post,
                    };
                    request.Content = stringContent;
                    Console.WriteLine("Posting run details to dashboard");
                    var response = client.SendAsync(request).Result;
                    string receiveStream = await response.Content.ReadAsStringAsync();
                    JObject responseObj = JObject.Parse(receiveStream.ToString());
                    string runId = (string)responseObj.SelectToken("data[0].runId");
                    WriteRunDetailsToTextFile(userKey, wsName, projectName, status, runId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to post run details" + e.Message);
            }
        }

        /// <summary>
        /// Write run details to text file
        /// </summary>
      
        private static void WriteRunDetailsToTextFile(string userKey, string wsName, string projectName, string status, string runId)
        {
            string[] lines = File.ReadAllLines(pathToPropertiesFile);
            string[] runDetails = new string[] { userKey, wsName, projectName, status, runId };
            for (int i = 0; i <lines.Length; i++)
            {
                lines[i] = lines[i].Substring(0, lines[i].LastIndexOf("=") + 1);
                lines[i] = lines[i] + runDetails[i];
            }
            File.WriteAllLines(pathToPropertiesFile, lines);
        }


        public static void PostModuleDetails(string modulename, string startTime, int totalTests, string status)
        {
            JObject jObject = new JObject();
            jObject.Add("token", GetProperty("token"));
            jObject.Add("runId", GetProperty("runId"));
            jObject.Add("ws_name", GetProperty("ws_name"));
            jObject.Add("project_name", GetProperty("project_name"));
            jObject.Add("module_name", modulename);
            jObject.Add("starttime", startTime);
            jObject.Add("totaltests", totalTests);
            jObject.Add("status", status);
            string jsonString = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(dashboard_url);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(addmoduledata_url),
                        Method = HttpMethod.Post,
                    };
                    request.Content = stringContent;
                    Console.WriteLine("Posting module details to dashboard");
                    var response = client.SendAsync(request).Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to post module details" + e.Message);
            }
        }

        private static string GetProperty(string key)
        {
            string[] lines = File.ReadAllLines(pathToPropertiesFile);
            for (int i = 0; i < lines.Length; i++)
            {
                string FileKey = Regex.Split(lines[i], "=")[0].Trim();
                if (FileKey == key)
                    return Regex.Split(lines[i], "=")[1].Trim();
            }
            return "-";
        }
   
        public static void PostDataToDashboard(string moduleName,
            string testCaseTitle, string testStatus, string testData, string failStacktrace, string testBrowser,
            string testMachine, string imageLink, string videoLink, string testDevice, string testOs, string testDate,
            string testStartTime, string testEndTime, string testSuite, string runBy,
            string errorMessage, string executionMode, string failType)
        {
            JObject obj = new JObject();
            JObject obj1 = new JObject();
            obj.Add("runId", GetProperty("runId"));
            obj.Add("productName", GetProperty("project_name"));
            obj.Add("moduleName", moduleName);
            obj.Add("testcaseTitle", testCaseTitle);
            obj.Add("testStatus", testStatus);
            obj.Add("testData", testData);
            obj.Add("failStacktrace", failStacktrace);
            obj.Add("testBrowser", testBrowser);
            obj.Add("testMachine", testMachine);
            obj.Add("imageLink", imageLink);
            obj.Add("videoLink", videoLink);
            obj.Add("testDevice", testDevice);
            obj.Add("testOs", testOs);
            obj.Add("testDate", testDate);
            obj.Add("testStartTime", testStartTime);
            obj.Add("testEndTime", testEndTime);
            obj.Add("testSuite", testSuite);
            obj.Add("runBy", "Test");
            obj.Add("errorMessage", errorMessage);
            obj.Add("executionMode", executionMode);
            obj.Add("failType", "-");
            obj1.Add("graphData", obj);
            string jsonString = JsonConvert.SerializeObject(obj1);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                       
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(dashboard_url);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(testdetails_url),
                        Method = HttpMethod.Post,
                    };
                    request.Headers.Add("usertoken", user_key);
                    request.Headers.Add("ProjectId", project_key);
                    request.Content = stringContent;
                    Console.WriteLine("Posting test data to dashboard");
                    var response = client.SendAsync(request).Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to post test data" + e.Message);
            }

        }

    }
}
