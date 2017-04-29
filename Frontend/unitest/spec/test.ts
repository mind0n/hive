/// <reference path="../typings/index.d.ts" />
/// <reference path="../src/src.ts" />
import ajax from ajax;

describe("Unit test demo", function(){
    it("jQuery should exist", function(){
        expect($).toBeTruthy();
    });
    it("ajax util should exist", function(){
        expect(ajax).toBeTruthy();
    });    
});