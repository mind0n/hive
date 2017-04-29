/// <reference path="../typings/index.d.ts" />

import {sayHello} from "../src/component/greet.ts";
import {showHello} from "../src/component/greet.ts";

import {assert} from "chai";

describe("Unit test suite 2", function(){
    it("2: sayHello & showHello should exist", function(){
        assert.isFunction(sayHello);
        assert.isFunction(showHello);
    });
    // it("showHello should work", function(){
    //     let id = "helloDiv";
    //     let div = document.getElementById(id);
    //     if (!div){
    //         div = document.createElement('div');
    //         div.id = id;
    //         div.style.display = 'none';
    //         document.body.appendChild(div);
    //     }        
    //     let content = "Unit Test";
    //     showHello(id, content);
    //     assert.equal(div.innerHTML, "Hello from Unit Test");
    // });
});