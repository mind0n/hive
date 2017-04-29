import * as $ from "jquery";
import * as React from "react";
import * as ReactDOM from "react-dom";

import HelloComponent from "./components/Hello";
import LabelTextbox from "./components/LabelTextbox";
import WebUser from "./components/WebUser";

$(function(){
    let id:string = "target";
    let container:Element = document.getElementById(id);
    let model:WebUser = new WebUser("James", null);
    function validate():boolean{
        return model.validate();
    }
    let wrapper:any = <div>
    <HelloComponent class="welcome" compiler="TypeScript" framework="ReactJS" />
    <LabelTextbox class="field-username" type="text" label="Username" model={model} />
    <button onClick={validate}>Validate</button></div>; 
    ReactDOM.render(wrapper, container);
});
