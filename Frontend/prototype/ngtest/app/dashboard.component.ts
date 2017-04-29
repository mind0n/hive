import {Component, OnInit} from '@angular/core';

import {Hero} from './hero';

import {HeroService} from './hero.service';

@Component({
    selector:'my-dashboard',
    template:`
        <h3>Top Heroes</h3>
        <div class="grid grid-pad">
            <div *ngFor="let hero of topHeroes" (click)="gotoDetail(hero)" class="col-1-4">
                <div class="module hero">
                    <h4>{{hero.name}}</h4>
                </div>
            </div>
        </div>
    `
    ,providers:[HeroService]
})
export class DashboardComponent implements OnInit{
    topHeroes:Hero[];
    constructor(private heroService:HeroService){}
    ngOnInit():void{
        this.heroService.getHeros().then((heroes)=>{this.topHeroes = heroes.slice(1,5)});
    }
    gotoDetail(hero:Hero):void{

    }
}