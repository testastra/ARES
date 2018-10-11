require("./jasmineListeners");

describe('TEST1:Protractor Demo App:5',function() {

    it("Should navigate to way2automation 1", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website - Multiform');
        });
    });

    it("Should navigate to way2automation 2", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website ');
        });
    });

    it("Should navigate to way2automation 3", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website ');
        });
    });

    it("Should navigate to way2automation 4", function () {
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website ');
        });
    });

    it("Should navigate to way2automation 5", function () {
        browser.sleep(2000);
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website ');
        });

    });

    it("Should navigate to way2automation 6", function () {
        browser.sleep(2000);
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website');
        });
    });
    it("Should navigate to way2automation 6", function () {
        browser.sleep(2000);
        browser.get("http://www.way2automation.com/angularjs-protractor/multiform/#/form/interests");
        browser.getTitle().then(function (title) {
            expect(title).toEqual('Protractor practice website');
        });
    });
});
