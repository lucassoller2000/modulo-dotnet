import axios from 'axios'
import CONFIG from '../config'
import LoginService from './LoginService'
export default class SuiteService {
    static salvarSuite(nome, descricao, capacidade, valorDiaria) {
        return axios.post(`${CONFIG.API_URL_BASE}/api/suite`, {
            nome,
            descricao,
            capacidade,
            valorDiaria,
        },
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static obterSuites() {
        return axios.get(`${CONFIG.API_URL_BASE}/api/suite`, 
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        }
        )
    }

    static pesquisarSuites(dataInicio, dataFim, numeroPessoas) {
        return axios.get(`${CONFIG.API_URL_BASE}/api/suite/pesquisa?DataInicio=${dataInicio}&DataFim=${dataFim}&NumeroPessoas=${numeroPessoas}`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        }
        )
    }

    static deletarSuite(id) {
        return axios.delete(`${CONFIG.API_URL_BASE}/api/suite/${id}`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static editarSuite(nome, descricao, capacidade, valorDiaria, id) {
        return axios.put(`${CONFIG.API_URL_BASE}/api/suite/${id}`, {
            nome,
            descricao,
            capacidade,
            valorDiaria
        },
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }
}