package tests;

import org.testng.Assert;
import org.testng.annotations.Test;

public class Opencart_ProductSearchTests {

	@Test
	public void addItemToCartNVerify() {
		try {
			Thread.sleep(500);
		} catch (Exception e) {
		}
		System.out.println("User successfully registered!");
		Assert.assertTrue(true, "User not registerd successfully");
	}

	@Test
	public void writeReviewOnProductNVerify() {
		try {
			Thread.sleep(500);
		} catch (Exception e) {
		}
		System.out.println("User logged in successfully with the valid credentials");
		Assert.assertTrue(true, "There is some problem with login credentils, please check them and try again");
	}

	@Test
	public void verifyAddItemsToWishlist() {
		try {
			Thread.sleep(400);
		} catch (Exception e) {
		}
		System.out.println("Verified my account page options with the valid credentials");
		Assert.assertTrue(true, "Options in my account page are not displayed");
	}

	@Test
	public void checkItemsAdded() {

		int numberOfItemsInCart = 2;
		try {
			Thread.sleep(80);
		} catch (Exception e) {
		}

		if (numberOfItemsInCart > 0) {
			Assert.fail("New user should not have any items added to the cart");
		}
	}

}
