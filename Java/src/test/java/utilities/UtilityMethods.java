/*************************************** PURPOSE **********************************

 - This class contains all Generic methods which are not related to specific application
 */
package utilities;

import org.testng.Assert;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class UtilityMethods {
	
	public static String modeOfExecution;

	public static String sMonthNamePostAdditionOfXdays;
	public static String sYearNamePostAdditionOfXdays;
	private static String fileSeperator = System.getProperty("file.separator");
	static Thread thread;
	

	/**
	 * Purpose - to get the system name
	 * 
	 * @return - String (returns system name)
	 */
	public static String machineName() {
		String sComputername = null;
		try {
			sComputername = InetAddress.getLocalHost().getHostName();
		} catch (UnknownHostException e) {
//			log.error("Unable to get the hostname..." + e.getCause());
		}
		return sComputername;
	}

	/**
     * Used to get date and time on specific format basis
     *
     * @param format_Needed input string value of format
     * @return fomated date and time string
     */
    public static String getDateTimeInSpecificFormat(String format_Needed) {
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern(format_Needed);
        LocalDateTime localDate = LocalDateTime.now();
        return dtf.format(localDate).toString();

    }
    
    /**
     * To get the entire exception stack trace from TestNG Listners
     *
     * @param errorMessage - Error message will be returned
     * @return the stack trace
     */
    public static String getStackTraceFromListners(Throwable errorMessage) {
        String trace = "";
        int i;
        StackTraceElement[] stackTrace = errorMessage.getStackTrace();
        for (i = 0; i < 5; i++) {
            trace = trace + "\n" + stackTrace[i];
        }
        return trace;
    }

	/**
	 * Used to convert string into base 64 encode format
	 * @param inputString  input string
	 * @return returned browser name string
	 */
	public static String getBrowserName(String inputString) {
		String browserName = "";
		try {
			if (inputString.contains("Chrome")) {
				browserName = "Chrome";
			} else if (inputString.contains("MSIE")) {
				browserName = "Internet Explorer";
			} else
				browserName = "Firefox";
		} catch (Exception e) {
			Assert.fail("Unable to find current browser"+ e.getStackTrace());
		}
		return browserName;
	}
}