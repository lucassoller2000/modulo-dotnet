import React from 'react'
import Button from '../../generic/Button/Button'
import './MinhasReservasCard.css'
export default class SuiteCard extends React.Component{

    render() {
        return (
            <div className="MinhaReserva">
                <div className="MinhaReservaCard">
                <div className="MinhaReservaCard--content">
                    <div className="MinhaReserva--description"><h5>Nome da Suíte: {this.props.suiteNome}</h5></div>
                    <div className="MinhaReserva--description"><h5>Descrição: {this.props.suiteDescricao}</h5></div>
                    <div className="MinhaReserva--description"><h5>Capacidade: {this.props.suiteCapacidade}</h5></div>
                    <div className="MinhaReserva--description"><h5>Valor da Diária R$: {this.props.suiteDiaria}</h5></div>
                    <div className="MinhaReserva--description"><h5>Data do Início da Reservas: {this.props.dataInicio}</h5></div>
                    <div className="MinhaReserva--description"><h5>Data do Fim da Reserva: {this.props.dataFim}</h5></div>
                    <div className="MinhaReserva--description"><h5>Número de Pessoas: {this.props.numeroPessoas}</h5></div>
                    <div className="MinhaReserva--description"><h5>Valor Total R$: {this.props.valorTotal}</h5></div>
                </div>
                <div className="MinhaReserva--button">
                    <Button classButton="danger" isOutline={true} onClick={this.props.onClickDeleteButton} text="Excluir" />
                </div>   
                </div>
            </div>)
    }
}