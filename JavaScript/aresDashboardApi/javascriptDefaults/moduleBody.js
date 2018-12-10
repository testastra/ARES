
var apidata = require("../apiData");
exports.json= {
    "token": apidata.userToken,
    "runId": "-",
    "ws_name": apidata.ws_name,
    "project_name": apidata.projectName,
    "module_name": "-",
    "starttime": "-",
    "totaltests": '-',
    "status": apidata.initialRunStatus
};
