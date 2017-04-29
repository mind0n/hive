"use strict";
var router_1 = require('@angular/router');
var hero_component_1 = require('./hero.component');
var dashboard_component_1 = require('./dashboard.component');
var appRoutes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: dashboard_component_1.DashboardComponent },
    { path: 'heroes', component: hero_component_1.HeroesComponent }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map