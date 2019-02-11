# Welcome to the ARES Dashboard

ARES is a web based portal, which provides automation results at one place. It can integrate with most popular automation tools like Selenium, Protractor etc.,

__OVERVIEW OF ARES__

 ![ARES DASHBOARD](https://i.ibb.co/tq99n8c/dashboard-overview.gif)

---
__HOW TO USE__

In addition to your test automation code, you need to call 3 Restful APIs at designated places in your test automation framework. These APIs would carry some information about your test automation such as number of tests to be executed, test name, test status, platform used, error logs etc to the dashboard server in JSON format, which would then represent the data in some graphs and charts. 

These APIs are authenticated through User token & Project ID (For more info go through [How does ARES work](https://github.com/testastra/ARES/wiki/2.-How-does-it-work%3F) wiki)

---
__HOW TO GET USER TOKEN AND PROJECT ID__

 - User need to create account in testastra to get USER token
 - Create Project to get Project ID

Visit [testastra.com](http://testastra.com/) to register and create an account.

---
**STEPS FOR ACCOUNT CREATION**

 - Step 1: Go to [testastra.com](http://testastra.com/)
 - Step 2: Select _ARES_ product and Click SignUp / Login
 
   ![SIGNUP OR LOGIN](https://i.ibb.co/Yb43SsJ/ares-navigation.gif)

    *For new users*: Account activation link will be sent to registered Email ID
    > Note: If you didn't receive email, Please check your _Junk folder_

 - Step 3: Access to Dashboard Home page

    > *New Users*: Navigate to activation link recieved in email to activate account and login

    > *Existing User*: ARES Dashboard will be loaded

   ![DASHBOARD](https://i.ibb.co/N3VVPwH/ares-project-dashboard.gif)

---
**CREATE PROJECT**
> Assumption: User logged into ARES dashboard

 - Step 1: Click on create project
 - Step 2: Fill required info like _Project Name_, _Tool_ and _Language_
 - Step 3: Click create

   ![CREATE PROJECT](https://i.ibb.co/Mf2j1h7/create-project.gif)

---
**GET USER TOKEN AND PROJECT ID**
> Assumption: User logged into ARES dashboard and created project

 - Step 1: Hover on elipse of project
 - Step 2: Click _Copy API Details_
 - Step 3: Click on _UserKey_ and _ProjectKey_ to copy content

   ![COPY API DETAILS](https://i.ibb.co/1QV5Zyc/get-user-token.gif)