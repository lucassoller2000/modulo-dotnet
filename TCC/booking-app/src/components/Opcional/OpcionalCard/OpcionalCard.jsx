import React from 'react'
import Button from '../../generic/Button/Button'
import './OpcionalCard.css'
export default class OpcionalCard extends React.Component{

    render() {
        return (
            <div className="Opcional">
                <div className="OpcionalCard">
                <div className="OpcionalCard--content">
                    <div className="Opcional--title"><h5>Nome do Opcional: {this.props.nome}</h5></div>
                    <div className="Opcional--description"><h5>Descrição: {this.props.descricao}</h5></div>
                    <div className="Opcional--value"><h5>Valor R$: {this.props.valor}</h5></div>
                </div>
                <div className="Opcional--button">
                    <Button classButton="success" isOutline={true} onClick={this.props.onClickEditButton} text="Editar" />
                    <Button classButton="danger" isOutline={true} onClick={this.props.onClickDeleteButton} text="Excluir" />
                </div> 
                </div>  
            </div>)
    }
}