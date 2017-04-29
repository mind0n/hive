import {ModuleWithProviders} from '@angular/core';
import {Routes,RouterModule} from '@angular/router';
import {HeroesComponent} from './hero.component';
import {DashboardComponent} from './dashboard.component';

const appRoutes: Routes=[
    {path:'', redirectTo:'/dashboard', pathMatch:'full'}
    ,{path:'dashboard', component:DashboardComponent}
    ,{path:'heroes', component:HeroesComponent}
];

export const routing:ModuleWithProviders = RouterModule.forRoot(appRoutes);
