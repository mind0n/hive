/// <reference path="../typings/index.d.ts" />

import {sayHello} from "../src/component/greet.ts";
import {showHello} from "../src/component/greet.ts";

describe("Unit test suite", function(){
    it("sayHello & showHello should exist", function(){
        expect(sayHello).toBeTruthy();
        expect(showHello).toBeTruthy();
    });
});