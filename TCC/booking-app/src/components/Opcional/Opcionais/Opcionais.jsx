import React from 'react'
import OpcionalCard from '../OpcionalCard/OpcionalCard'
import OpcionalService from '../../../Services/OpcionalService'
import Modal from '../../generic/Modal/Modal'
import { Redirect} from 'react-router-dom'
import Alert from '../../generic/Alert/Alert'
import './Opcionais.css'
import Selected from '../../../SelectedContents'

export default class Opcionais extends React.Component {

    constructor() {
        super()
        this.state = {
            selectedOpcional: {},
            selectedOpcionalToDelete: {},
            opcionais: [],
            idToEdit: '',
            error:'',
            selectedContent: Selected.LISTAROPCIONAIS
        }

        this.onClickDeleteButton = this.onClickDeleteButton.bind(this)
        this.onClickEditButton = this.onClickEditButton.bind(this)
        this.onCloseModalSelectedOpcional = this.onCloseModalSelectedOpcional.bind(this)
        this.onCloseModalSelectedOpcionalToDelete = this.onCloseModalSelectedOpcionalToDelete.bind(this)
        this.deleteOpcional = this.deleteOpcional.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    componentDidMount() {
        this.loadOpcionais()
    }

    onCloseModalSelectedOpcional() {
        this.setState({
            selectedOpcional: {}
        })
    }

    onCloseModalSelectedOpcionalToDelete() {
        this.setState({
            selectedOpcionalToDelete: {}
        })
    }

    onClickDeleteButton(selectedOpcionalToDelete) {
        this.setState({
            selectedOpcionalToDelete
        })
    }

    onClickEditButton(id) {
        this.setState({
            idToEdit: id,
            selectedContent: Selected.EDITAROPCIONAL
        })
    }

    
    loadOpcionais() {
        OpcionalService.obterOpcionais()
        .then((resp =>{
            this.setState({
                opcionais: resp.data
            })
        }))
    }

    deleteOpcional(id){
        OpcionalService.deletarOpcional(id)
        .then(() => {
            this.loadOpcionais()
        }).finally(() => {
            this.onCloseModalSelectedOpcionalToDelete()
        }).catch((err =>{
            this.setState({
                error: err.response.data
            })
        }))
    }

    renderError() {
        return this.state.error ? <Alert text={this.state.error} alertType="danger" /> : undefined
    }

    renderOpcionais() {
            const opcionais = this.state.opcionais.map((opcional) => {
                return <div key={opcional.id}>
                    <OpcionalCard
                        nome={opcional.nome}
                        descricao={opcional.descricao}
                        valor={opcional.valor}
                        id={opcional.id}
                        onClickDeleteButton={() => this.onClickDeleteButton(opcional)}
                        onClickEditButton={() => this.onClickEditButton(opcional.id)}
                    />
                </div>
            })
            return <div className="Opcional--content">
                {opcionais}
            </div>
        // }
    }

    onClickLinkHome(){
        this.setSelectedContent(Selected.HOME)
    }
    render() {
        if(this.state.selectedContent === Selected.EDITAROPCIONAL)
        {
            return <Redirect to ={'/opcional/editar/' + `${this.state.idToEdit}`} />
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
        return <div className="Opcionais--content">
    
                    <div className="navBar">
                        <h3><span className="Opcional--link" onClick={this.onClickLinkHome}>Home</span></h3>
                    </div>
                    <div className="Opcionais">
                    <div className="Opcional--container col-md-9">
                        <div className="Opcional--header">
                            <h3><span>Lista de Opcionais</span></h3>
                        </div>
                    <div>
                    <div className="Posts">
                    <Modal show={this.state.selectedOpcionalToDelete.nome}
                        text={`Deseja excluir o opcional ${this.state.selectedOpcionalToDelete.nome} ?`}
                        title="Confirmação"
                        onClose={this.onCloseModalSelectedOpcionalToDelete}
                        classButtonAction="danger"
                        onClickButtonAction={() => this.deleteOpcional(this.state.selectedOpcionalToDelete.id)}
                        textButtonAction="Excluir"
                    />
                    {this.renderError()}
                    {this.renderOpcionais()}
                    </div>
                </div>
            </div>
            </div>
        </div>
    }
}