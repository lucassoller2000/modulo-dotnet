import React from 'react'
import './Home.css'
import '../../global.css'
import {Redirect} from 'react-router-dom'
import Selected from '../../SelectedContents'
import LoginService from '../../Services/LoginService'

export default class Home extends React.Component {
    constructor() {
        super()
        this.state = {
            selectedContent: Selected.HOME
        }
        this.onClickLinkCadastrarSuite = this.onClickLinkCadastrarSuite.bind(this)
        this.onClickLinkCadastrarOpcional = this.onClickLinkCadastrarOpcional.bind(this)
        this.onClickLinkListarOpcionais = this.onClickLinkListarOpcionais.bind(this)
        this.onClickLinkLogout = this.onClickLinkLogout.bind(this)
        this.onClickLinkListarSuites = this.onClickLinkListarSuites.bind(this)
        this.onClickLinkMinhasReservas = this.onClickLinkMinhasReservas.bind(this)
        this.onClickLinkReservarSuites = this.onClickLinkReservarSuites.bind(this)
        this.onClickLinkPesquisarSuites = this.onClickLinkPesquisarSuites.bind(this)
    }
    
    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    
    onClickLinkCadastrarSuite() {
        this.setSelectedContent(Selected.SUITE)
    }

    onClickLinkPesquisarSuites() {
        this.setSelectedContent(Selected.PESQUISARSUITES)
    }

    onClickLinkCadastrarOpcional() {
        this.setSelectedContent(Selected.OPCIONAL)
    }

    onClickLinkListarOpcionais() {
        this.setSelectedContent(Selected.LISTAROPCIONAIS)
    }

    onClickLinkLogout(){
        LoginService.logout()
        this.setSelectedContent(Selected.LOGIN)
    }

    onClickLinkListarSuites(){
        this.setSelectedContent(Selected.LISTARSUITES)
    }

    onClickLinkMinhasReservas(){
        this.setSelectedContent(Selected.MINHASRESERVAS)
    }

    onClickLinkReservarSuites(){
        this.setSelectedContent(Selected.PESQUISARSUITES)
    }

    renderAdmin() {
        if(localStorage.getItem('USER_TYPE') === 'Admin'){
            return <div>
            <h3><span className="Home--link" onClick={this.onClickLinkCadastrarSuite}>Cadastrar Suíte</span></h3>
            <h3><span className="Home--link" onClick={this.onClickLinkCadastrarOpcional}>Cadastrar Opcional</span></h3>
            <h3><span className="Home--link" onClick={this.onClickLinkListarOpcionais}>Listar Opcionais</span></h3>
            <h3><span className="Home--link" onClick={this.onClickLinkListarSuites}>Listar Suítes</span></h3></div>
        }
    }

    render() {
        if( this.state.selectedContent === Selected.SUITE){
            return <Redirect to='/suiteCadastro' />
        }
        if( this.state.selectedContent === Selected.OPCIONAL){
            return <Redirect to='/opcionalCadastro' />
        }
        if( this.state.selectedContent === Selected.LISTAROPCIONAIS){
            return <Redirect to='/listar/opcionais' />
        }
        if( this.state.selectedContent === Selected.LOGIN){
            return <Redirect to='/' />
        }
        if( this.state.selectedContent === Selected.LISTARSUITES){
            return <Redirect to='/listar/suites' />
        }
        if( this.state.selectedContent === Selected.PESQUISARSUITES){
            return <Redirect to='/pesquisar/suites' />
        }
        if( this.state.selectedContent === Selected.MINHASRESERVAS){
            return <Redirect to='/listar/reservas' />
        }
        if( this.state.selectedContent === Selected.PESQUISARSUITES){
            return <Redirect to='/pesquisar/suites' />
        }
        if(!localStorage.getItem('USER_TYPE'))
        {
            return <Redirect to='/' />
        }
        return (
            <div className="Home">
                <div className="navBar">
                    <h3><span className="Home--link" onClick={this.onClickLinkMinhasReservas}>Minhas Reservas</span></h3>
                    <h3><span className="Home--link" onClick={this.onClickLinkReservarSuites}>Reservar Suítes</span></h3>
                    {this.renderAdmin()}
                    <h3><span className="Home--link" onClick={this.onClickLinkLogout}>Logout</span></h3>
                </div>
                <div className="Home--content"></div>
            </div>
        )
    }
}