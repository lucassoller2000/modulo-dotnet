import React from 'react'
import Input from '../generic/Input/Input'
import Button from '../generic/Button/Button'
import Alert from '../generic/Alert/Alert'
import LoginService from '../../Services/LoginService'
import './LoginForm.css'
import '../../global.css'
import {Redirect} from 'react-router-dom'

const SELECTED_CONTENTS = {
    LOGIN: 'LOGIN',
    REGISTER: 'REGISTER',
    HOME: 'HOME'

}


export default class LoginForm extends React.Component {
    constructor() {
        super()
        this.state = this.getInitialState(),{
            selectedContent: SELECTED_CONTENTS.LOGIN
        }
        this.handdleChange = this.handdleChange.bind(this)
        this.onClickLoginButton = this.onClickLoginButton.bind(this)
        this.onClickLinkRegister = this.onClickLinkRegister.bind(this)
    }

    getInitialState() {
        return {
            email: '',
            senha: '',
            error: ''
        }
    }
    
    onClickLinkRegister() {
        this.setSelectedContent(SELECTED_CONTENTS.REGISTER)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    handdleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }

    onClickLoginButton() {
        const account = this.state
            LoginService.login(account.email, account.senha)
            .then((result) => {
                this.setState(this.getInitialState())
                this.setSelectedContent(SELECTED_CONTENTS.HOME)
            }).catch((err) => {
                this.setState({
                    error: err.response.data
                })
            })
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    render() {
        if( this.state.selectedContent === SELECTED_CONTENTS.REGISTER){
            return <Redirect to='/registro' />
        }
        if( this.state.selectedContent === SELECTED_CONTENTS.HOME){
            return <Redirect to='/home' />
        }
        return (
            <div className="Login">
            <div className="Post--container">
                <div className="Post--header">
                    <h3>Por Favor, faça o  <span className="Login--link" >Login</span>, ou <span className="Login--link" onClick={this.onClickLinkRegister}>Cadastre-se</span></h3>
                </div>
                <div className="col-sm-12">
                {this.renderError()}
                <Input
                    label="E-mail"
                    value={this.state.email}
                    name="email"
                    placeholder="Digite seu e-mail"
                    handdleChange={this.handdleChange}
                    type="email"
                />
                <Input
                    label="Senha"
                    value={this.state.senha}
                    name="senha"
                    placeholder="Digite sua senha"
                    handdleChange={this.handdleChange}
                    type="password"
                />
                <Button type="button"
                    text="Login"
                    onClick={this.onClickLoginButton}
                    classButton="primary" 
                />
                </div>
            </div>
        </div>
        )
    }
}