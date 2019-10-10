import CONFIG from '../config'
import axios from 'axios'

const LOGGED_USER = 'LOGGED_USER'
const USER_TYPE = 'USER_TYPE'

export default class LoginService {

	static setLoggedUser(token, tipoUsuario) {
		localStorage.setItem(LOGGED_USER, token)
		localStorage.setItem(USER_TYPE, tipoUsuario)
	}

	static login(email, senha) {
		return axios.post(`${CONFIG.API_URL_BASE}/api/usuario/login`, {
			email,
			senha
		}).then((result) => {
			console.info(result);
			this.setLoggedUser(result.data.token, result.data.tipoUsuario)
			return result
		})
	}

	static getLoggedUser() {
		return localStorage.getItem(LOGGED_USER)
	}

	static logout() {
		localStorage.removeItem(LOGGED_USER);
		localStorage.removeItem(USER_TYPE);
	}
}