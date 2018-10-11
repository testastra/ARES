package ZenQ_Dashboard;

import java.io.File;
import java.io.FileInputStream;
import java.util.Properties;

/**
 * Gets dashboard properties from configuration file
 *
 */
public class DashboardResources {

	static Properties conf;

	/**
	 * loads configuration
	 */
	static {
		conf = new Properties();
		try {
			conf.load(new FileInputStream(
					new File(System.getProperty("user.dir") + "\\src\\test\\resources\\Config.properties")));
		} catch (Exception e) {

		}
	}

	/**
	 * Gets create run id resource URL from properties
	 * 
	 * @return - returns URL as String object
	 */
	public static String getCreateRunIdURL() {
		return conf.getProperty("CreateRunIDUrl_ARES");
	}

	/**
	 * Gets add module data resource URL from properties
	 * 
	 * @return - returns URL as String object
	 */
	public static String getAddModuleDataURL() {
		return conf.getProperty("AddModuleDataUrl_ARES");
	}

	/**
	 * Gets test details resource URL
	 * 
	 * @return - returns URL as String object
	 */
	public static String getTestDetailsURL() {
		return conf.getProperty("TestDetailsUrl_ARES");
	}

	/**
	 * Gets project name
	 * 
	 * @return - project name will be returned as string object
	 */
	public static String getProjectName() {
		return conf.getProperty("ProjectName_ARES");
	}

	/**
	 * gets project key from properties file
	 * 
	 * @return Project key
	 */
	public static String getProjectKey() {
		return conf.getProperty("ProjectKey_ARES");
	}

	/**
	 * Gets user token
	 * 
	 * @return user token as string object
	 */
	public static String getProjectUserToken() {
		return conf.getProperty("UserToken_ARES");
	}

	/**
	 * Gets product name
	 * 
	 * @return product name as string
	 */
	public static String getProductName() {
		return conf.getProperty("ProductName_ARES");
	}

	/**
	 * Gets project User
	 * 
	 * @return project user as string object
	 */
	public static String getProjectUser() {
		return conf.getProperty("ProjectUser_ARES");
	}

	/**
	 * Gets test device
	 * 
	 * @return test device as string object
	 */
	public static String getTestDevice() {
		return conf.getProperty("TestDevice_ARES");
	}

	/**
	 * Gets workspace name
	 * 
	 * @return ws name
	 */
	public static String getWorkspaceName() {
		return conf.getProperty("Ws_Name_ARES");
	}

}
