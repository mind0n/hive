import { Component, Input, OnInit } from '@angular/core';

import {Hero} from "./hero";

import {HeroService} from "./hero.service";

@Component({
  selector: 'my-app',
  template: `
    <h1>{{title}}</h1>
    <a routerLink="/dashboard">Dashboard</a>
    <a routerLink="/heroes">Heroes</a>
    <router-outlet></router-outlet>
    `,
    styles: [``]
})
export class AppComponent {
    title = 'Tour of Heros';
}
