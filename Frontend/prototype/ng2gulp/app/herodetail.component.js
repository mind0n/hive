"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var hero_1 = require("./hero");
var HeroDetailComponent = (function () {
    function HeroDetailComponent() {
    }
    __decorate([
        core_1.Input(), 
        __metadata('design:type', hero_1.Hero)
    ], HeroDetailComponent.prototype, "hero", void 0);
    HeroDetailComponent = __decorate([
        core_1.Component({
            selector: 'my-hero-detail',
            template: "\n    <div *ngIf=\"hero\">\n      <h2>{{hero.name}} details!</h2>\n      <div><label>id: </label>{{hero.id}}</div>\n      <div>\n        <label>name: </label>\n        <input [(ngModel)]=\"hero.name\" placeholder=\"name\"/>\n      </div>\n    </div>\n  "
        }), 
        __metadata('design:paramtypes', [])
    ], HeroDetailComponent);
    return HeroDetailComponent;
}());
exports.HeroDetailComponent = HeroDetailComponent;

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImhlcm9kZXRhaWwuY29tcG9uZW50LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7QUFBQSxxQkFBK0IsZUFBZSxDQUFDLENBQUE7QUFDL0MscUJBQW1CLFFBQVEsQ0FBQyxDQUFBO0FBZTVCO0lBQUE7SUFJQSxDQUFDO0lBRkM7UUFBQyxZQUFLLEVBQUU7O3FEQUFBO0lBZlY7UUFBQyxnQkFBUyxDQUFDO1lBQ1QsUUFBUSxFQUFFLGdCQUFnQjtZQUMxQixRQUFRLEVBQUUsK1BBU1Q7U0FDRixDQUFDOzsyQkFBQTtJQUtGLDBCQUFDO0FBQUQsQ0FKQSxBQUlDLElBQUE7QUFKWSwyQkFBbUIsc0JBSS9CLENBQUEiLCJmaWxlIjoiaGVyb2RldGFpbC5jb21wb25lbnQuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQge0NvbXBvbmVudCwgSW5wdXR9IGZyb20gXCJAYW5ndWxhci9jb3JlXCI7XHJcbmltcG9ydCB7SGVyb30gZnJvbSBcIi4vaGVyb1wiO1xyXG5cclxuQENvbXBvbmVudCh7XHJcbiAgc2VsZWN0b3I6ICdteS1oZXJvLWRldGFpbCcsXHJcbiAgdGVtcGxhdGU6IGBcclxuICAgIDxkaXYgKm5nSWY9XCJoZXJvXCI+XHJcbiAgICAgIDxoMj57e2hlcm8ubmFtZX19IGRldGFpbHMhPC9oMj5cclxuICAgICAgPGRpdj48bGFiZWw+aWQ6IDwvbGFiZWw+e3toZXJvLmlkfX08L2Rpdj5cclxuICAgICAgPGRpdj5cclxuICAgICAgICA8bGFiZWw+bmFtZTogPC9sYWJlbD5cclxuICAgICAgICA8aW5wdXQgWyhuZ01vZGVsKV09XCJoZXJvLm5hbWVcIiBwbGFjZWhvbGRlcj1cIm5hbWVcIi8+XHJcbiAgICAgIDwvZGl2PlxyXG4gICAgPC9kaXY+XHJcbiAgYFxyXG59KVxyXG5leHBvcnQgY2xhc3MgSGVyb0RldGFpbENvbXBvbmVudCB7XHJcbiAgXHJcbiAgQElucHV0KClcclxuICBoZXJvOkhlcm87XHJcbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
