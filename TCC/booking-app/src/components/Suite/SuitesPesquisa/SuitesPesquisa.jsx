import React from 'react'
import Input from '../../generic/Input/Input'
import Button from '../../generic/Button/Button'
import Alert from '../../generic/Alert/Alert'
import SuiteService from '../../../Services/SuiteService'
import {Redirect} from 'react-router-dom'
import Selected from '../../../SelectedContents'
import SuitePesquisaCard from '../SuitePesquisaCard/SuitePesquisaCard'
import './SuitesPesquisa.css'
import ReservaService from '../../../Services/ReservaService'


export default class SuitesPesquisa extends React.Component {
    constructor() {
        super()
        this.state = this.getInitialState(),{
            selectedContent: Selected.SUITE
        }
        this.handdleChange = this.handdleChange.bind(this)
        this.onClickSalvarSuiteButton = this.onClickSalvarSuiteButton.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
        this.onSuccess = this.onSuccess.bind(this)
    }


    getInitialState() {
        return {
            dataInicio: undefined,
            dataFim: undefined,
            numeroPessoas: 0,
            error: '',
            success:'',
            suites:[],
            valores: []
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
            success: "Cadastrado com sucesso"
        })
    }
    onClickLinkHome(){
        this.setSelectedContent(Selected.HOME)
    }

    onClickSalvarSuiteButton() {
        const account = this.state
            SuiteService.pesquisarSuites(account.dataInicio, account.dataFim, account.numeroPessoas)
            .then((result) => {
                this.setState({
                    suites: result.data,
                    error: ''
                })
            }).catch((err) => {
                this.setState({
                    error: err.response.data,
                    suites: []
                })
            })
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    onClickReservarButton(idSuite){
        const account = this.state
        ReservaService.salvarReserva(idSuite, account.numeroPessoas, account.dataInicio, account.dataFim)
        .then((result) => {
            this.onSuccess()
            this.setState({
                suites: []
            })     
        }).catch((err) => {
            this.setState({
                error: err.response.data,
            })
        })
    }


    renderSuccess(){
        return this.state.success ? <Alert text={this.state.success} alertType="danger" /> : undefined
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    renderSuites() {
        const suites = this.state.suites.map((suite) => {
            return <div key={suite.id}>
                <SuitePesquisaCard
                    nome={suite.nome}
                    descricao={suite.descricao}
                    capacidade={suite.capacidade}
                    valorDiaria={suite.valorDiaria}
                    id={suite.id}
                    onClickReservarButton={() => this.onClickReservarButton(suite.id)}
                />
            </div>
        })
        return <div className="SuitePesquisa--content">
            {suites}
        </div>
}

    render() {
        if( this.state.selectedContent === Selected.SUITE)
        {
            return <Redirect to='/suiteCadastro' />
        }
        if(this.state.selectedContent === Selected.HOME)
        {
            return <Redirect to='/home' />
        }
        if(!localStorage.getItem('USER_TYPE'))
        {
            return <Redirect to='/' />
        }
        return (
            <div className="SuitePesquisa--head">
                <div className="navBar">
                    <h3><span className="SuitePesquisa--link" onClick={this.onClickLinkHome}>Home</span></h3>    
                </div>
                <div className="SuitePesquisa">
                <div className="SuitePesquisa--container col-md-5">
                    <div className="SuitePesquisa--header">
                        <h3><span>Pesquisar Suítes</span></h3>
                    </div>
                    <div className="suiteForm">
                        {this.renderError()}
                        
                        <Input
                            label="Data do início da reserva"
                            value={this.state.dataInicio}
                            name="dataInicio"
                            handdleChange={this.handdleChange}
                            type="date"
                        />
                        <Input
                            label="Data do fim da reserva"
                            value={this.state.dataFim}
                            name="dataFim"
                            handdleChange={this.handdleChange}
                            type="date"
                        />
                        <Input
                            label="Número de pessoas"
                            value={this.state.numeroPessoas}
                            name="numeroPessoas"
                            placeholder="Digite o número de pessoas"
                            handdleChange={this.handdleChange}
                            type="number"
                        />
                        
                        <Button type="button"
                            text="Pesquisar"
                            onClick={this.onClickSalvarSuiteButton}
                            classButton="primary" 
                        />
                        
                    </div>
                    <div className="SuitesPesquisa">
                        {this.renderSuites()}
                    </div> 
                </div>
            </div>
        </div>
        )
    }
}