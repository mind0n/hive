import { Component }          from '@angular/core';

@Component({
  selector: 'my-app',  
  template: `
<div>
   <h2>An app with DYNAMIC content</h2>
   <hr />
   <dynamic-detail></dynamic-detail>
</div>`,
})
export class AppComponent { }