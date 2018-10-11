package ZenQ_Dashboard;

import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClientBuilder;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import utilities.UtilityMethods;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * AresDashboard provides the facility to store test results i.e. module data
 * and test results data to affle dashboard AresDashboard has 3 sections as Live
 * Executions, Dashboard and Detailed TestResults
 * 
 * Live Executions section - Where you will see current Run Overall Progress in
 * terms of percentage - Overall pass percentage of the current test execution -
 * List of module names in current run
 * 
 * Dashboard - Where you find graphs as Automation health, Trends, Platform
 * coverage, Module/Functional Area, Test Failure Analysis, Consistently failed
 * tests and Automation Execution Time Trend
 * 
 * Automation Trends - This graph shows the number of tests executed (passed and
 * Failed) date-wise Platform Coverage - It shows the number of tests executed
 * (passed and Failed) against different browser Top5 Consistently Failed tests
 * - If a test fails consistently, it will be shown here Module/Functional Area
 * - This graph shows the module wise tests executed both passed and failed
 * tests Test Failure Analysis - This chart represents error messages for failed
 * tests Automation Execution Time Trend - This graph represents execution time
 * of last 5 tests, you will have module selection dropdown, upon selecting
 * module graph will be updated
 * 
 * Detailed TestResults - Under this section, you find Testcase title, Date,
 * Test Result, Error Message, project name, browser name and ImageLink for
 * failed Testcase title - Test method name lie here Date - Test execution date
 * ex. 2018-09-10 Test Result - Test status whether it is Passed or Failed Error
 * Message - Test failure reason, if it assertion failure corresponding
 * assertion message will be shown here Project Name - Name of the project
 * created in dashboard Browser Name - Browser name on which execution happened
 * ImageLink - Screenshot for failed test, ideally http global link is preferred
 * 
 * Gets ARES dashboard properties from DashboardResources
 * 
 * Sends API requests to dashboard upon requests received from listener
 *
 */
public class AresDashboard {

	public static String createRunIDURL = DashboardResources.getCreateRunIdURL();
	public static String addModuleDataURL = DashboardResources.getAddModuleDataURL();
	public static String testDetailsURL = DashboardResources.getTestDetailsURL();
	public static String projectName = DashboardResources.getProjectName();
	public static String wsName = DashboardResources.getWorkspaceName();
	public static String token = DashboardResources.getProjectUserToken();
	public String projectID = DashboardResources.getProjectKey();
	public String productName = DashboardResources.getProductName();
	public String testDevice = DashboardResources.getTestDevice();
	public String projectUser = DashboardResources.getProjectUser();

	/**
	 * Sends Test Results to dashboard with the mentioned parameters This method
	 * is called at the end of testcase
	 * 
	 * 
	 * @param runID
	 *            - Run ID of current execution
	 * @param suiteName
	 *            - test suite name
	 * @param sModuleName
	 *            - module name
	 * @param sTestCaseName
	 *            - name of the test case
	 * @param sTestStatus
	 *            - test execution status
	 * @param imagePath
	 *            - screenshot path if test fails
	 * @param errorMessage
	 *            - test execution error message
	 * @param videoLink
	 *            - video link of test execution
	 * @param sBrowserName
	 *            - browser name on which execution happened
	 * @param sStartTime
	 *            - test start time
	 * @param sEndTime
	 *            - test end time
	 * @return HttpResponse - HttpResponse object will be returned
	 */
	public HttpResponse postTestResults(String runID, String suiteName, String sModuleName, String sTestCaseName,
			String sTestStatus, String imagePath, String errorMessage, String videoLink, String sBrowserName,
			String sStartTime, String sEndTime) {
		HttpResponse response = null;
		String URL = testDetailsURL;
		HttpClient client = HttpClientBuilder.create().build();
		HttpPost post = new HttpPost(URL);
		post.setHeader("usertoken", token);
		post.setHeader("ProjectId", projectID);
		post.setHeader("Content-type", "application/json");

		StringEntity params = null;
		try {

			String jsonBody = "{\n" + " \"graphData\": {\n" + "   \n" + "    \"runId\": \"" + runID + "\",\n"
					+ "    \"productName\": \"" + productName + "\",\n" + "    \"moduleName\": \"" + sModuleName
					+ "\",\n" + "    \"testcaseTitle\": \"" + sTestCaseName + "\",\n" + "    \"testStatus\": \""
					+ sTestStatus + "\",\n" + "    \"testData\": \"-\",\n" + "    \"failStacktrace\": \"-\",\n"
					+ "    \"testBrowser\": \"" + sBrowserName + "\",\n" + "    \"testMachine\": \""
					+ UtilityMethods.machineName() + "\",\n" + "    \"imageLink\": \"" + imagePath + "\",\n"
					+ "    \"videoLink\": \"" + videoLink + "\",\n" + "    \"testDevice\": \"" + testDevice + "\",\n"
					+ "    \"testOs\": \"" + System.getProperty("os.name") + "\",\n" + "    \"testDate\": \""
					+ new SimpleDateFormat("yyyy-MM-dd").format(new Date()) + "\",\n" + "    \"testStartTime\": \""
					+ sStartTime + "\",\n" + "    \"testEndTime\": \"" + sEndTime + "\",\n" + "     \"testSuite\": \""
					+ suiteName + "\",\n" + "    \"runBy\": \"" + projectUser + "\",\n" + "    \"errorMessage\": \""
					+ errorMessage + "\",\n" + "    \"executionMode\": \"" + UtilityMethods.modeOfExecution + "\",\n"
					+ "    \"failType\": \"-\"\n" + "  }\n" + "}";
			params = new StringEntity(jsonBody);

			System.out.println(jsonBody);
			post.setEntity(params);
			response = client.execute(post);

			String sResponse = readResponse(response);
			System.out.println(sResponse);
		} catch (IOException e) {
			e.printStackTrace();
		}

		return response;
	}

