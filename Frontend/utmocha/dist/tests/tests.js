(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
/// <reference path="../typings/index.d.ts" />
"use strict";
var greet_ts_1 = require("../src/component/greet.ts");
var greet_ts_2 = require("../src/component/greet.ts");
var chai_1 = require("chai");
describe("Unit test suite", function () {
    it("sayHello & showHello should exist", function () {
        chai_1.assert.isFunction(greet_ts_1.sayHello);
        chai_1.assert.isFunction(greet_ts_2.showHello);
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
},{"../src/component/greet.ts":3,"chai":"chai"}],2:[function(require,module,exports){
/// <reference path="../typings/index.d.ts" />
"use strict";
var greet_ts_1 = require("../src/component/greet.ts");
var greet_ts_2 = require("../src/component/greet.ts");
var chai_1 = require("chai");
describe("Unit test suite 2", function () {
    it("2: sayHello & showHello should exist", function () {
        chai_1.assert.isFunction(greet_ts_1.sayHello);
        chai_1.assert.isFunction(greet_ts_2.showHello);
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
},{"../src/component/greet.ts":3,"chai":"chai"}],3:[function(require,module,exports){
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
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyaWZ5L25vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCJzcGVjL3Rlc3RzLnRzIiwic3BlYy90ZXN0czIudHMiLCJzcmMvY29tcG9uZW50L2dyZWV0LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0FDQUEsOENBQThDOztBQUU5Qyx5QkFBdUIsMkJBQTJCLENBQUMsQ0FBQTtBQUNuRCx5QkFBd0IsMkJBQTJCLENBQUMsQ0FBQTtBQUVwRCxxQkFBcUIsTUFBTSxDQUFDLENBQUE7QUFFNUIsUUFBUSxDQUFDLGlCQUFpQixFQUFFO0lBQ3hCLEVBQUUsQ0FBQyxtQ0FBbUMsRUFBRTtRQUNwQyxhQUFNLENBQUMsVUFBVSxDQUFDLG1CQUFRLENBQUMsQ0FBQztRQUM1QixhQUFNLENBQUMsVUFBVSxDQUFDLG9CQUFTLENBQUMsQ0FBQztJQUNqQyxDQUFDLENBQUMsQ0FBQztJQUNILDBDQUEwQztJQUMxQywyQkFBMkI7SUFDM0IsNkNBQTZDO0lBQzdDLGlCQUFpQjtJQUNqQiwrQ0FBK0M7SUFDL0MsdUJBQXVCO0lBQ3ZCLHNDQUFzQztJQUN0QywwQ0FBMEM7SUFDMUMsZ0JBQWdCO0lBQ2hCLGlDQUFpQztJQUNqQyw4QkFBOEI7SUFDOUIsMkRBQTJEO0lBQzNELE1BQU07QUFDVixDQUFDLENBQUMsQ0FBQzs7QUN6QkgsOENBQThDOztBQUU5Qyx5QkFBdUIsMkJBQTJCLENBQUMsQ0FBQTtBQUNuRCx5QkFBd0IsMkJBQTJCLENBQUMsQ0FBQTtBQUVwRCxxQkFBcUIsTUFBTSxDQUFDLENBQUE7QUFFNUIsUUFBUSxDQUFDLG1CQUFtQixFQUFFO0lBQzFCLEVBQUUsQ0FBQyxzQ0FBc0MsRUFBRTtRQUN2QyxhQUFNLENBQUMsVUFBVSxDQUFDLG1CQUFRLENBQUMsQ0FBQztRQUM1QixhQUFNLENBQUMsVUFBVSxDQUFDLG9CQUFTLENBQUMsQ0FBQztJQUNqQyxDQUFDLENBQUMsQ0FBQztJQUNILDBDQUEwQztJQUMxQywyQkFBMkI7SUFDM0IsNkNBQTZDO0lBQzdDLGlCQUFpQjtJQUNqQiwrQ0FBK0M7SUFDL0MsdUJBQXVCO0lBQ3ZCLHNDQUFzQztJQUN0QywwQ0FBMEM7SUFDMUMsZ0JBQWdCO0lBQ2hCLGlDQUFpQztJQUNqQyw4QkFBOEI7SUFDOUIsMkRBQTJEO0lBQzNELE1BQU07QUFDVixDQUFDLENBQUMsQ0FBQzs7O0FDekJILGtCQUF5QixJQUFZO0lBQ2pDLE1BQU0sQ0FBQyxnQkFBYyxJQUFNLENBQUM7QUFDaEMsQ0FBQztBQUZlLGdCQUFRLFdBRXZCLENBQUE7QUFFRCxtQkFBMEIsT0FBZSxFQUFFLElBQVk7SUFDbkQsSUFBSSxHQUFHLEdBQUcsUUFBUSxDQUFDLGNBQWMsQ0FBQyxPQUFPLENBQUMsQ0FBQztJQUMzQyxHQUFHLENBQUMsU0FBUyxHQUFHLFFBQVEsQ0FBQyxJQUFJLENBQUMsQ0FBQztBQUNuQyxDQUFDO0FBSGUsaUJBQVMsWUFHeEIsQ0FBQSIsImZpbGUiOiJnZW5lcmF0ZWQuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlc0NvbnRlbnQiOlsiKGZ1bmN0aW9uIGUodCxuLHIpe2Z1bmN0aW9uIHMobyx1KXtpZighbltvXSl7aWYoIXRbb10pe3ZhciBhPXR5cGVvZiByZXF1aXJlPT1cImZ1bmN0aW9uXCImJnJlcXVpcmU7aWYoIXUmJmEpcmV0dXJuIGEobywhMCk7aWYoaSlyZXR1cm4gaShvLCEwKTt2YXIgZj1uZXcgRXJyb3IoXCJDYW5ub3QgZmluZCBtb2R1bGUgJ1wiK28rXCInXCIpO3Rocm93IGYuY29kZT1cIk1PRFVMRV9OT1RfRk9VTkRcIixmfXZhciBsPW5bb109e2V4cG9ydHM6e319O3Rbb11bMF0uY2FsbChsLmV4cG9ydHMsZnVuY3Rpb24oZSl7dmFyIG49dFtvXVsxXVtlXTtyZXR1cm4gcyhuP246ZSl9LGwsbC5leHBvcnRzLGUsdCxuLHIpfXJldHVybiBuW29dLmV4cG9ydHN9dmFyIGk9dHlwZW9mIHJlcXVpcmU9PVwiZnVuY3Rpb25cIiYmcmVxdWlyZTtmb3IodmFyIG89MDtvPHIubGVuZ3RoO28rKylzKHJbb10pO3JldHVybiBzfSkiLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vdHlwaW5ncy9pbmRleC5kLnRzXCIgLz5cclxuXHJcbmltcG9ydCB7c2F5SGVsbG99IGZyb20gXCIuLi9zcmMvY29tcG9uZW50L2dyZWV0LnRzXCI7XHJcbmltcG9ydCB7c2hvd0hlbGxvfSBmcm9tIFwiLi4vc3JjL2NvbXBvbmVudC9ncmVldC50c1wiO1xyXG5cclxuaW1wb3J0IHthc3NlcnR9IGZyb20gXCJjaGFpXCI7XHJcblxyXG5kZXNjcmliZShcIlVuaXQgdGVzdCBzdWl0ZVwiLCBmdW5jdGlvbigpe1xyXG4gICAgaXQoXCJzYXlIZWxsbyAmIHNob3dIZWxsbyBzaG91bGQgZXhpc3RcIiwgZnVuY3Rpb24oKXtcclxuICAgICAgICBhc3NlcnQuaXNGdW5jdGlvbihzYXlIZWxsbyk7XHJcbiAgICAgICAgYXNzZXJ0LmlzRnVuY3Rpb24oc2hvd0hlbGxvKTtcclxuICAgIH0pO1xyXG4gICAgLy8gaXQoXCJzaG93SGVsbG8gc2hvdWxkIHdvcmtcIiwgZnVuY3Rpb24oKXtcclxuICAgIC8vICAgICBsZXQgaWQgPSBcImhlbGxvRGl2XCI7XHJcbiAgICAvLyAgICAgbGV0IGRpdiA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKGlkKTtcclxuICAgIC8vICAgICBpZiAoIWRpdil7XHJcbiAgICAvLyAgICAgICAgIGRpdiA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoJ2RpdicpO1xyXG4gICAgLy8gICAgICAgICBkaXYuaWQgPSBpZDtcclxuICAgIC8vICAgICAgICAgZGl2LnN0eWxlLmRpc3BsYXkgPSAnbm9uZSc7XHJcbiAgICAvLyAgICAgICAgIGRvY3VtZW50LmJvZHkuYXBwZW5kQ2hpbGQoZGl2KTtcclxuICAgIC8vICAgICB9ICAgICAgICBcclxuICAgIC8vICAgICBsZXQgY29udGVudCA9IFwiVW5pdCBUZXN0XCI7XHJcbiAgICAvLyAgICAgc2hvd0hlbGxvKGlkLCBjb250ZW50KTtcclxuICAgIC8vICAgICBhc3NlcnQuZXF1YWwoZGl2LmlubmVySFRNTCwgXCJIZWxsbyBmcm9tIFVuaXQgVGVzdFwiKTtcclxuICAgIC8vIH0pO1xyXG59KTsiLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vdHlwaW5ncy9pbmRleC5kLnRzXCIgLz5cclxuXHJcbmltcG9ydCB7c2F5SGVsbG99IGZyb20gXCIuLi9zcmMvY29tcG9uZW50L2dyZWV0LnRzXCI7XHJcbmltcG9ydCB7c2hvd0hlbGxvfSBmcm9tIFwiLi4vc3JjL2NvbXBvbmVudC9ncmVldC50c1wiO1xyXG5cclxuaW1wb3J0IHthc3NlcnR9IGZyb20gXCJjaGFpXCI7XHJcblxyXG5kZXNjcmliZShcIlVuaXQgdGVzdCBzdWl0ZSAyXCIsIGZ1bmN0aW9uKCl7XHJcbiAgICBpdChcIjI6IHNheUhlbGxvICYgc2hvd0hlbGxvIHNob3VsZCBleGlzdFwiLCBmdW5jdGlvbigpe1xyXG4gICAgICAgIGFzc2VydC5pc0Z1bmN0aW9uKHNheUhlbGxvKTtcclxuICAgICAgICBhc3NlcnQuaXNGdW5jdGlvbihzaG93SGVsbG8pO1xyXG4gICAgfSk7XHJcbiAgICAvLyBpdChcInNob3dIZWxsbyBzaG91bGQgd29ya1wiLCBmdW5jdGlvbigpe1xyXG4gICAgLy8gICAgIGxldCBpZCA9IFwiaGVsbG9EaXZcIjtcclxuICAgIC8vICAgICBsZXQgZGl2ID0gZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoaWQpO1xyXG4gICAgLy8gICAgIGlmICghZGl2KXtcclxuICAgIC8vICAgICAgICAgZGl2ID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnZGl2Jyk7XHJcbiAgICAvLyAgICAgICAgIGRpdi5pZCA9IGlkO1xyXG4gICAgLy8gICAgICAgICBkaXYuc3R5bGUuZGlzcGxheSA9ICdub25lJztcclxuICAgIC8vICAgICAgICAgZG9jdW1lbnQuYm9keS5hcHBlbmRDaGlsZChkaXYpO1xyXG4gICAgLy8gICAgIH0gICAgICAgIFxyXG4gICAgLy8gICAgIGxldCBjb250ZW50ID0gXCJVbml0IFRlc3RcIjtcclxuICAgIC8vICAgICBzaG93SGVsbG8oaWQsIGNvbnRlbnQpO1xyXG4gICAgLy8gICAgIGFzc2VydC5lcXVhbChkaXYuaW5uZXJIVE1MLCBcIkhlbGxvIGZyb20gVW5pdCBUZXN0XCIpO1xyXG4gICAgLy8gfSk7XHJcbn0pOyIsImV4cG9ydCBmdW5jdGlvbiBzYXlIZWxsbyhuYW1lOiBzdHJpbmcpIHtcclxuICAgIHJldHVybiBgSGVsbG8gZnJvbSAke25hbWV9YDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNob3dIZWxsbyhkaXZOYW1lOiBzdHJpbmcsIG5hbWU6IHN0cmluZykge1xyXG4gICAgbGV0IGVsdCA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKGRpdk5hbWUpO1xyXG4gICAgZWx0LmlubmVyVGV4dCA9IHNheUhlbGxvKG5hbWUpO1xyXG59Il19
