
export function validatable(target: any){
    console.log("Validatable class annotation reached");
}

function init(target:any){
    if (!target.validators){
        target.validators = [];
    }
}

export function required(target:any, prop: string | symbol):any{
    console.log("Required property annotation reached");

    init(target);
    target.validators.push(function(modelErrors:string[]):boolean{
        if (!!this[prop]){
            modelErrors.push(prop.toString().concat(' validation success!'));
            return true;
        }else{
            modelErrors.push(prop.toString().concat(' cannot be empty!'));
            return false;
        }
    });
}

export function validate(target:any):[boolean, string[]]{
    let rlt = true;
    let modelErrors:string[] = [];
    if (target.validators != null && target.validators.length > 0){
        for(let v of target.validators){
            rlt = v.call(target, modelErrors) && rlt;
        }
    }
    console.log(modelErrors);
    return [rlt, modelErrors];
}

