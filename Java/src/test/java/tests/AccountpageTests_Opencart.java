package tests;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxOptions;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.testng.Assert;
import org.testng.ITestContext;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import utilities.UtilityMethods;

/**
 * Verifies My Account page options 
 * 
 */
public class AccountpageTests_Opencart {

	private WebDriver driver;
	
	static String executionMode = "linear";
	
	String itemName = "Nikon";
	
	/**
	 * Intialises driver object based on the browser type
	 * @param browserType - parameterized from testng.xml file
	 * @param context - ItestContext object
	 */
	@Parameters({"browserType"})
	@BeforeMethod
	public void launchURL(String browserType, ITestContext context){
		
		if(browserType.equals("firefox")){
			FirefoxOptions firefoxOptions = new FirefoxOptions();
			firefoxOptions.addPreference("browserName", browserType);
			firefoxOptions.addPreference("security.sandbox.content.level", 5);
			System.setProperty("webdriver.gecko.driver", System.getProperty("user.dir")+"\\src\\test\\resources\\Drivers\\geckodriver.exe");
			driver = new FirefoxDriver(firefoxOptions);
		}else{
			System.setProperty("webdriver.chrome.driver", System.getProperty("user.dir")+"\\src\\test\\resources\\Drivers\\chromedriver.exe");
			DesiredCapabilities capabilites = DesiredCapabilities.chrome();
			capabilites.setBrowserName("chrome");
			driver = new ChromeDriver();
		}
		
		context.setAttribute("driver", driver);
		UtilityMethods.modeOfExecution = executionMode;
		driver.manage().deleteAllCookies();
		driver.get("http://192.168.2.151/opencart");
		driver.manage().window().maximize();
		
	}
	
	/**
	 * Login to Opencart, selects and verifies my account dropdown 
	 * 
	 * @throws Exception - object will be thrown if exception happens during execution
	 */
	@Test(enabled=true)
	public void loginToOpencartAndVerifyMyAccountOptions() throws Exception{
		
//		Select my account dropdown
		WebElement myAccountDropdown = driver.findElement(By.xpath("//li[@class='dropdown']/a[@title='My Account']"));
		myAccountDropdown.click();
	
//		Selects login link
		WebElement loginLink = driver.findElement(By.xpath("//ul[contains(@class, 'dropdown-menu')]//a[text()='Login']"));
		loginLink.click();
		String loginPageTitle = driver.getTitle();
		
//		Verifies login page title
		Assert.assertTrue(loginPageTitle.equals("Account Login"), "User has not navigated to Opencart login page");
		
//		Enters email address value as tester@gmail.com
		WebElement emailAddressTextbox = driver.findElement(By.id("input-email"));
		emailAddressTextbox.sendKeys("tester@gmail.com");
		
//		Enters password value as tester@123		
		WebElement passwordTextbox = driver.findElement(By.id("input-password"));
		passwordTextbox.sendKeys("tester@123");
		
//		Selects login button
		WebElement loginBtn = driver.findElement(By.cssSelector("input[value='Login']"));
		loginBtn.click();
		
		String myAccountPageTitle = driver.getTitle();
//		Verifies My Account page title
		Assert.assertTrue(myAccountPageTitle.equals("My Account"), "User is unable to login to Opencart, check your login credentials once");
		
	}
	
	
	/**
	 * Verifies Reward points page from  register account page
	 * @throws Exception
	 */
	@Test(enabled=true)
	public void verifyRewardPointsFromRegisterAccountPage() throws Exception{
		
//		Select my account dropdown
		WebElement myAccountDropdown = driver.findElement(By.xpath("//li[@class='dropdown']/a[@title='My Account']"));
		myAccountDropdown.click();
		String registerAccountPageTitle = driver.getTitle();
//		Verifies register account page title
		Assert.assertTrue(registerAccountPageTitle.equals("Register Account"), "Register account page is not navigated from my account dropdown");
//		Selects reward points link
		WebElement rewardPointsLinkt = driver.findElement(By.xpath("//div[@class='list-group']//a[text()='Reward Points']"));
		rewardPointsLinkt.click();
//		Enters email address value as tester@gmail.com
		WebElement emailAddressTextbox = driver.findElement(By.id("input-email"));
		emailAddressTextbox.sendKeys("tester@gmail.com");
//		Enters password value as tester@123	
		WebElement passwordTextbox = driver.findElement(By.id("input-password"));
		passwordTextbox.sendKeys("tester@123");
//		Selects login button
		WebElement loginBtn = driver.findElement(By.cssSelector("input[value='Login']"));
		loginBtn.click();
		String yourRewardPointsPageTitle = driver.getTitle();
//		Verifies Your Reward Points page title
		Assert.assertTrue(yourRewardPointsPageTitle.equals("Your Reward Points"), "Reward points page is not opened");
		System.out.println("Verified Reward points page from Register account page");
	}
	
	/**
	 * Closes browser
	 */
	@AfterMethod
	public void closeBrowser(){
		driver.close();
	}
}
