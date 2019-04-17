# How to use ARES Dashboard in Robotframework

In this tutorial will discuss about usage of ARES Dashboard in Robotframework

> Prerequisite: 
> - Basic knowledge on robotframework
> - Python requests library installed (`pip install requests`) to make REST API calls
> - Created project in ARES Dashboard ( creating project in ARES is explained in [01_Introduction.md](/Tutorials/01_Introduction.md) )

__SETUP PROJECT__

In this section will discuss about set-up project for ARES dashboard usage

 - Step 1: Copy _USER Token_ and _Project ID_
    > Explained in _GET USER TOKEN AND PROJECT ID_ section of _Introduction_

 - Step 2: Download [Sample Example](https://github.com/testastra/ARES/releases/download/v1.0-rf/rf.zip) 

 - Step 3: Copy following files into project (which contains code to make REST calls and listener for test status):

    > - Copy _Config.py_

    > - Copy _AresListener.py_

 - Step 4: Update _Config.py_ with project info (copied in _Step 1_ )
    > Project Name, Project Key, User Token etc.,

 - Step 5: Write custom robot test cases
   > Our customized _AresListener.py_(files copied from Sample ARES scripts) will make respective REST API call's for ARES Dashboard

 - Step 6: Execute test cases
	> robot --listener AresListener.py -V config.py mysuite.robot

 - Step 7: Go to *ARES dashboard* to view live execution status of test case

Note: For more usage of REST API calls. Please refer our wiki [link](https://github.com/testastra/ARES/wiki)

Observation: Current _AresListener_ doesn't support parallel execution. Our team is working on it, if you have working example you can share with us.