import React from 'react'
import Input from '../../generic/Input/Input'
import Button from '../../generic/Button/Button'
import Alert from '../../generic/Alert/Alert'
import OpcionalService from '../../../Services/OpcionalService'
import {Redirect} from 'react-router-dom'
import '../../../global.css'
import Selected from '../../../SelectedContents'

export default class OpcionalEdicao extends React.Component {
    constructor() {
        super()
        this.state = this.getInitialState(), {
            selectedContent: Selected.EDITAROPCIONAL
        }
        this.handdleChange = this.handdleChange.bind(this)
        this.onClickSalvarOpcionalButton = this.onClickSalvarOpcionalButton.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    getInitialState() {
        return {
            nome: '',
            descricao: '',
            valor: 0,
            error: '',
            success:''
        }
    }
    onClickLinkCadastrarOpcional() {
        this.setSelectedContent(Selected.REGISTER)
    }

    handdleChange(event) {
        const target = event.target
        const value = target.value
        const name = target.name
        this.setState({
            [name]: value
        })
    }
    onSuccess(){
        this.setState({
            success: "Editado com sucesso"
        })
    }

    onClickSalvarOpcionalButton() {
        const account = this.state
            OpcionalService.editarOpcional(account.nome, account.descricao, account.valor, this.props.match.params.id)
            .then((result) => {
                this.setState(this.getInitialState())
                this.onSuccess()
            }).catch((err) => {
                this.setState({
                    error: err.response.data
                })
            })
    }

    onClickLinkHome(){
        this.setSelectedContent(Selected.HOME)
    }

    renderSuccess(){
        return this.state.success ? <Alert text={this.state.success} alertType="primary" /> : undefined
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    render() {
        if(this.state.selectedContent === Selected.HOME)
        {
            return <Redirect to='/home' />
        }
        if(!localStorage.getItem('USER_TYPE'))
        {
            return <Redirect to='/' />
        }
        if(localStorage.getItem('USER_TYPE') !== 'Admin')
        {
            return <Redirect to='/home' />
        }
        return (
                <div>
                    <div className="navBar">
                        <h3><span className="Post--link" onClick={this.onClickLinkHome}>Home</span></h3>                    </div>
                    <div className="Post">
                    <div className="Post--container col-md-5">
                        <div className="Post--header">
                            <h3><span>Editar Opcional</span></h3>
                        </div>
                    <div>
                        {this.renderSuccess()}
                        {this.renderError()}
                        <Input
                            label="Nome"
                            value={this.state.nome}
                            name="nome"
                            placeholder="Digite o nome"
                            handdleChange={this.handdleChange}
                            type="email"
                        />
                        <Input
                            label="Descrição"
                            value={this.state.descricao}
                            name="descricao"
                            placeholder="Digite a descrição"
                            handdleChange={this.handdleChange}
                            type="text"
                        />
                        <Input
                            label="Valor"
                            value={this.state.valor}
                            name="valor"
                            placeholder="Digite o valor"
                            handdleChange={this.handdleChange}
                            type="number"
                        />
                        <Button type="button"
                            text="Salvar"
                            onClick={this.onClickSalvarOpcionalButton}
                            classButton="primary" 
                        />
                    </div>
                </div>
            </div>
        </div>
        )
    }
}