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
var platform_browser_1 = require('@angular/platform-browser');
var forms_1 = require('@angular/forms');
var app_component_1 = require('./app.component');
var herodetail_component_1 = require('./herodetail.component');
var hero_component_1 = require('./hero.component');
var dashboard_component_1 = require('./dashboard.component');
var app_routing_1 = require('./app.routing');
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, app_routing_1.routing],
            declarations: [app_component_1.AppComponent, herodetail_component_1.HeroDetailComponent, hero_component_1.HeroesComponent, dashboard_component_1.DashboardComponent],
            bootstrap: [app_component_1.AppComponent]
        }), 
        __metadata('design:paramtypes', [])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC5tb2R1bGUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7OztBQUFBLHFCQUE4QixlQUFlLENBQUMsQ0FBQTtBQUM5QyxpQ0FBOEIsMkJBQTJCLENBQUMsQ0FBQTtBQUMxRCxzQkFBNEIsZ0JBQWdCLENBQUMsQ0FBQTtBQUM3Qyw4QkFBNkIsaUJBQWlCLENBQUMsQ0FBQTtBQUMvQyxxQ0FBb0Msd0JBQXdCLENBQUMsQ0FBQTtBQUM3RCwrQkFBOEIsa0JBQWtCLENBQUMsQ0FBQTtBQUNqRCxvQ0FBaUMsdUJBQXVCLENBQUMsQ0FBQTtBQUN6RCw0QkFBc0IsZUFBZSxDQUFDLENBQUE7QUFPdEM7SUFBQTtJQUF5QixDQUFDO0lBTDFCO1FBQUMsZUFBUSxDQUFDO1lBQ1IsT0FBTyxFQUFPLENBQUMsZ0NBQWEsRUFBRSxtQkFBVyxFQUFFLHFCQUFPLENBQUM7WUFDbkQsWUFBWSxFQUFFLENBQUMsNEJBQVksRUFBRSwwQ0FBbUIsRUFBRSxnQ0FBZSxFQUFFLHdDQUFrQixDQUFDO1lBQ3RGLFNBQVMsRUFBSyxDQUFDLDRCQUFZLENBQUM7U0FDN0IsQ0FBQzs7aUJBQUE7SUFDdUIsZ0JBQUM7QUFBRCxDQUF6QixBQUEwQixJQUFBO0FBQWIsaUJBQVMsWUFBSSxDQUFBIiwiZmlsZSI6ImFwcC5tb2R1bGUuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBOZ01vZHVsZSB9ICAgICAgZnJvbSAnQGFuZ3VsYXIvY29yZSc7XHJcbmltcG9ydCB7IEJyb3dzZXJNb2R1bGUgfSBmcm9tICdAYW5ndWxhci9wbGF0Zm9ybS1icm93c2VyJztcclxuaW1wb3J0IHsgRm9ybXNNb2R1bGUgfSBmcm9tICdAYW5ndWxhci9mb3Jtcyc7XHJcbmltcG9ydCB7IEFwcENvbXBvbmVudCB9IGZyb20gJy4vYXBwLmNvbXBvbmVudCc7XHJcbmltcG9ydCB7IEhlcm9EZXRhaWxDb21wb25lbnQgfSBmcm9tICcuL2hlcm9kZXRhaWwuY29tcG9uZW50JztcclxuaW1wb3J0IHtIZXJvZXNDb21wb25lbnR9IGZyb20gJy4vaGVyby5jb21wb25lbnQnO1xyXG5pbXBvcnQge0Rhc2hib2FyZENvbXBvbmVudH0gZnJvbSAnLi9kYXNoYm9hcmQuY29tcG9uZW50JztcclxuaW1wb3J0IHtyb3V0aW5nfSBmcm9tICcuL2FwcC5yb3V0aW5nJztcclxuXHJcbkBOZ01vZHVsZSh7XHJcbiAgaW1wb3J0czogICAgICBbQnJvd3Nlck1vZHVsZSwgRm9ybXNNb2R1bGUsIHJvdXRpbmddLFxyXG4gIGRlY2xhcmF0aW9uczogW0FwcENvbXBvbmVudCwgSGVyb0RldGFpbENvbXBvbmVudCwgSGVyb2VzQ29tcG9uZW50LCBEYXNoYm9hcmRDb21wb25lbnRdLFxyXG4gIGJvb3RzdHJhcDogICAgW0FwcENvbXBvbmVudF1cclxufSlcclxuZXhwb3J0IGNsYXNzIEFwcE1vZHVsZSB7IH1cclxuIl0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
