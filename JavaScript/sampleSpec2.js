
require("./jasmineListeners");

describe('DASHBOARD:Protractor Demo App:6',function() {

    it("Should navigate to github 1", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });
    it("Should navigate to github 2", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });
    it("Should navigate to github 3", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });

    it("Should navigate to github 4", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });
    it("Should navigate to github 5", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });
    it("Should navigate to github 6", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice webste - Multiform');
        });
    });
    it("Should navigate to github 7", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice webste - Multiform');
        });
    });
});
