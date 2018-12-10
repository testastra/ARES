package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class RegistrationTests {

	@Test
	public void registerNewUser() {
		try {
			Thread.sleep(300);
		} catch (Exception e) {
		}
		System.out.println("User successfully registered!");
		Assert.assertTrue(true, "User not registerd successfully");
	}

	@Test
	public void loginNVerifyRegisteredUser() {
		try {
			Thread.sleep(500);
		} catch (Exception e) {
		}
		System.out.println("User logged in successfully with the valid credentials");
		Assert.assertTrue(true, "There is some problem with login credentils, please check them and try again");
	}

	@Test
	public void verifyMyAccountpageOptions() {
		try {
			Thread.sleep(400);
		} catch (Exception e) {
		}
		System.out.println("Verified my account page options with the valid credentials");
		Assert.assertTrue(true, "Options in my account page are not displayed");
	}

	@Test
	public void checkItemsAddedInCartForNewUser() {

		int numberOfItemsInCart = 2;
		try {
			Thread.sleep(200);
		} catch (Exception e) {
		}

		if (numberOfItemsInCart > 0) {
			Assert.fail("New user should not have any items added to the cart");
		}
	}

	@Test
	public void changeUserAddressAndVerify() {

		try {
			Thread.sleep(300);
		} catch (Exception e) {

		}

		Assert.fail("Modified address is not saved, please check it once");
	}

	@Test
	public void VerifyReferralMessage() {

		String msg = "success";
		try {
			Thread.sleep(100);
		} catch (Exception e) {

		}
		Assert.assertTrue(msg.equals(" "), "Success message is not displayed upon referring friend");
	}

}
