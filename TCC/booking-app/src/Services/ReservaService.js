import axios from 'axios'
import CONFIG from '../config'
import LoginService from '../Services/LoginService'
export default class ReservaService {
    static salvarReserva(idSuite, numeroPessoas, dataInicio, dataFim) {
        return axios.post(`${CONFIG.API_URL_BASE}/api/BaseReserva`,
        {
            idSuite,
            numeroPessoas,
            dataInicio,
            dataFim,
            idOpcionais: []
        },
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }
    static obterReservas() {
        return axios.get(`${CONFIG.API_URL_BASE}/api/BaseReserva/usuario`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }

    static deletarReserva(id) {
        return axios.delete(`${CONFIG.API_URL_BASE}/api/BaseReserva/${id}/usuario`,
        {
            headers: {
                authorization:  `Bearer ${LoginService.getLoggedUser()}`,
                'Content-Type': 'application/json',
            }
        })
    }
}