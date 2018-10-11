package tests;

import java.util.List;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxOptions;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.testng.Assert;
import org.testng.ITestContext;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;
import utilities.UtilityMethods;

/**
 * Searches for a product and adds item to cart and verifies
 * 
 */
public class ProductSearchTests_Opencart{
	
	
	private WebDriver driver;
	
	static String executionMode = "linear";
	
	String itemName = "Nikon";
	
	/**
	 * Intialises driver object based on the browser type
	 * @param browserType - Parameterised from testng.xml
	 * @param context - ITestContext
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
	 * Adds item to cart, verifies item in cart and removes from cart
	 * 
	 * @throws Exception - throws Exception object if exception happens during execution
	 */
	@Test(enabled=true)
	public void addItemToCartAndVerify() throws Exception{
		
//		Selects Cameras tab in menu
		driver.findElement(By.xpath("//li/a[text()='Cameras']")).click();
//		Selects Nikon camera in results list
		driver.findElement(By.xpath("//div[contains(@class, 'product-layout')]//h4//a[contains(text(), 'Nikon')]")).click();
		WebElement productCostElement = driver.findElement(By.xpath("//div[@id='content']//div[@class='col-sm-4']//h2"));
//		Clicks on add to cart
		WebElement addToCartEle = driver.findElement(By.xpath("//div[@id='content']//button[@id='button-cart']"));
		addToCartEle.click();
//		Verifies Item added to cart alert
		WebElement itemAddedSuccessfullyAlert = driver.findElement(By.xpath("//div[contains(@class, 'alert-success')]"));
		Assert.assertTrue(itemAddedSuccessfullyAlert.isDisplayed(), "Item is not added to cart upon click on add to cart button");
//		Selects shopping cart link on item added alert
		WebElement shoppingCartLink = driver.findElement(By.xpath("//div[contains(@class, 'alert-success')]//a[contains(@href, 'route=checkout/cart')]"));
		shoppingCartLink.click();
//		Clicks on remove icon against to item name which is added to cart 
		WebElement addedItemRemovebtn = driver.findElement(By.xpath("(//div[@id='content']//table[contains(@class, 'table-bordered')])[1]//td//a[contains(text(), '"+itemName+"')]/parent::td//following::button[@data-original-title='Remove']"));
		addedItemRemovebtn.click();
				
	}
	
	
	/**
	 * Writes review on product and verifies error messages 
	 * @throws Exception - throws Exception object if exception happens during execution
	 */
	@Test(enabled=true)
	public void writeReviewOnProductAndVerifyErrorMessage() throws Exception{
		
//		Mousehovers on Laptop and notebooks ele and selects Show all laptops and notebooks
		WebElement laptopsAndNotebooksEle = driver.findElement(By.xpath("//li[@class='dropdown']//a[text()='Laptops & Notebooks']"));
		Actions action = new Actions(driver);
		action.moveToElement(laptopsAndNotebooksEle).click(driver.findElement(By.xpath("//a[text()='Show All Laptops & Notebooks']"))).build().perform();
//		Selects HP laptop from available lists
		driver.findElement(By.xpath("//div[contains(@class, 'product-layout')]//h4//a[contains(text(), 'HP LP3065')]")).click();
//		Selects Reviews tab
		WebElement reviewsTab = driver.findElement(By.xpath("//ul[contains(@class, 'nav-tabs')]//a[contains(text(), 'Reviews')]"));
		reviewsTab.click();
//		Enters value in your name textbox in review form
		WebElement yourNameTextboxInReviewform = driver.findElement(By.id("input-name"));
		yourNameTextboxInReviewform.sendKeys("testing");
//		Selects review continue button
		WebElement reviewContinueBtn = driver.findElement(By.id("button-review"));
		reviewContinueBtn.click();
//		Verifies review alert error messsage
		WebElement reviewAlertErrorMssg = driver.findElement(By.xpath("//div[contains(@class, 'alert-danger')]"));
		Assert.assertTrue(reviewAlertErrorMssg.isDisplayed(), "Review form error message is not displayed if user is continuing without giving review rating");
		
	}
	
	
	/**
	 * Verifies items added to whishlist from homepage footer section
	 * @throws Exception
	 */
	@Test(enabled=true)
	public void verifyItemsAddedToWhishlistFromHomepageFooterSection() throws Exception{
		
//		Clicks on wishlist list
		WebElement wishlistLink = driver.findElement(By.xpath("//li//a[text()='Wish List' and contains(@href, 'account/wishlist')]"));
		wishlistLink.click();
//		Enters email in email field
		WebElement emailAddressTextbox = driver.findElement(By.id("input-email"));
		emailAddressTextbox.sendKeys("tester@gmail.com");
//		Enters password in email field
		WebElement passwordTextbox = driver.findElement(By.id("input-password"));
		passwordTextbox.sendKeys("tester@123");
//		Clicks on login link
		WebElement loginBtn = driver.findElement(By.cssSelector("input[value='Login']"));
		loginBtn.click();
//		Gets the list of items in whishlist
		List<WebElement> itemnamesInWishlist = driver.findElements(By.xpath("(//table[contains(@class, 'table table-bordered')]/tbody/tr//td)[2]/a"));
		
		for(WebElement itemname : itemnamesInWishlist){
			System.out.println("Item name : "+itemname);
		}
		
	}
	
	@AfterMethod
	public void closeBrowser(){
		driver.close();
	}
}
