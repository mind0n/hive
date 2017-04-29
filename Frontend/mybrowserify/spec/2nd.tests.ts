/// <reference path="../typings/index.d.ts" />

import {sayHello} from "../src/component/greet.ts";
import {showHello} from "../src/component/greet.ts";

describe("Unit test suite", function(){
    it("showHello should work", function(){
        let id = "helloDiv";
        let div = document.createElement('div');
        div.id = id;
        div.style.display = 'none';
        document.body.appendChild(div);
        let content = "Unit Test";
        showHello(id, content);
        expect(div.innerHTML).toBeTruthy();
    });
});