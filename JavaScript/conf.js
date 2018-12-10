
let aresApi = require("./aresDashboardApi/apiMethods/postCreateRunIdRequest");
let apiData = require("./aresDashboardApi/apiData");
aresApi.createRunId(apiData.ws_name, apiData.initialRunStatus,apiData.projectName);

exports.config = {
    framework: 'jasmine2',
    directConnect: true,
    random:false,
    capabilities:
        {
            browserName: 'chrome',
            specs:['sampleSpec1.js', 'sampleSpec2.js', 'sampleSpec3.js']
        },
    jasmineNodeOpts:{
        defaultTimeoutInterval: 120000,
    },
    baseUrl: 'http://testastra.com/dashboard'

};