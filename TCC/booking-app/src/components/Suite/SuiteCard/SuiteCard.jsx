import React from 'react'
import Button from '../../generic/Button/Button'
import './SuiteCard.css'

export default class SuiteCard extends React.Component{

    render() {
        return (
            <div className="Suites">
                <div className="SuiteCard">
                <div>
                    <div className="Suite--title"><h5>Nome da Suíte: {this.props.nome}</h5></div>
                    <div className="Suite--description"><h5>Descrição: {this.props.descricao}</h5></div>
                    <div className="Suite--capacity"><h5>Capacidade: {this.props.capacidade}</h5></div>
                    <div className="Suite--value"><h5>Valor da diária R$: {this.props.valorDiaria}</h5></div>
                </div>
                <div className="Suite--button">
                    <Button classButton="success" isOutline={true} onClick={this.props.onClickEditButton} text="Editar" />
                    <Button classButton="danger" isOutline={true} onClick={this.props.onClickDeleteButton} text="Excluir" />
                </div> 
                </div>  
            </div>)
    }
}