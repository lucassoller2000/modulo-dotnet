import React, { Component } from 'react';
import RegisterForm from './components/RegisterForm/RegisterForm'
import LoginForm from './components/LoginForm/LoginForm'
import OpcionalCadastro from  './components/Opcional/OpcionalCadastro/OpcionalCadastro'
import SuiteCadastro from './components/Suite/SuiteCadastro/SuiteCadastro'
import Home from './components/Home/Home'
import Reserva from './components/Reserva/Reserva'
import NotFound from './components/NotFound/NotFound'
import Opcionais from './components/Opcional/Opcionais/Opcionais'
import OpcionalEdicao from './components/Opcional/OpcionalEdicao/OpcionalEdicao'
import Suites from './components/Suite/Suites/Suites'
import SuiteEdicao from './components/Suite/SuiteEdicao/SuiteEdicao'
import { Switch, Route, Redirect} from 'react-router-dom'
import SuitePesquisa from './components/Suite/SuitesPesquisa/SuitesPesquisa'
import MinhasReservas from './components/Reserva/MinhasReservas/MinhasReservas'
import './App.css';

class App extends Component {
  render() {
    return (
      <div>
        <Switch>
            <Route path="/404" component={NotFound} />
            <Route path="/" exact component={LoginForm} />
            <Route path="/home" component={Home} />    
            <Route path="/opcionalCadastro" component={OpcionalCadastro} />
            <Route path="/listar/opcionais" component={Opcionais} />
            <Route path="/listar/suites" component={Suites} />
            <Route path="/suiteCadastro" component={SuiteCadastro} />
            <Route path="/opcional/editar/:id" component={OpcionalEdicao} />
            <Route path="/suite/editar/:id" component={SuiteEdicao} />
            <Route path="/reserva" component={Reserva} />
            <Route path="/registro" component={RegisterForm} />
            <Route path="/pesquisar/suites" component={SuitePesquisa} />
            <Route path="/listar/reservas" component={MinhasReservas} />
            <Redirect to="/404"/>
          </Switch>
      </div>
    );
  }
}

export default App;
