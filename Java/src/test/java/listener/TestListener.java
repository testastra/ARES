package listener;

import ZenQ_Dashboard.AresDashboard;
import utilities.UtilityMethods;

import java.io.File;
import java.io.FileInputStream;
import java.util.NoSuchElementException;
import java.util.Properties;

import org.json.simple.JSONObject;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.testng.*;

/**
 * TestNG listeners which are called at different levels during test suite
 * execution
 *
 */
public class TestListener extends TestListenerAdapter implements ISuiteListener {

	public static AresDashboard aresDashboard;
	private String sRunID = null;
	private String testStartTime = null;
	private String testEndTime = null;
	private static String suiteName = "";
	public static String browserName;
	Properties conf;

	/**
	 * Creating AresDashboard object, and calling createRunIDDetails method
	 * which will create a unique run Id for every test execution
	 */

	public String getRunID() {
		return this.sRunID;
	}

	/**
	 * This method will be called if a test case is failed. 
	 * Purpose - For attaching captured screenshots and videos in ReportNG report
	 */
	public void onTestFailure(ITestResult result) {

		// The following code is used to post data to dashboard
		ITestContext context = result.getTestContext();
		WebDriver driver = (WebDriver) context.getAttribute("driver");
		String browserDetails = (String) ((JavascriptExecutor) driver).executeScript("return navigator.userAgent;");
		testEndTime = UtilityMethods.getDateTimeInSpecificFormat("yyyy-MM-dd HH:mm:ss.SSS");
		String testName = result.getName();
		Exception expObject = new Exception(result.getThrowable());
		String errormessage = expObject.toString().split("\n")[0].substring(20);
		errormessage = JSONObject.escape(errormessage.replaceAll("[\r\n]+", " "));

		String exceptionCategory = null;
		Exception exceptionType = new Exception(result.getThrowable());

		if (exceptionType instanceof NoSuchElementException) {
			exceptionCategory = exceptionType.getMessage().split(System.getProperty("line.separator"))[0].split(":")[0]
					.replaceAll("[\r\n]+", " ");
		} else if (exceptionType instanceof StaleElementReferenceException) {
			exceptionCategory = "element is not attached to the page document. Hence, Stale Element Reference Exception occured";
		}
		String failStackTrace = UtilityMethods.getStackTraceFromListners(result.getThrowable());
		
		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
			aresDashboard.postTestResults(getRunID(), suiteName, result.getTestContext().getName(), testName, "FAILED", "-",
					errormessage, "-", UtilityMethods.getBrowserName(browserDetails), testStartTime, testEndTime);
		}

	}

	/**
	 * This method will be called if a test case is skipped.
	 * 
	 */
	public void onTestSkipped(ITestResult result) {

	}

	/**
	 * This method will be called if a test case is passed. Purpose - For
	 * attaching captured videos in ReportNG report
	 */
	public void onTestSuccess(ITestResult result) {

		// The following code is used to post data to dashboard
		ITestContext context = result.getTestContext();
		WebDriver driver = (WebDriver) context.getAttribute("driver");
		String browserDetails = (String) ((JavascriptExecutor) driver).executeScript("return navigator.userAgent;");
		String testName = result.getName();
		testEndTime = UtilityMethods.getDateTimeInSpecificFormat("yyyy-MM-dd HH:mm:ss.SSS");
		
		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
			aresDashboard.postTestResults(getRunID(), suiteName, result.getTestContext().getName(), testName, "PASSED", "-",
					"-", "-", UtilityMethods.getBrowserName(browserDetails), testStartTime, testEndTime);	
		}
	}

	/**
	 * This method will be called before a test case is executed. Purpose - For
	 * starting video capture and launching balloon popup in ReportNG report
	 * 
	 * @param result
	 *            - ITestResult object
	 */
	@Override
	public void onTestStart(ITestResult result) {

//		 To capture test start time, used to post data to automation dashboard
		testStartTime = UtilityMethods.getDateTimeInSpecificFormat("yyyy-MM-dd HH:mm:ss.SSS");

	}

	/**
	 * Invoked after test class in instantiated
	 * 
	 * We're capturing number of tests in a module, sending tests count to dashboard in post module data method
	 */
	@Override
	public void onStart(ITestContext context) {

		int noOfTests = 0;
		ITestNGMethod[] method = context.getAllTestMethods();
		for (ITestNGMethod m : method) {
			noOfTests++;
		}

		System.out.println("ONSTART: " + context.getName());

		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
			// posting module data to dashboard at the test starting
			aresDashboard.postModuleData(getRunID(), context.getName(), String.valueOf(noOfTests), "started");
		}
	}

	/**
	 * Invokes after all the test runs
	 * 
	 * We're capturing test count and sending test count to dashboard
	 */
	@Override
	public void onFinish(ITestContext context) {

		
//		The following code captures number of tests in module	
		int noOfTests = 0;
		ITestNGMethod[] method1 = context.getAllTestMethods();
		for (ITestNGMethod m : method1) {
			noOfTests++;
		}
		
		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
//	 		posting module data to dashboard at the test ending
			aresDashboard.postModuleData(getRunID(), context.getName(), String.valueOf(noOfTests), "ended");
		}


	}

	public void onFinish(ISuite arg0) {
		
		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
			sRunID = aresDashboard.createRunIDDetails("ended");
		}
	}

	/**
	 * This method starts before suite execution starts
	 */
	public void onStart(ISuite iSuite) {

		conf = new Properties();
		try {
			conf.load(new FileInputStream(
					new File(System.getProperty("user.dir") + "\\src\\test\\resources\\Config.properties")));
		} catch (Exception e) {

		}
		aresDashboard = new AresDashboard();
		
		if(conf.getProperty("postResultsToARESdashboard").equals("true")){
			sRunID = aresDashboard.createRunIDDetails("started");
		}
		
		// Suite name is to store data to dashboard
		suiteName = iSuite.getName();
	}

}
