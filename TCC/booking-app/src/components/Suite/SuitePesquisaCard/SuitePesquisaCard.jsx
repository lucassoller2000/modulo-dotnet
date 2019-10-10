import React from 'react'
import Button from '../../generic/Button/Button'
import './SuitePesquisaCard.css'
export default class SuitePesquisaCard extends React.Component{

    render() {
        return (
            <div className="SuitesPesquisa">
                <div className="SuitePesquisaCard">
                <div className="SuitePesquisa--content">
                    <div className="SuitePesquisa--title"><h5>Nome da Suíte: {this.props.nome}</h5></div>
                    <div className="SuitePesquisa--description"><h5>Descrição: {this.props.descricao}</h5></div>
                    <div className="SuitePesquisa--description"><h5>Capacidade: {this.props.capacidade}</h5></div>
                    <div className="SuitePesquisa--description"><h5>Valor da Diária R$: {this.props.valorDiaria}</h5></div>
                </div>
                <div className="SuitePesquisa--button">
                    <Button classButton="success" isOutline={true} onClick={this.props.onClickReservarButton} text="Reservar" />
                </div>   
                </div>
            </div>)
    }
}