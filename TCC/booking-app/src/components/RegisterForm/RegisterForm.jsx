import React from 'react'
import Input from '../generic/Input/Input'
import Button from '../generic/Button/Button'
import Alert from '../generic/Alert/Alert'
import RegisterService from '../../Services/RegisterService'
import './RegisterForm.css'
import '../../global.css'
import {Redirect} from 'react-router-dom'
import Selected from '../../SelectedContents'

const SELECTED_CONTENTS = {
    LOGIN: 'LOGIN',
}

export default class RegisterForm extends React.Component {
    constructor() {
        super()
        this.state = this.getInitialState(),{
            selectedContent: Selected.LOGIN
        }
        this.handdleChange = this.handdleChange.bind(this)
        this.onClickRegisterButton = this.onClickRegisterButton.bind(this)
        this.onClickLinkLogin = this.onClickLinkLogin.bind(this)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    getInitialState() {
        return {
            email: '',
            senha: '',
            primeiroNome: '',
            ultimoNome: '',
            cpf: '',
            dataNascimento: undefined,
            error: '',
            registered:''
        }
    }

    handdleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }
    
    renderRegisterSuccess() {
        return this.state.registered ? <Alert alertType="primary" text={this.state.registered} /> : undefined
    }

    onClickLinkLogin(){
        this.setSelectedContent(SELECTED_CONTENTS.LOGIN)
    }
    onClickRegisterButton() {
        const account = this.state
            RegisterService
            .register(account.email, account.senha, account.primeiroNome, account.ultimoNome, account.cpf,
            account.dataNascimento)
            .then((result) => {
                this.setState(this.getInitialState())
                this.success()
            }).catch((err) => {
                this.setState({
                    error: err.response.data.error
                })
            })
    }
    success(){
        this.setState({
            registered: "Cadastro realizado com sucesso, faça o login"
        })
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    render() {
        if(this.state.selectedContent === SELECTED_CONTENTS.LOGIN){
            <Redirect to='/' />
        }

        return (
            <div className="Register">
            <div className="Register--container col-md-5">
                <div className="Register--header">
                <h3>Por Favor, faça o  <span className="Register--link" onClick={this.onClickLinkLogin}>Login</span>, ou <span className="Register--link">Cadastre-se</span></h3>
                </div>
                <div className="App-content">
                    <div>
                        {this.renderRegisterSuccess()}
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

                        <Input
                            label="Primeiro nome"
                            value={this.state.primeiroNome}
                            name="primeiroNome"
                            placeholder="Digite seu primeiro nome"
                            handdleChange={this.handdleChange}
                            type="text"
                        />
                        <Input
                            label="Último nome"
                            value={this.state.ultimoNome}
                            name="ultimoNome"
                            placeholder="Digite seu último nome"
                            handdleChange={this.handdleChange}
                            type="text"
                        />
                        <Input
                            label="CPF"
                            value={this.state.cpf}
                            name="cpf"
                            placeholder="Digite seu cpf"
                            handdleChange={this.handdleChange}
                            type="text"
                        />

                        <Input
                            label="Data de nascimento"
                            value={this.state.dataNascimento}
                            name="dataNascimento"
                            handdleChange={this.handdleChange}
                            type="date"
                        />
                        <Button type="button"
                            text="Registrar"
                            onClick={this.onClickRegisterButton}
                            classButton="primary" />
                    </div>
                </div>
            </div>
        </div>

        )
    }
}