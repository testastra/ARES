let aresApiModule = require("./aresDashboardApi/apiMethods/postCreateModuleRequest.js");
let aresTestResult=require("./aresDashboardApi/apiMethods/postTestResultRequest");
let moduleBody = require("./aresDashboardApi/jsonFiles/moduleBody.json");
let testBody = require("./aresDashboardApi/jsonFiles/testBody.json");
let BrowserName, moduleName, totalTest, description, startTime;

let myReporter = {

    getBrowserCapabilities : (function(){
        global.browser.getProcessedConfig().then(function () {
            browser.getCapabilities().then(function (capabilities) {
                BrowserName = capabilities.get('browserName');
            });
        });
    }()),

    jasmineStarted: function(suiteInfo) {
        beforeEach(async function(){
            await this._asyncFlow;
            this._asyncFlow = null;
        })
    },

    suiteStarted:   function(result) {

        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1;
        var day = dateObj.getUTCDate();
        var year = dateObj.getUTCFullYear();
        newdate = year + "-" + month + "-" + day;
        var time = dateObj.getHours()+":"+dateObj.getMinutes()+":"+dateObj.getSeconds();
        startTime = year + "-" + month + "-" + day+" "+time+"."+dateObj.getMilliseconds();
        var moduleStatus="started";
        description = result.description;
        moduleName = result.description.split(":")[0];
        totalTest = result.description.split(':')[2];
        aresApiModule.postModuleDetails(moduleBody.ws_name, moduleBody.project_name, moduleName, startTime, totalTest, moduleStatus);
    },

    specStarted:  function(result) {
        testBody.graphData.testcaseTitle = result.description;
        testBody.graphData.testStatus = "In Progress";
        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getUTCDate();
        var year = dateObj.getUTCFullYear();
        newdate = year + "-" + month + "-" + day;
        testBody.graphData.testDate = newdate;
        var time = dateObj.getHours()+":"+dateObj.getMinutes()+":"+dateObj.getSeconds();
        startTime = year + "-" + month + "-" + day+" "+time+"."+dateObj.getMilliseconds();
        testBody.graphData.testStartTime = startTime;
    },

    specDone: function(result) {
        if(result.failedExpectations.includes("Expected")){
            testBody.graphData.errorMessage = "Expectation failure";
            console.log(result);
        }
        aresTestResult.postTestResults(testBody.graphData.runId, result.status, startTime, testBody.graphData.testEndTime, BrowserName, testBody.graphData.executionMode, testBody.graphData.failStacktrace, testBody.graphData.errorMessage, testBody.graphData.testDevice, testBody.graphData.testOs, testBody.graphData.testDate, testBody.graphData.runBy, testBody.graphData.productName, testBody.graphData.moduleName,testBody.graphData.testSuite ,testBody.graphData.testcaseTitle, testBody.graphData.testData , testBody.graphData.imageLink, testBody.graphData.videoLink, testBody.graphData.failType, testBody.graphData.testMachine);
    },

    jasmineDone: function(result) {
        console.log('Finished suite');
    },
};

jasmine.getEnv().addReporter(myReporter);

module.exports = myReporter;

