let request = require("request");
let apiKeys = require("../apiData.js");
let addGraphDataApi = browser.baseUrl+"/graph/addgraphdata";
let fs = require("fs");
let jsonBody = require("../jsonFiles/testBody.json");

let postTestResultRequest = {

    /*
    * Description: Api request to post test results
    * Reads data from the JSON body and appends the method parameters to the key value pairs
    * runId created from the createRunId method
    * moduleName is the name of the module
    * productName is the name of the product
    * */
    postTestResults : function apiRequest(runId, status, testStartTime, testEndTime, testBrowser,  executionMode, failedStackTrace, errorMessage, testDevice, testOs, testDate, runBy, productName, moduleName, testSuite, testcaseTitle, testData , imageLink, videoLink, failType, testMachine) {

        // Reads testBody.json
        fs.readFile("./aresDashboardApi/jsonFiles/testBody.json", function(err, data) {
            var res = data.toString();
            var j = JSON.parse(res);

            jsonBody.graphData.runId = j.graphData.runId;
            jsonBody.graphData.testStartTime = testStartTime;
            jsonBody.graphData.testEndTime = testEndTime;
            jsonBody.graphData.testSuite = testSuite;
            jsonBody.graphData.testBrowser = testBrowser;
            jsonBody.graphData.productName = productName;
            jsonBody.graphData.moduleName = j.graphData.moduleName;
            jsonBody.graphData.testcaseTitle = testcaseTitle;
            jsonBody.graphData.testData = testData;
            jsonBody.graphData.testMachine = testMachine;
            jsonBody.graphData.imageLink = imageLink;
            jsonBody.graphData.videoLink = videoLink;
            jsonBody.graphData.testDevice = testDevice;
            jsonBody.graphData.testOs = testOs;
            jsonBody.graphData.testDate = testDate;
            jsonBody.graphData.runBy = runBy;
            jsonBody.graphData.failType = failType;
            jsonBody.graphData.executionMode = executionMode;

            if (status === "passed") {
                jsonBody.graphData.testStatus = "PASSED";
            }

            if (status === "failed") {
                jsonBody.graphData.testStatus = "FAILED";
                jsonBody.graphData.failStacktrace = failedStackTrace;
                jsonBody.graphData.errorMessage = errorMessage;
            }
            let tb = JSON.stringify(jsonBody);

            var options = {
                url: addGraphDataApi,
                headers: {
                    'Content-Type': 'application/json',
                    'Projectid': apiKeys.projectId,
                    'Usertoken': apiKeys.userToken
                },
                body: tb,
                dataType: 'json',
            };

            // Makes the API request
            return new Promise(function (resolve, reject) {
                request.post(options, function (error, response, body) {
                    if (error) {
                        console.log(error);
                    }
                })
            });
        });
    }
};
module.exports= postTestResultRequest;