import React from 'react'
import Input from '../../generic/Input/Input'
import Button from '../../generic/Button/Button'
import Alert from '../../generic/Alert/Alert'
import SuiteService from '../../../Services/SuiteService'
import {Redirect} from 'react-router-dom'
import '../../../global.css'
import Selected from '../../../SelectedContents'

export default class SuiteEdicao extends React.Component {
    constructor() {
        super()
        this.state = this.getInitialState(),{
            selectedContent: Selected.EDITARSUITE
        }
        this.handdleChange = this.handdleChange.bind(this)
        this.onClickEditarSuiteButton = this.onClickEditarSuiteButton.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
    }

    getInitialState() {
        return {
            nome: '',
            descricao: '',
            capacidade: 0,
            valorDiaria: 0,
            error: '',
            success:''
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
    onSuccess(){
        this.setState({
            success: "Editado com sucesso"
        })
    }

    onClickEditarSuiteButton() {
        const account = this.state
            SuiteService.editarSuite(account.nome, account.descricao, account.capacidade, account.valorDiaria,
            this.props.match.params.id)
            .then((result) => {
                this.setState(this.getInitialState())
                this.onSuccess()
            }).catch((err) => {
                this.setState({
                    error: err.response.data
                })
            })
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
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
                    <h3><span className="Post--link" onClick={this.onClickLinkHome}>Home</span></h3>                
                </div>
                <div className="Post">
                <div className="Post--container col-md-5">
                    <div className="Post--header">
                        <h3><span>Editar Suíte</span></h3>
                    </div>
                    <div className="suiteForm">
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
                            label="Capacidade"
                            value={this.state.capacidade}
                            name="capacidade"
                            placeholder="Digite a capacidade"
                            handdleChange={this.handdleChange}
                            type="number"
                        />
                        <Input
                            label="Valor da diária"
                            value={this.state.valorDiaria}
                            name="valorDiaria"
                            placeholder="Digite o valor da diária"
                            handdleChange={this.handdleChange}
                            type="number"
                        />
                        <Button type="button"
                            text="Salvar"
                            onClick={this.onClickEditarSuiteButton}
                            classButton="primary" 
                        />
                    </div>
                </div>
            </div>
        </div>
        )
    }
}