var apidata = require("../apiData");

exports.json= {
    "token": apidata.userToken,
    "ws_name":apidata.ws_name ,
    "project_name": apidata.projectName,
    "status": apidata.initialRunStatus
};
