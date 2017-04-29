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
var HEROES = [
    { id: 11, name: 'Mr. Nice' },
    { id: 12, name: 'Narco' },
    { id: 13, name: 'Bombasto' },
    { id: 14, name: 'Celeritas' },
    { id: 15, name: 'Magneta' },
    { id: 16, name: 'RubberMan' },
    { id: 17, name: 'Dynama' },
    { id: 18, name: 'Dr IQ' },
    { id: 19, name: 'Magma' },
    { id: 20, name: 'Tornado' }
];
var HeroService = (function () {
    function HeroService() {
    }
    HeroService.prototype.getHeros = function () {
        return Promise.resolve(HEROES);
    };
    HeroService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], HeroService);
    return HeroService;
}());
exports.HeroService = HeroService;

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImhlcm8uc2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7O0FBQUEscUJBQXlCLGVBQWUsQ0FBQyxDQUFBO0FBS3pDLElBQU0sTUFBTSxHQUFXO0lBQ3JCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsVUFBVSxFQUFFO0lBQzVCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsT0FBTyxFQUFFO0lBQ3pCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsVUFBVSxFQUFFO0lBQzVCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsV0FBVyxFQUFFO0lBQzdCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFFO0lBQzNCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsV0FBVyxFQUFFO0lBQzdCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsUUFBUSxFQUFFO0lBQzFCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsT0FBTyxFQUFFO0lBQ3pCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsT0FBTyxFQUFFO0lBQ3pCLEVBQUUsRUFBRSxFQUFFLEVBQUUsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFFO0NBQzVCLENBQUM7QUFHRjtJQUFBO0lBSUEsQ0FBQztJQUhHLDhCQUFRLEdBQVI7UUFDSSxNQUFNLENBQUMsT0FBTyxDQUFDLE9BQU8sQ0FBQyxNQUFNLENBQUMsQ0FBQztJQUNuQyxDQUFDO0lBSkw7UUFBQyxpQkFBVSxFQUFFOzttQkFBQTtJQUtiLGtCQUFDO0FBQUQsQ0FKQSxBQUlDLElBQUE7QUFKWSxtQkFBVyxjQUl2QixDQUFBIiwiZmlsZSI6Imhlcm8uc2VydmljZS5qcyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7SW5qZWN0YWJsZX0gZnJvbSBcIkBhbmd1bGFyL2NvcmVcIjtcclxuXHJcbmltcG9ydCB7SGVyb30gZnJvbSBcIi4vaGVyb1wiO1xyXG5cclxuXHJcbmNvbnN0IEhFUk9FUzogSGVyb1tdID0gW1xyXG4gIHsgaWQ6IDExLCBuYW1lOiAnTXIuIE5pY2UnIH0sXHJcbiAgeyBpZDogMTIsIG5hbWU6ICdOYXJjbycgfSxcclxuICB7IGlkOiAxMywgbmFtZTogJ0JvbWJhc3RvJyB9LFxyXG4gIHsgaWQ6IDE0LCBuYW1lOiAnQ2VsZXJpdGFzJyB9LFxyXG4gIHsgaWQ6IDE1LCBuYW1lOiAnTWFnbmV0YScgfSxcclxuICB7IGlkOiAxNiwgbmFtZTogJ1J1YmJlck1hbicgfSxcclxuICB7IGlkOiAxNywgbmFtZTogJ0R5bmFtYScgfSxcclxuICB7IGlkOiAxOCwgbmFtZTogJ0RyIElRJyB9LFxyXG4gIHsgaWQ6IDE5LCBuYW1lOiAnTWFnbWEnIH0sXHJcbiAgeyBpZDogMjAsIG5hbWU6ICdUb3JuYWRvJyB9XHJcbl07XHJcblxyXG5ASW5qZWN0YWJsZSgpXHJcbmV4cG9ydCBjbGFzcyBIZXJvU2VydmljZXtcclxuICAgIGdldEhlcm9zKCk6UHJvbWlzZTxIZXJvW10+e1xyXG4gICAgICAgIHJldHVybiBQcm9taXNlLnJlc29sdmUoSEVST0VTKTtcclxuICAgIH1cclxufSJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==
