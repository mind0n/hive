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
var hero_service_1 = require('./hero.service');
var DashboardComponent = (function () {
    function DashboardComponent(heroService) {
        this.heroService = heroService;
    }
    DashboardComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.heroService.getHeros().then(function (heroes) { _this.topHeroes = heroes.slice(1, 5); });
    };
    DashboardComponent.prototype.gotoDetail = function (hero) {
    };
    DashboardComponent = __decorate([
        core_1.Component({
            selector: 'my-dashboard',
            template: "\n        <h3>Top Heroes</h3>\n        <div class=\"grid grid-pad\">\n            <div *ngFor=\"let hero of topHeroes\" (click)=\"gotoDetail(hero)\" class=\"col-1-4\">\n                <div class=\"module hero\">\n                    <h4>{{hero.name}}</h4>\n                </div>\n            </div>\n        </div>\n    ",
            providers: [hero_service_1.HeroService]
        }), 
        __metadata('design:paramtypes', [hero_service_1.HeroService])
    ], DashboardComponent);
    return DashboardComponent;
}());
exports.DashboardComponent = DashboardComponent;

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImRhc2hib2FyZC5jb21wb25lbnQudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7OztBQUFBLHFCQUFnQyxlQUFlLENBQUMsQ0FBQTtBQUloRCw2QkFBMEIsZ0JBQWdCLENBQUMsQ0FBQTtBQWdCM0M7SUFFSSw0QkFBb0IsV0FBdUI7UUFBdkIsZ0JBQVcsR0FBWCxXQUFXLENBQVk7SUFBRSxDQUFDO0lBQzlDLHFDQUFRLEdBQVI7UUFBQSxpQkFFQztRQURHLElBQUksQ0FBQyxXQUFXLENBQUMsUUFBUSxFQUFFLENBQUMsSUFBSSxDQUFDLFVBQUMsTUFBTSxJQUFJLEtBQUksQ0FBQyxTQUFTLEdBQUcsTUFBTSxDQUFDLEtBQUssQ0FBQyxDQUFDLEVBQUMsQ0FBQyxDQUFDLENBQUEsQ0FBQSxDQUFDLENBQUMsQ0FBQztJQUNyRixDQUFDO0lBQ0QsdUNBQVUsR0FBVixVQUFXLElBQVM7SUFFcEIsQ0FBQztJQXRCTDtRQUFDLGdCQUFTLENBQUM7WUFDUCxRQUFRLEVBQUMsY0FBYztZQUN2QixRQUFRLEVBQUMsb1VBU1I7WUFDQSxTQUFTLEVBQUMsQ0FBQywwQkFBVyxDQUFDO1NBQzNCLENBQUM7OzBCQUFBO0lBVUYseUJBQUM7QUFBRCxDQVRBLEFBU0MsSUFBQTtBQVRZLDBCQUFrQixxQkFTOUIsQ0FBQSIsImZpbGUiOiJkYXNoYm9hcmQuY29tcG9uZW50LmpzIiwic291cmNlc0NvbnRlbnQiOlsiaW1wb3J0IHtDb21wb25lbnQsIE9uSW5pdH0gZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcblxyXG5pbXBvcnQge0hlcm99IGZyb20gJy4vaGVybyc7XHJcblxyXG5pbXBvcnQge0hlcm9TZXJ2aWNlfSBmcm9tICcuL2hlcm8uc2VydmljZSc7XHJcblxyXG5AQ29tcG9uZW50KHtcclxuICAgIHNlbGVjdG9yOidteS1kYXNoYm9hcmQnLFxyXG4gICAgdGVtcGxhdGU6YFxyXG4gICAgICAgIDxoMz5Ub3AgSGVyb2VzPC9oMz5cclxuICAgICAgICA8ZGl2IGNsYXNzPVwiZ3JpZCBncmlkLXBhZFwiPlxyXG4gICAgICAgICAgICA8ZGl2ICpuZ0Zvcj1cImxldCBoZXJvIG9mIHRvcEhlcm9lc1wiIChjbGljayk9XCJnb3RvRGV0YWlsKGhlcm8pXCIgY2xhc3M9XCJjb2wtMS00XCI+XHJcbiAgICAgICAgICAgICAgICA8ZGl2IGNsYXNzPVwibW9kdWxlIGhlcm9cIj5cclxuICAgICAgICAgICAgICAgICAgICA8aDQ+e3toZXJvLm5hbWV9fTwvaDQ+XHJcbiAgICAgICAgICAgICAgICA8L2Rpdj5cclxuICAgICAgICAgICAgPC9kaXY+XHJcbiAgICAgICAgPC9kaXY+XHJcbiAgICBgXHJcbiAgICAscHJvdmlkZXJzOltIZXJvU2VydmljZV1cclxufSlcclxuZXhwb3J0IGNsYXNzIERhc2hib2FyZENvbXBvbmVudCBpbXBsZW1lbnRzIE9uSW5pdHtcclxuICAgIHRvcEhlcm9lczpIZXJvW107XHJcbiAgICBjb25zdHJ1Y3Rvcihwcml2YXRlIGhlcm9TZXJ2aWNlOkhlcm9TZXJ2aWNlKXt9XHJcbiAgICBuZ09uSW5pdCgpOnZvaWR7XHJcbiAgICAgICAgdGhpcy5oZXJvU2VydmljZS5nZXRIZXJvcygpLnRoZW4oKGhlcm9lcyk9Pnt0aGlzLnRvcEhlcm9lcyA9IGhlcm9lcy5zbGljZSgxLDUpfSk7XHJcbiAgICB9XHJcbiAgICBnb3RvRGV0YWlsKGhlcm86SGVybyk6dm9pZHtcclxuXHJcbiAgICB9XHJcbn0iXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
