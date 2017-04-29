import * as React from "react";
import * as ReactDOM from "react-dom";

export default class HelloComponent extends React.Component<any, any>{
    render():any{
        return <div className={this.props.class}>{this.props.framework}</div>
    }
}