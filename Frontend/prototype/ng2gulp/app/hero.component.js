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
var core_1 = require('@angular/core');
var hero_service_1 = require("./hero.service");
var HeroesComponent = (function () {
    function HeroesComponent(heroService) {
        this.heroService = heroService;
    }
    HeroesComponent.prototype.onSelect = function (hero) {
        this.selectedHero = hero;
    };
    HeroesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.heroService.getHeros().then(function (heroes) { return _this.heroes = heroes; });
    };
    HeroesComponent = __decorate([
        core_1.Component({
            selector: 'my-heroes',
            template: "\n    <h2>My Heroes</h2>\n    <ul class=\"heroes\">\n      <li *ngFor=\"let hero of heroes\" (click)=\"onSelect(hero)\" [class.selected]=\"hero === selectedHero\">\n        <span class=\"badge\">{{hero.id}}</span> {{hero.name}}\n      </li>\n    </ul>\n    <my-hero-detail [hero]=\"selectedHero\"></my-hero-detail>\n    ",
            styles: ["\n      .selected {\n        background-color: #CFD8DC !important;\n        color: white;\n      }\n      .heroes {\n        margin: 0 0 2em 0;\n        list-style-type: none;\n        padding: 0;\n        width: 15em;\n      }\n      .heroes li {\n        cursor: pointer;\n        position: relative;\n        left: 0;\n        background-color: #EEE;\n        margin: .5em;\n        padding: .3em 0;\n        height: 1.6em;\n        border-radius: 4px;\n      }\n      .heroes li.selected:hover {\n        background-color: #BBD8DC !important;\n        color: white;\n      }\n      .heroes li:hover {\n        color: #607D8B;\n        background-color: #DDD;\n        left: .1em;\n      }\n      .heroes .text {\n        position: relative;\n        top: -3px;\n      }\n      .heroes .badge {\n        display: inline-block;\n        font-size: small;\n        color: white;\n        padding: 0.8em 0.7em 0 0.7em;\n        background-color: #607D8B;\n        line-height: 1em;\n        position: relative;\n        left: -1px;\n        top: -4px;\n        height: 1.8em;\n        margin-right: .8em;\n        border-radius: 4px 0 0 4px;\n      }\n    "],
            providers: [hero_service_1.HeroService]
        }), 
        __metadata('design:paramtypes', [hero_service_1.HeroService])
    ], HeroesComponent);
    return HeroesComponent;
}());
exports.HeroesComponent = HeroesComponent;

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImhlcm8uY29tcG9uZW50LnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7QUFBQSxxQkFBeUMsZUFBZSxDQUFDLENBQUE7QUFJekQsNkJBQTBCLGdCQUFnQixDQUFDLENBQUE7QUFnRTNDO0lBR0kseUJBQW9CLFdBQXdCO1FBQXhCLGdCQUFXLEdBQVgsV0FBVyxDQUFhO0lBRTVDLENBQUM7SUFDRCxrQ0FBUSxHQUFSLFVBQVMsSUFBVTtRQUNqQixJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksQ0FBQztJQUMzQixDQUFDO0lBQ0Qsa0NBQVEsR0FBUjtRQUFBLGlCQUVDO1FBREMsSUFBSSxDQUFDLFdBQVcsQ0FBQyxRQUFRLEVBQUUsQ0FBQyxJQUFJLENBQUMsVUFBQSxNQUFNLElBQUUsT0FBQSxLQUFJLENBQUMsTUFBTSxHQUFDLE1BQU0sRUFBbEIsQ0FBa0IsQ0FBQyxDQUFDO0lBQy9ELENBQUM7SUF6RUw7UUFBQyxnQkFBUyxDQUFDO1lBQ1QsUUFBUSxFQUFFLFdBQVc7WUFDckIsUUFBUSxFQUFFLGtVQVFQO1lBQ0QsTUFBTSxFQUFFLENBQUMsc29DQWdEUixDQUFDO1lBQ0EsU0FBUyxFQUFDLENBQUMsMEJBQVcsQ0FBQztTQUM1QixDQUFDOzt1QkFBQTtJQWFGLHNCQUFDO0FBQUQsQ0FaQSxBQVlDLElBQUE7QUFaWSx1QkFBZSxrQkFZM0IsQ0FBQSIsImZpbGUiOiJoZXJvLmNvbXBvbmVudC5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IENvbXBvbmVudCwgSW5wdXQsIE9uSW5pdCB9IGZyb20gJ0Bhbmd1bGFyL2NvcmUnO1xyXG5cclxuaW1wb3J0IHtIZXJvfSBmcm9tIFwiLi9oZXJvXCI7XHJcblxyXG5pbXBvcnQge0hlcm9TZXJ2aWNlfSBmcm9tIFwiLi9oZXJvLnNlcnZpY2VcIjtcclxuXHJcbkBDb21wb25lbnQoe1xyXG4gIHNlbGVjdG9yOiAnbXktaGVyb2VzJyxcclxuICB0ZW1wbGF0ZTogYFxyXG4gICAgPGgyPk15IEhlcm9lczwvaDI+XHJcbiAgICA8dWwgY2xhc3M9XCJoZXJvZXNcIj5cclxuICAgICAgPGxpICpuZ0Zvcj1cImxldCBoZXJvIG9mIGhlcm9lc1wiIChjbGljayk9XCJvblNlbGVjdChoZXJvKVwiIFtjbGFzcy5zZWxlY3RlZF09XCJoZXJvID09PSBzZWxlY3RlZEhlcm9cIj5cclxuICAgICAgICA8c3BhbiBjbGFzcz1cImJhZGdlXCI+e3toZXJvLmlkfX08L3NwYW4+IHt7aGVyby5uYW1lfX1cclxuICAgICAgPC9saT5cclxuICAgIDwvdWw+XHJcbiAgICA8bXktaGVyby1kZXRhaWwgW2hlcm9dPVwic2VsZWN0ZWRIZXJvXCI+PC9teS1oZXJvLWRldGFpbD5cclxuICAgIGAsXHJcbiAgICBzdHlsZXM6IFtgXHJcbiAgICAgIC5zZWxlY3RlZCB7XHJcbiAgICAgICAgYmFja2dyb3VuZC1jb2xvcjogI0NGRDhEQyAhaW1wb3J0YW50O1xyXG4gICAgICAgIGNvbG9yOiB3aGl0ZTtcclxuICAgICAgfVxyXG4gICAgICAuaGVyb2VzIHtcclxuICAgICAgICBtYXJnaW46IDAgMCAyZW0gMDtcclxuICAgICAgICBsaXN0LXN0eWxlLXR5cGU6IG5vbmU7XHJcbiAgICAgICAgcGFkZGluZzogMDtcclxuICAgICAgICB3aWR0aDogMTVlbTtcclxuICAgICAgfVxyXG4gICAgICAuaGVyb2VzIGxpIHtcclxuICAgICAgICBjdXJzb3I6IHBvaW50ZXI7XHJcbiAgICAgICAgcG9zaXRpb246IHJlbGF0aXZlO1xyXG4gICAgICAgIGxlZnQ6IDA7XHJcbiAgICAgICAgYmFja2dyb3VuZC1jb2xvcjogI0VFRTtcclxuICAgICAgICBtYXJnaW46IC41ZW07XHJcbiAgICAgICAgcGFkZGluZzogLjNlbSAwO1xyXG4gICAgICAgIGhlaWdodDogMS42ZW07XHJcbiAgICAgICAgYm9yZGVyLXJhZGl1czogNHB4O1xyXG4gICAgICB9XHJcbiAgICAgIC5oZXJvZXMgbGkuc2VsZWN0ZWQ6aG92ZXIge1xyXG4gICAgICAgIGJhY2tncm91bmQtY29sb3I6ICNCQkQ4REMgIWltcG9ydGFudDtcclxuICAgICAgICBjb2xvcjogd2hpdGU7XHJcbiAgICAgIH1cclxuICAgICAgLmhlcm9lcyBsaTpob3ZlciB7XHJcbiAgICAgICAgY29sb3I6ICM2MDdEOEI7XHJcbiAgICAgICAgYmFja2dyb3VuZC1jb2xvcjogI0RERDtcclxuICAgICAgICBsZWZ0OiAuMWVtO1xyXG4gICAgICB9XHJcbiAgICAgIC5oZXJvZXMgLnRleHQge1xyXG4gICAgICAgIHBvc2l0aW9uOiByZWxhdGl2ZTtcclxuICAgICAgICB0b3A6IC0zcHg7XHJcbiAgICAgIH1cclxuICAgICAgLmhlcm9lcyAuYmFkZ2Uge1xyXG4gICAgICAgIGRpc3BsYXk6IGlubGluZS1ibG9jaztcclxuICAgICAgICBmb250LXNpemU6IHNtYWxsO1xyXG4gICAgICAgIGNvbG9yOiB3aGl0ZTtcclxuICAgICAgICBwYWRkaW5nOiAwLjhlbSAwLjdlbSAwIDAuN2VtO1xyXG4gICAgICAgIGJhY2tncm91bmQtY29sb3I6ICM2MDdEOEI7XHJcbiAgICAgICAgbGluZS1oZWlnaHQ6IDFlbTtcclxuICAgICAgICBwb3NpdGlvbjogcmVsYXRpdmU7XHJcbiAgICAgICAgbGVmdDogLTFweDtcclxuICAgICAgICB0b3A6IC00cHg7XHJcbiAgICAgICAgaGVpZ2h0OiAxLjhlbTtcclxuICAgICAgICBtYXJnaW4tcmlnaHQ6IC44ZW07XHJcbiAgICAgICAgYm9yZGVyLXJhZGl1czogNHB4IDAgMCA0cHg7XHJcbiAgICAgIH1cclxuICAgIGBdXHJcbiAgICAsIHByb3ZpZGVyczpbSGVyb1NlcnZpY2VdXHJcbn0pXHJcbmV4cG9ydCBjbGFzcyBIZXJvZXNDb21wb25lbnQgaW1wbGVtZW50cyBPbkluaXQge1xyXG4gICAgc2VsZWN0ZWRIZXJvOkhlcm87XHJcbiAgICBoZXJvZXM6SGVyb1tdO1xyXG4gICAgY29uc3RydWN0b3IocHJpdmF0ZSBoZXJvU2VydmljZTogSGVyb1NlcnZpY2Upe1xyXG5cclxuICAgIH1cclxuICAgIG9uU2VsZWN0KGhlcm86IEhlcm8pOiB2b2lkIHtcclxuICAgICAgdGhpcy5zZWxlY3RlZEhlcm8gPSBoZXJvO1xyXG4gICAgfVxyXG4gICAgbmdPbkluaXQoKTp2b2lke1xyXG4gICAgICB0aGlzLmhlcm9TZXJ2aWNlLmdldEhlcm9zKCkudGhlbihoZXJvZXM9PnRoaXMuaGVyb2VzPWhlcm9lcyk7XHJcbiAgICB9XHJcbn1cclxuXHJcblxyXG4iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
