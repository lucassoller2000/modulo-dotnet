import React from 'react'
import Modal from '../../generic/Modal/Modal'
import { Redirect} from 'react-router-dom'
import ReservaService from '../../../Services/ReservaService'
import Alert from '../../generic/Alert/Alert'
import Selected from '../../../SelectedContents'
import MinhasReservasCard from '../MinhasReservasCard/MinhasReservasCard'
import './MinhasReservas.css'
export default class MinhasReservas extends React.Component {

    constructor() {
        super()
        this.state = {
            selectedReserva: {},
            selectedReservaToDelete: {},
            suite: {},
            reservas: [],
            error:'',
            selectedContent: Selected.MINHASRESERVAS
        }

        this.onClickDeleteButton = this.onClickDeleteButton.bind(this)
        this.onCloseModalSelectedReserva = this.onCloseModalSelectedReserva.bind(this)
        this.onCloseModalSelectedReservaToDelete = this.onCloseModalSelectedReservaToDelete.bind(this)
        this.deleteReserva = this.deleteReserva.bind(this)
        this.onClickLinkHome = this.onClickLinkHome.bind(this)
    }

    setSelectedContent(content) {
        this.setState({
            selectedContent: content
        })
    }

    componentDidMount() {
        this.loadReservas()
    }

    onCloseModalSelectedReserva() {
        this.setState({
            selectedReserva: {}
        })
    }

    onCloseModalSelectedReservaToDelete() {
        this.setState({
            selectedReservaToDelete: {},
            suite: {}
        })
    }

    onClickDeleteButton(selectedReservaToDelete, suite) {
        this.setState({
            selectedReservaToDelete,
            suite
        })
    }
    
    loadReservas() {
        ReservaService.obterReservas()
        .then(resp =>{
            this.setState({
                reservas: resp.data
            })
        })
        .catch(err =>{
            console.log(err.data)
        })
    }

    deleteReserva(id){
        ReservaService.deletarReserva(id)
        .then(() => {
            this.loadReservas()
        }).finally(() => {
            this.onCloseModalSelectedReservaToDelete()
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

    renderReservas() {
            const reservas = this.state.reservas.map((reserva) => {
                return <div key={reserva.id}>
                    <MinhasReservasCard
                        dataInicio={reserva.dataInicio}
                        dataFim={reserva.dataFim}
                        numeroPessoas={reserva.numeroPessoas}
                        valorTotal={reserva.valorTotal}
                        suiteNome={reserva.suite.nome}
                        suiteDescricao={reserva.suite.descricao}
                        suiteCapacidade={reserva.suite.capacidade}
                        suiteDiaria={reserva.suite.valorDiaria}
                        onClickDeleteButton={() => this.onClickDeleteButton(reserva, reserva.suite)}
                    />
                </div>
            })
            return <div className="MinhasReservas--content">
                {reservas}
            </div>
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
        return <div className="Opcionais--content">
    
                    <div className="navBar">
                        <h3><span className="MinhasReservas--link" onClick={this.onClickLinkHome}>Home</span></h3>        
                    </div>
                    <div className="MinhasReservas">
                    <div className="MinhasReservas--container col-md-9">
                        <div className="MinhasReservas--header">
                            <h3><span>Minhas Reservas</span></h3>
                        </div>
                    <div>
                    <div className="Posts">
                    <Modal show={this.state.suite.nome}
                        text={`Deseja excluir a reserva da suíte ${this.state.suite.nome} ?`}
                        title="Confirmação"
                        onClose={this.onCloseModalSelectedReservaToDelete}
                        classButtonAction="danger"
                        onClickButtonAction={() => this.deleteReserva(this.state.selectedReservaToDelete.id)}
                        textButtonAction="Excluir"
                    />
                    {this.renderError()}
                    {this.renderReservas()}
                    </div>
                </div>
            </div>
            </div>
        </div>
    }
}