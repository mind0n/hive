import {required} from "./validation";
import {validatable} from "./validation";
import {validate} from "./validation";

@validatable
export default class WebUser {


    @required
    UserId: string;

    @required
    Pwd: string;


    constructor(userId?: string, pwd?: string) {
        this.UserId = userId;
        this.Pwd = pwd;
    }

    validate():boolean{
        return validate(this)[0];
    }
}



