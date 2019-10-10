import axios from 'axios'
import CONFIG from '../config'
export default class RegisterService {
    static register(email, senha, primeiroNome, ultimoNome, cpf, dataNascimento) {
        return axios.post(`${CONFIG.API_URL_BASE}/api/usuario`, {
            email,
            senha,
            primeiroNome,
            ultimoNome,
            cpf,
            dataNascimento
        })
    }
}