	/**
	 * Posts module level data to Affle dashboard It helps to represent
	 * Module/Functional area in dashboard It gets total number of tests in a
	 * module and updates the module specific data in dashboard
	 * 
	 * @param runID
	 *            - Run ID of current execution
	 * @param sModuleName
	 *            - Module name of project if you have more than one module
	 * @param sTestCount
	 *            - Number of tests in module
	 * @param sStatus
	 *            - status, values will be started/ended
	 */
	public void postModuleData(String runID, String sModuleName, String sTestCount, String sStatus) {

		String moduleJsonBody;
		if (sStatus.equalsIgnoreCase("started")) {

			moduleJsonBody = "{\n" + "\"token\": \"" + token + "\",\n" + " \"runId\": \"" + runID + "\",\n"
					+ " \"ws_name\": \"" + wsName + "\",\n" + " \"project_name\": \"" + projectName + "\",\n"
					+ " \"module_name\": \"" + sModuleName + "\",\n" + " \"starttime\": \""
					+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS").format(new Date()) + "\",\n"
					+ " \"totaltests\":  \"" + sTestCount + "\",\n" + " \"status\": \"" + sStatus + "\"\n" + "}\n";
		} else {
			moduleJsonBody = "{\n" + "\"token\": \"" + token + "\",\n" + " \"runId\": \"" + runID + "\",\n"
					+ " \"ws_name\": \"" + wsName + "\",\n" + " \"project_name\": \"" + projectName + "\",\n"
					+ " \"module_name\": \"" + sModuleName + "\",\n" + " \"endtime\": \""
					+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS").format(new Date()) + "\",\n"
					+ " \"totaltests\":  \"" + sTestCount + "\",\n" + " \"status\": \"" + sStatus + "\"\n" + "}\n";

		}
		StringEntity params = null;
		String URL = addModuleDataURL;

		System.out.println(moduleJsonBody);
		HttpResponse response = null;
		HttpClient client = HttpClientBuilder.create().build();
		HttpPost post = new HttpPost(URL);
		post.setHeader("Content-type", "application/json");
		try {
			params = new StringEntity(moduleJsonBody);
			post.setEntity(params);
			response = client.execute(post);
			String sResponse = readResponse(response);
			System.out.println("MODULE DATA RESPONSE : " + sResponse);
		} catch (IOException e) {
			e.printStackTrace();
		}

	}

	/**
	 * Creates run id for every execution This method is called once for every
	 * execution at listener level in static block Created Run Id will be used
	 * for subsequent API calls in current execution
	 * 
	 * @param sStatus
	 *            - status, value will be started
	 * @return - Run id will be returned as string object
	 */
	public static String createRunIDDetails(String sStatus) {

		String jsonBody = "{\n" + "\"token\": \n" + "\"" + token + "\",\n" + " \"ws_name\": \"" + wsName + "\",\n"
				+ " \"project_name\": \"" + projectName + "\",\n" + " \"status\": \"" + sStatus + "\"\n" + "}";
		StringEntity params = null;
		String URL = createRunIDURL;

		HttpResponse response = null;
		HttpClient client = HttpClientBuilder.create().build();
		HttpPost post = new HttpPost(URL);
		// add header
		post.setHeader("Content-type", "application/json");
		try {
			params = new StringEntity(jsonBody);
			post.setEntity(params);

			response = client.execute(post);
		} catch (IOException e) {
			e.printStackTrace();
		}
		String sResponse = readResponse(response);// {"data":[{"runId":"5b756de6c5ea6774444957db"}],"message":"Run
													// Id created
													// successfully","statusCode":200}
		String runID = null;
		JSONParser parser = new JSONParser();
		JSONObject json = new JSONObject();
		JSONArray array = new JSONArray();
		try {
			json = (JSONObject) parser.parse(sResponse);
			array = (JSONArray) parser.parse(json.get("data").toString());
			JSONObject array1 = (JSONObject) array.get(0);
			runID = array1.get("runId").toString();
		} catch (ParseException e) {
			e.printStackTrace();
		}

		return runID;
	}

	/**
	 * Reads response as String object from HttpResponse object
	 * 
	 * @param response
	 *            - HttpResponse will be parameterized
	 * @return - response as String will be returned to called method
	 */
	public static String readResponse(HttpResponse response) {
		BufferedReader rd = null;
		StringBuffer result = new StringBuffer();
		try {
			rd = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));

			String line = "";
			while ((line = rd.readLine()) != null) {
				result.append(line);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}

		return result.toString();

	}

}
