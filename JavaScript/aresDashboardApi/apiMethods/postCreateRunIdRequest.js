let fs = require('fs');
let request = require("request");
let apiKeys = require("../apiData.js");
let runBody  = require("../javascriptDefaults/runBody.js");
let moduleBody = require('../javascriptDefaults/moduleBody.js');
let createRunIdApi = "http://testastra.com/graph/createrunid";

let postCreateRunIdRequest = {

    /*
    * Description: Api request to Create RunID
    * moduleBody.json is created with the create runID, ProjectName
    * ws_name is the workspace name
    * status is always started
    * projectName is the name of the project created in dashBoard
    * userToken is generated as the project is created in the dashboard
    * */

    createRunId: function (ws_name, status, projectName) {
        runBody.json.status = status;
        runBody.json.project_name = projectName;
        runBody.json.ws_name = ws_name;
        runBody.json.token = apiKeys.userToken;
        var JSONresult = JSON.stringify(runBody.json);
        var options = {
            url: createRunIdApi,
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSONresult,
            dataType: 'json',
        };

        function callback(error, response, body) {
            if (!error) {
                let res = JSON.parse(body);
                console.log(body);

                //Appended the data to moduleBody
                moduleBody.json.runId = res.data[0].runId;
                moduleBody.json.project_name = projectName;
                let mb = JSON.stringify(moduleBody.json);

                //Creates a new moduleBody.json with  runId
                fs.writeFile("./aresDashboardApi/jsonFiles/moduleBody.json", mb, 'utf8', function (err) {
                    if (err) {
                        return console.log(err);
                    }
                    console.log("module body created & saved!");
                });
            }
            else {
                console.log('Error happened: ' + error);
            }
            return body;
        }

        // Makes the API request
        request.post(options, callback);
    },
};
module.exports = postCreateRunIdRequest;