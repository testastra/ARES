
require("./jasmineListeners");


describe('ESIGN:Protractor Demo App:6', function() {

    it('Should navigate to github 1', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(browser.getTitle()).toEqual('Super Calculator');
    });
    it('Should navigate to github 2', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(true).toBe(true);
    });
    it('Should navigate to github 3', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(browser.getTitle()).toEqual('Super Calculator');
    });
    it('Should navigate to github 4', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(browser.getTitle()).toEqual('Super Calculator');
    });
    it('Should navigate to github 5', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(browser.getTitle()).toEqual('Super Calculator');
    });
    it('Should have a title', function() {
        browser.get('http://juliemr.github.io/protractor-demo/');
        expect(browser.getTitle()).toEqual('Super Calculator');
    });
 });