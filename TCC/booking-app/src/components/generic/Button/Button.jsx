import React from 'react'
export default class Button extends React.Component {

    getClassName() {
        return `btn btn-${this.props.classButton} ${this.props.classModal} ${this.props.class}`
    }
    render() {
        return <button className={this.getClassName()}
            onClick={this.props.onClick}
            data-toggle={this.props.datatoggle}
            data-target={this.props.datatarget} 
            data-whatever={this.props.datawhatever}
            type={this.props.type}>
            {this.props.text}</button>
    }

}