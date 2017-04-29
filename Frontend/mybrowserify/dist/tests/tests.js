(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
/// <reference path="../typings/index.d.ts" />
"use strict";
var greet_ts_1 = require("../src/component/greet.ts");
var greet_ts_2 = require("../src/component/greet.ts");
describe("Unit test suite", function () {
    it("sayHello & showHello should exist", function () {
        expect(greet_ts_1.sayHello).toBeTruthy();
        expect(greet_ts_2.showHello).toBeTruthy();
    });
});
},{"../src/component/greet.ts":3}],2:[function(require,module,exports){
/// <reference path="../typings/index.d.ts" />
"use strict";
var greet_ts_1 = require("../src/component/greet.ts");
describe("Unit test suite", function () {
    it("showHello should work", function () {
        var id = "helloDiv";
        var div = document.createElement('div');
        div.id = id;
        div.style.display = 'none';
        document.body.appendChild(div);
        var content = "Unit Test";
        greet_ts_1.showHello(id, content);
        expect(div.innerHTML).toBeTruthy();
    });
});
},{"../src/component/greet.ts":3}],3:[function(require,module,exports){
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
},{}]},{},[1,2])
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyaWZ5L25vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCJzcGVjLzFzdC50ZXN0cy50cyIsInNwZWMvMm5kLnRlc3RzLnRzIiwic3JjL2NvbXBvbmVudC9ncmVldC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQ0FBLDhDQUE4Qzs7QUFFOUMseUJBQXVCLDJCQUEyQixDQUFDLENBQUE7QUFDbkQseUJBQXdCLDJCQUEyQixDQUFDLENBQUE7QUFFcEQsUUFBUSxDQUFDLGlCQUFpQixFQUFFO0lBQ3hCLEVBQUUsQ0FBQyxtQ0FBbUMsRUFBRTtRQUNwQyxNQUFNLENBQUMsbUJBQVEsQ0FBQyxDQUFDLFVBQVUsRUFBRSxDQUFDO1FBQzlCLE1BQU0sQ0FBQyxvQkFBUyxDQUFDLENBQUMsVUFBVSxFQUFFLENBQUM7SUFDbkMsQ0FBQyxDQUFDLENBQUM7QUFDUCxDQUFDLENBQUMsQ0FBQzs7QUNWSCw4Q0FBOEM7O0FBRzlDLHlCQUF3QiwyQkFBMkIsQ0FBQyxDQUFBO0FBRXBELFFBQVEsQ0FBQyxpQkFBaUIsRUFBRTtJQUN4QixFQUFFLENBQUMsdUJBQXVCLEVBQUU7UUFDeEIsSUFBSSxFQUFFLEdBQUcsVUFBVSxDQUFDO1FBQ3BCLElBQUksR0FBRyxHQUFHLFFBQVEsQ0FBQyxhQUFhLENBQUMsS0FBSyxDQUFDLENBQUM7UUFDeEMsR0FBRyxDQUFDLEVBQUUsR0FBRyxFQUFFLENBQUM7UUFDWixHQUFHLENBQUMsS0FBSyxDQUFDLE9BQU8sR0FBRyxNQUFNLENBQUM7UUFDM0IsUUFBUSxDQUFDLElBQUksQ0FBQyxXQUFXLENBQUMsR0FBRyxDQUFDLENBQUM7UUFDL0IsSUFBSSxPQUFPLEdBQUcsV0FBVyxDQUFDO1FBQzFCLG9CQUFTLENBQUMsRUFBRSxFQUFFLE9BQU8sQ0FBQyxDQUFDO1FBQ3ZCLE1BQU0sQ0FBQyxHQUFHLENBQUMsU0FBUyxDQUFDLENBQUMsVUFBVSxFQUFFLENBQUM7SUFDdkMsQ0FBQyxDQUFDLENBQUM7QUFDUCxDQUFDLENBQUMsQ0FBQzs7O0FDaEJILGtCQUF5QixJQUFZO0lBQ2pDLE1BQU0sQ0FBQyxnQkFBYyxJQUFNLENBQUM7QUFDaEMsQ0FBQztBQUZlLGdCQUFRLFdBRXZCLENBQUE7QUFFRCxtQkFBMEIsT0FBZSxFQUFFLElBQVk7SUFDbkQsSUFBSSxHQUFHLEdBQUcsUUFBUSxDQUFDLGNBQWMsQ0FBQyxPQUFPLENBQUMsQ0FBQztJQUMzQyxHQUFHLENBQUMsU0FBUyxHQUFHLFFBQVEsQ0FBQyxJQUFJLENBQUMsQ0FBQztBQUNuQyxDQUFDO0FBSGUsaUJBQVMsWUFHeEIsQ0FBQSIsImZpbGUiOiJnZW5lcmF0ZWQuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlc0NvbnRlbnQiOlsiKGZ1bmN0aW9uIGUodCxuLHIpe2Z1bmN0aW9uIHMobyx1KXtpZighbltvXSl7aWYoIXRbb10pe3ZhciBhPXR5cGVvZiByZXF1aXJlPT1cImZ1bmN0aW9uXCImJnJlcXVpcmU7aWYoIXUmJmEpcmV0dXJuIGEobywhMCk7aWYoaSlyZXR1cm4gaShvLCEwKTt2YXIgZj1uZXcgRXJyb3IoXCJDYW5ub3QgZmluZCBtb2R1bGUgJ1wiK28rXCInXCIpO3Rocm93IGYuY29kZT1cIk1PRFVMRV9OT1RfRk9VTkRcIixmfXZhciBsPW5bb109e2V4cG9ydHM6e319O3Rbb11bMF0uY2FsbChsLmV4cG9ydHMsZnVuY3Rpb24oZSl7dmFyIG49dFtvXVsxXVtlXTtyZXR1cm4gcyhuP246ZSl9LGwsbC5leHBvcnRzLGUsdCxuLHIpfXJldHVybiBuW29dLmV4cG9ydHN9dmFyIGk9dHlwZW9mIHJlcXVpcmU9PVwiZnVuY3Rpb25cIiYmcmVxdWlyZTtmb3IodmFyIG89MDtvPHIubGVuZ3RoO28rKylzKHJbb10pO3JldHVybiBzfSkiLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vdHlwaW5ncy9pbmRleC5kLnRzXCIgLz5cclxuXHJcbmltcG9ydCB7c2F5SGVsbG99IGZyb20gXCIuLi9zcmMvY29tcG9uZW50L2dyZWV0LnRzXCI7XHJcbmltcG9ydCB7c2hvd0hlbGxvfSBmcm9tIFwiLi4vc3JjL2NvbXBvbmVudC9ncmVldC50c1wiO1xyXG5cclxuZGVzY3JpYmUoXCJVbml0IHRlc3Qgc3VpdGVcIiwgZnVuY3Rpb24oKXtcclxuICAgIGl0KFwic2F5SGVsbG8gJiBzaG93SGVsbG8gc2hvdWxkIGV4aXN0XCIsIGZ1bmN0aW9uKCl7XHJcbiAgICAgICAgZXhwZWN0KHNheUhlbGxvKS50b0JlVHJ1dGh5KCk7XHJcbiAgICAgICAgZXhwZWN0KHNob3dIZWxsbykudG9CZVRydXRoeSgpO1xyXG4gICAgfSk7XHJcbn0pOyIsIi8vLyA8cmVmZXJlbmNlIHBhdGg9XCIuLi90eXBpbmdzL2luZGV4LmQudHNcIiAvPlxyXG5cclxuaW1wb3J0IHtzYXlIZWxsb30gZnJvbSBcIi4uL3NyYy9jb21wb25lbnQvZ3JlZXQudHNcIjtcclxuaW1wb3J0IHtzaG93SGVsbG99IGZyb20gXCIuLi9zcmMvY29tcG9uZW50L2dyZWV0LnRzXCI7XHJcblxyXG5kZXNjcmliZShcIlVuaXQgdGVzdCBzdWl0ZVwiLCBmdW5jdGlvbigpe1xyXG4gICAgaXQoXCJzaG93SGVsbG8gc2hvdWxkIHdvcmtcIiwgZnVuY3Rpb24oKXtcclxuICAgICAgICBsZXQgaWQgPSBcImhlbGxvRGl2XCI7XHJcbiAgICAgICAgbGV0IGRpdiA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ2RpdicpO1xyXG4gICAgICAgIGRpdi5pZCA9IGlkO1xyXG4gICAgICAgIGRpdi5zdHlsZS5kaXNwbGF5ID0gJ25vbmUnO1xyXG4gICAgICAgIGRvY3VtZW50LmJvZHkuYXBwZW5kQ2hpbGQoZGl2KTtcclxuICAgICAgICBsZXQgY29udGVudCA9IFwiVW5pdCBUZXN0XCI7XHJcbiAgICAgICAgc2hvd0hlbGxvKGlkLCBjb250ZW50KTtcclxuICAgICAgICBleHBlY3QoZGl2LmlubmVySFRNTCkudG9CZVRydXRoeSgpO1xyXG4gICAgfSk7XHJcbn0pOyIsImV4cG9ydCBmdW5jdGlvbiBzYXlIZWxsbyhuYW1lOiBzdHJpbmcpIHtcclxuICAgIHJldHVybiBgSGVsbG8gZnJvbSAke25hbWV9YDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNob3dIZWxsbyhkaXZOYW1lOiBzdHJpbmcsIG5hbWU6IHN0cmluZykge1xyXG4gICAgbGV0IGVsdCA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKGRpdk5hbWUpO1xyXG4gICAgZWx0LmlubmVyVGV4dCA9IHNheUhlbGxvKG5hbWUpO1xyXG59Il19
