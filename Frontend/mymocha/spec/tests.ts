/// <reference path="../typings/index.d.ts" />
/// <reference path="../src/main.ts" />
import {sayHello} from "../src/component/greet.ts";
import {showHello} from "../src/component/greet.ts";

import * as chai from "chai";
let assert = chai.assert;

function testfunc(n:number):boolean{
    if (n>0){
        return true;
    }else{
        return false;
    }
}

describe('Unit test demo', function() {
  describe('Coverage demo', function() {
    it('greater than 0 should be true', function() {
        assert.isTrue(testfunc(10));
        // assert.equal(-1, [1,2,3].indexOf(5));
        // assert.equal(-1, [1,2,3].indexOf(0));
    });
  });
});

// describe("Unit test suite", function(){
//     it("sayHello & showHello should exist", function(){
//         expect(sayHello).toBeTruthy();
//         expect(showHello).toBeTruthy();
//     });
//     it("showHello should work", function(){
//         let id = "helloDiv";
//         let div = document.getElementById(id);
//         if (!div){
//             div = document.createElement('div');
//             div.id = id;
//             div.style.display = 'none';
//             document.body.appendChild(div);
//         }        
//         let content = "Unit Test";
//         showHello(id, content);
//         expect(div.innerHTML).toBeTruthy();
//     });
// });