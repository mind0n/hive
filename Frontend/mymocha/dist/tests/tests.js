(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
"use strict";
function sayHello(name) {
    return "Hello from " + name;
}
exports.sayHello = sayHello;
function showHello(divName, name) {
    var elt = document.getElementById(divName);
    elt.innerText = sayHello(name);
}
exports.showHello = showHello;
},{}],2:[function(require,module,exports){
"use strict";
/// <reference path="../typings/index.d.ts" />
/// <reference path="../src/main.ts" />
var greet_ts_1 = require("../src/component/greet.ts");
var greet_ts_2 = require("../src/component/greet.ts");
describe("Unit test suite", function () {
    it("sayHello & showHello should exist", function () {
        expect(greet_ts_1.sayHello).toBeTruthy();
        expect(greet_ts_2.showHello).toBeTruthy();
    });
    it("showHello should work", function () {
        var id = "helloDiv";
        var div = document.getElementById(id);
        if (!div) {
            div = document.createElement('div');
            div.id = id;
            div.style.display = 'none';
            document.body.appendChild(div);
        }
        var content = "Unit Test";
        greet_ts_2.showHello(id, content);
        expect(div.innerHTML).toBeTruthy();
    });
});
},{"../src/component/greet.ts":1}]},{},[2])
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCJzcmMvY29tcG9uZW50L2dyZWV0LnRzIiwic3BlYy90ZXN0cy50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTs7QUNBQSxrQkFBeUIsSUFBWTtJQUNqQyxNQUFNLENBQUMsZ0JBQWMsSUFBTSxDQUFDO0FBQ2hDLENBQUM7QUFGZSxnQkFBUSxXQUV2QixDQUFBO0FBRUQsbUJBQTBCLE9BQWUsRUFBRSxJQUFZO0lBQ25ELElBQUksR0FBRyxHQUFHLFFBQVEsQ0FBQyxjQUFjLENBQUMsT0FBTyxDQUFDLENBQUM7SUFDM0MsR0FBRyxDQUFDLFNBQVMsR0FBRyxRQUFRLENBQUMsSUFBSSxDQUFDLENBQUM7QUFDbkMsQ0FBQztBQUhlLGlCQUFTLFlBR3hCLENBQUE7OztBQ1BELDhDQUE4QztBQUM5Qyx1Q0FBdUM7QUFDdkMseUJBQXVCLDJCQUEyQixDQUFDLENBQUE7QUFDbkQseUJBQXdCLDJCQUEyQixDQUFDLENBQUE7QUFFcEQsUUFBUSxDQUFDLGlCQUFpQixFQUFFO0lBQ3hCLEVBQUUsQ0FBQyxtQ0FBbUMsRUFBRTtRQUNwQyxNQUFNLENBQUMsbUJBQVEsQ0FBQyxDQUFDLFVBQVUsRUFBRSxDQUFDO1FBQzlCLE1BQU0sQ0FBQyxvQkFBUyxDQUFDLENBQUMsVUFBVSxFQUFFLENBQUM7SUFDbkMsQ0FBQyxDQUFDLENBQUM7SUFDSCxFQUFFLENBQUMsdUJBQXVCLEVBQUU7UUFDeEIsSUFBSSxFQUFFLEdBQUcsVUFBVSxDQUFDO1FBQ3BCLElBQUksR0FBRyxHQUFHLFFBQVEsQ0FBQyxjQUFjLENBQUMsRUFBRSxDQUFDLENBQUM7UUFDdEMsRUFBRSxDQUFDLENBQUMsQ0FBQyxHQUFHLENBQUMsQ0FBQSxDQUFDO1lBQ04sR0FBRyxHQUFHLFFBQVEsQ0FBQyxhQUFhLENBQUMsS0FBSyxDQUFDLENBQUM7WUFDcEMsR0FBRyxDQUFDLEVBQUUsR0FBRyxFQUFFLENBQUM7WUFDWixHQUFHLENBQUMsS0FBSyxDQUFDLE9BQU8sR0FBRyxNQUFNLENBQUM7WUFDM0IsUUFBUSxDQUFDLElBQUksQ0FBQyxXQUFXLENBQUMsR0FBRyxDQUFDLENBQUM7UUFDbkMsQ0FBQztRQUNELElBQUksT0FBTyxHQUFHLFdBQVcsQ0FBQztRQUMxQixvQkFBUyxDQUFDLEVBQUUsRUFBRSxPQUFPLENBQUMsQ0FBQztRQUN2QixNQUFNLENBQUMsR0FBRyxDQUFDLFNBQVMsQ0FBQyxDQUFDLFVBQVUsRUFBRSxDQUFDO0lBQ3ZDLENBQUMsQ0FBQyxDQUFDO0FBQ1AsQ0FBQyxDQUFDLENBQUMiLCJmaWxlIjoiZ2VuZXJhdGVkLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXNDb250ZW50IjpbIihmdW5jdGlvbiBlKHQsbixyKXtmdW5jdGlvbiBzKG8sdSl7aWYoIW5bb10pe2lmKCF0W29dKXt2YXIgYT10eXBlb2YgcmVxdWlyZT09XCJmdW5jdGlvblwiJiZyZXF1aXJlO2lmKCF1JiZhKXJldHVybiBhKG8sITApO2lmKGkpcmV0dXJuIGkobywhMCk7dmFyIGY9bmV3IEVycm9yKFwiQ2Fubm90IGZpbmQgbW9kdWxlICdcIitvK1wiJ1wiKTt0aHJvdyBmLmNvZGU9XCJNT0RVTEVfTk9UX0ZPVU5EXCIsZn12YXIgbD1uW29dPXtleHBvcnRzOnt9fTt0W29dWzBdLmNhbGwobC5leHBvcnRzLGZ1bmN0aW9uKGUpe3ZhciBuPXRbb11bMV1bZV07cmV0dXJuIHMobj9uOmUpfSxsLGwuZXhwb3J0cyxlLHQsbixyKX1yZXR1cm4gbltvXS5leHBvcnRzfXZhciBpPXR5cGVvZiByZXF1aXJlPT1cImZ1bmN0aW9uXCImJnJlcXVpcmU7Zm9yKHZhciBvPTA7bzxyLmxlbmd0aDtvKyspcyhyW29dKTtyZXR1cm4gc30pIiwiZXhwb3J0IGZ1bmN0aW9uIHNheUhlbGxvKG5hbWU6IHN0cmluZykge1xyXG4gICAgcmV0dXJuIGBIZWxsbyBmcm9tICR7bmFtZX1gO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gc2hvd0hlbGxvKGRpdk5hbWU6IHN0cmluZywgbmFtZTogc3RyaW5nKSB7XHJcbiAgICBsZXQgZWx0ID0gZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoZGl2TmFtZSk7XHJcbiAgICBlbHQuaW5uZXJUZXh0ID0gc2F5SGVsbG8obmFtZSk7XHJcbn0iLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vdHlwaW5ncy9pbmRleC5kLnRzXCIgLz5cclxuLy8vIDxyZWZlcmVuY2UgcGF0aD1cIi4uL3NyYy9tYWluLnRzXCIgLz5cclxuaW1wb3J0IHtzYXlIZWxsb30gZnJvbSBcIi4uL3NyYy9jb21wb25lbnQvZ3JlZXQudHNcIjtcclxuaW1wb3J0IHtzaG93SGVsbG99IGZyb20gXCIuLi9zcmMvY29tcG9uZW50L2dyZWV0LnRzXCI7XHJcblxyXG5kZXNjcmliZShcIlVuaXQgdGVzdCBzdWl0ZVwiLCBmdW5jdGlvbigpe1xyXG4gICAgaXQoXCJzYXlIZWxsbyAmIHNob3dIZWxsbyBzaG91bGQgZXhpc3RcIiwgZnVuY3Rpb24oKXtcclxuICAgICAgICBleHBlY3Qoc2F5SGVsbG8pLnRvQmVUcnV0aHkoKTtcclxuICAgICAgICBleHBlY3Qoc2hvd0hlbGxvKS50b0JlVHJ1dGh5KCk7XHJcbiAgICB9KTtcclxuICAgIGl0KFwic2hvd0hlbGxvIHNob3VsZCB3b3JrXCIsIGZ1bmN0aW9uKCl7XHJcbiAgICAgICAgbGV0IGlkID0gXCJoZWxsb0RpdlwiO1xyXG4gICAgICAgIGxldCBkaXYgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChpZCk7XHJcbiAgICAgICAgaWYgKCFkaXYpe1xyXG4gICAgICAgICAgICBkaXYgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KCdkaXYnKTtcclxuICAgICAgICAgICAgZGl2LmlkID0gaWQ7XHJcbiAgICAgICAgICAgIGRpdi5zdHlsZS5kaXNwbGF5ID0gJ25vbmUnO1xyXG4gICAgICAgICAgICBkb2N1bWVudC5ib2R5LmFwcGVuZENoaWxkKGRpdik7XHJcbiAgICAgICAgfSAgICAgICAgXHJcbiAgICAgICAgbGV0IGNvbnRlbnQgPSBcIlVuaXQgVGVzdFwiO1xyXG4gICAgICAgIHNob3dIZWxsbyhpZCwgY29udGVudCk7XHJcbiAgICAgICAgZXhwZWN0KGRpdi5pbm5lckhUTUwpLnRvQmVUcnV0aHkoKTtcclxuICAgIH0pO1xyXG59KTsiXX0=
