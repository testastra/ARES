let q = require('q');
let fs  = require("fs");
let request = require("request");
let apiKeys = require("../apiData.js");
let addModuleDataApi ="http://testastra.com/graph/addmoduledata";
let testBody = require("../javascriptDefaults/testBody.js");
let moduleBody = require("../jsonFiles/moduleBody");

let postCreateModuleRequest= {

    /*
   * Description: Api request to Create module
   * testBody.json is created with the create module_name, runId reading values from moduleBody.json
   * runId created from the createRunId method
   * module_name is the name of the module
   * project_name is the name of the sample project created in the dashBoard
   * moduleStatus indicates the starting/ending of the test execution
   * userToken is generated as the project is created
   * */
    postModuleDetails: function createModule(ws_name, project_name, moduleName, startTime, totalTests, moduleStatus) {

        // Reads moduleBody.json
        fs.readFile("./aresDashboardApi/jsonFiles/moduleBody.json", function(err, data){
            let res = data.toString();
            let j = JSON.parse(res);

            // Appended the data to testBody
            testBody.json.graphData.runId = j.runId;
            testBody.json.graphData.moduleName = moduleName;
            let tb = JSON.stringify(testBody.json);

            // Creates a new testBody.json with updated moduleName and runId
            fs.writeFile("./aresDashboardApi/jsonFiles/testBody.json", tb, 'utf8', function (err) {
                if (err) {
                    return console.log(err);
                }
                console.log("Test body created & saved!");
            });

            moduleBody.starttime = startTime;
            moduleBody.module_name = moduleName;
            moduleBody.totaltests = totalTests;
            moduleBody.status = moduleStatus;
            moduleBody.runId = j.runId;
            moduleBody.token = apiKeys.userToken;
            moduleBody.ws_name = ws_name;
            moduleBody.project_name = j.project_name;
            let ModuleJsonBody = JSON.stringify(moduleBody);

            let options = {
                url: addModuleDataApi,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: ModuleJsonBody,
                dataType: 'json',
            };

            function callback(error, response, body) {
                if (!error) {
                }
                else {
                    console.log('Error happened adding module data: ' + error);
                }
            }

            // Makes the API request
            return q.fcall(function () {
                request.post(options, callback);
            }).delay(1000);
        });

    },
};

module.exports = postCreateModuleRequest;