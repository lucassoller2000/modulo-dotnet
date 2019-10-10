import React from 'react'
import Modal from '../../generic/Modal/Modal'
import { Redirect} from 'react-router-dom'
import SuiteService from '../../../Services/SuiteService'
import Alert from '../../generic/Alert/Alert'
import Selected from '../../../SelectedContents'
import SuiteCard from '../SuiteCard/SuiteCard'
import './Suites.css'

export default class Suites extends React.Component {

    constructor() {
        super()
        this.state = {
            selectedSuite: {},
            selectedSuiteToDelete: {},
            suites: [],
            idToEdit: '',
            error:'',
            selectedContent: Selected.LISTARSUITES
        }

        this.onClickDeleteButton = this.onClickDeleteButton.bind(this)
        this.onClickEditButton = this.onClickEditButton.bind(this)
        this.onCloseModalSelectedSuite = this.onCloseModalSelectedSuite.bind(this)
        this.onCloseModalSelectedSuiteToDelete = this.onCloseModalSelectedSuiteToDelete.bind(this)
        this.deleteSuite = this.deleteSuite.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    componentDidMount() {
        this.loadSuites()
    }

    onCloseModalSelectedSuite() {
        this.setState({
            selectedSuite: {}
        })
    }

    onCloseModalSelectedSuiteToDelete() {
        this.setState({
            selectedSuiteToDelete: {}
        })
    }

    onClickDeleteButton(selectedSuiteToDelete) {
        this.setState({
            selectedSuiteToDelete
        })
    }

    onClickEditButton(id) {
        this.setState({
            idToEdit: id,
            selectedContent: Selected.EDITARSUITE
        })
    }

    
    loadSuites() {
        SuiteService.obterSuites()
        .then(resp =>{
            this.setState({
                suites: resp.data
            })
        })
        .catch(err =>{
            console.log(err.data)
        })
    }

    deleteSuite(id){
        SuiteService.deletarSuite(id)
        .then(() => {
            this.loadSuites()
        }).finally(() => {
            this.onCloseModalSelectedSuiteToDelete()
        }).catch((err =>{
            this.setState({
                error: err.response.data
            })
        }))
    }

    onClickLinkHome(){
        this.setSelectedContent(Selected.HOME)
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    renderSuites() {
            const suites = this.state.suites.map((suite) => {
                return <div key={suite.id}>
                    <SuiteCard
                        nome={suite.nome}
                        descricao={suite.descricao}
                        capacidade={suite.capacidade}
                        valorDiaria={suite.valorDiaria}
                        onClickDeleteButton={() => this.onClickDeleteButton(suite)}
                        onClickEditButton={() => this.onClickEditButton(suite.id)}
                    />
                </div>
            })
            return <div className="Suites--content">
                {suites}
            </div>
    }
    render() {
        if(this.state.selectedContent === Selected.EDITARSUITE)
        {
            return <Redirect to ={'/suite/editar/' + `${this.state.idToEdit}`} />
        }
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
        return <div className="Suite--content">
    
                    <div className="navBar">
                        <h3><span className="Post--link" onClick={this.onClickLinkHome}>Home</span></h3> 
                    </div>
                    <div className="Suite">
                        <div className="Suite--container col-md-9">
                            <div className="Suite--header">
                                <h3><span>Lista de Suites</span></h3>
                            </div>
                            <Modal show={this.state.selectedSuiteToDelete.nome}
                                text={`Deseja excluir a suíte ${this.state.selectedSuiteToDelete.nome} ?`}
                                title="Confirmação"
                                onClose={this.onCloseModalSelectedSuiteToDelete}
                                classButtonAction="danger"
                                onClickButtonAction={() => this.deleteSuite(this.state.selectedSuiteToDelete.id)}
                                textButtonAction="Excluir"
                            />
                            {this.renderError()}
                            {this.renderSuites()}
                        </div>
                    </div>
                </div>
    }
}