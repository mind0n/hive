import * as React from "react";
import * as ReactDOM from "react-dom";


export default class LabelTextbox extends React.Component<any, any>{
    update(event:any):any{
        let model = this.props.model;
        let box = this.refs["box"] as HTMLInputElement;
        model.UserId = box.value;
        this.setState({value:event.target.value});
    }
    
    render():any{
        let classes = 'lb-txt ' + this.props.class;
        let model = this.props.model;
        return <div className={classes}>
            <div className="label">{this.props.label}</div>
            <div className="txt-container"><input ref="box" className="txt" type={this.props.type} onChange={(e)=>this.update(e)} value={model.UserId} /></div>
        </div>
    }
}