import {Injectable} from "@angular/core";

@Injectable()
export class DynamicTemplateBuilder {

    public prepareTemplate(entity: any, useTextarea: boolean){
      
      let properties = Object.keys(entity);
      let template = "<form >";
      let editorName = useTextarea 
        ? "text-editor"
        : "string-editor";
        
      properties.forEach((propertyName) =>{
        template += `
          <${editorName}
              [propertyName]="'${propertyName}'"
              [entity]="entity"
          ></${editorName}>`;
      });
  
      return template + "</form>";
    }
}