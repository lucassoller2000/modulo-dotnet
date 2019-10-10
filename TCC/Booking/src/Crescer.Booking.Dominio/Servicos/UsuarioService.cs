using System;
using System.Collections.Generic;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;

namespace Crescer.Booking.Dominio.Servicos
{
    public class UsuarioService
    {

        public List<string> Validar(Usuario usuario)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(usuario.PrimeiroNome))
            {
                mensagens.Add("É necessário informar o primeiro nome");
                return mensagens;
            }
            
            if (string.IsNullOrEmpty(usuario.UltimoNome))
            {
                mensagens.Add("É necessário informar o último nome");
                return mensagens;
            }

            if (string.IsNullOrEmpty(usuario.Cpf))
            {   
                mensagens.Add("É necessário informar o CPF");
                return mensagens;
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                mensagens.Add("É necessário informar o email");
                return mensagens;
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                mensagens.Add("É necessário informar a senha");
                return mensagens;
            }

            if (usuario.DataNascimento?.Date.Year < 1900 || 
                usuario.DataNascimento?.Date.Year > 2018 ||  
                usuario.DataNascimento?.Day < 1 || 
                usuario.DataNascimento?.Day > 31 || 
                usuario.DataNascimento?.Month < 1 || 
                usuario.DataNascimento?.Month > 12)
            {
                mensagens.Add("É necessário informar uma data de nascimento válida");
                return mensagens;
            }

            if(usuario == null)
            {
                mensagens.Add("Nenhum usuário foi encontrado");
                return mensagens;
            }

            return mensagens;
        }

        public List<string> ValidarLogin(Usuario usuario)
        {
            List<string> mensagens = new List<string>();
            
            if(usuario == null)
                mensagens.Add("Email ou senha inválidos");

            return mensagens;
        }

        public List<string> ValidarUsuariosIguais(string email, IUsuarioRepository usuarioRepository)
        {
            List<string> mensagens = new List<string>();

            if(usuarioRepository.obterUsuariosIguais(email))
                mensagens.Add("Esse e-mail já está em uso");

            return mensagens;
        }
    }
}