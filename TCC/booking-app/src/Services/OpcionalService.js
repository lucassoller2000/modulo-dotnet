import axios from 'axios'
import CONFIG from '../config'
import LoginService from '../Services/LoginService'
export default class OpcionalService {
    static salvarOpcional(nome, descricao, valor) {
        return axios.post(`${CONFIG.API_URL_BASE}/api/opcional`, {
            nome,
            descricao,
            valor
        },
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static obterOpcionais() {
        return axios.get(`${CONFIG.API_URL_BASE}/api/opcional`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static deletarOpcional(id) {
        return axios.delete(`${CONFIG.API_URL_BASE}/api/opcional/${id}`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static editarOpcional(nome, descricao, valor, id) {
        return axios.put(`${CONFIG.API_URL_BASE}/api/opcional/${id}`, {
            nome,
            descricao,
            valor
        },
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }
}