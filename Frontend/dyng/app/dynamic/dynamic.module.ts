import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

// parts module
import { PartsModule }   from '../parts/parts.module';

// detail stuff
import { DynamicDetail }          from './detail.view';
import { DynamicTypeBuilder }     from './type.builder';
import { DynamicTemplateBuilder } from './template.builder';

@NgModule({
  imports:      [ PartsModule ],
  declarations: [ DynamicDetail ],
  exports:      [ DynamicDetail],
})

export class DynamicModule {

    static forRoot()
    {
        return {
            ngModule: DynamicModule,
            providers: [ // singletons accross the whole app
              DynamicTemplateBuilder,
              DynamicTypeBuilder
            ], 
        };
    }
}