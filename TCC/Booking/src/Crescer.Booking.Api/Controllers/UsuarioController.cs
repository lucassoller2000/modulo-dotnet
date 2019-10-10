using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Crescer.Booking.Api.Models.Resquest;
using Crescer.Booking.Dominio;
using Crescer.Booking.Dominio.Contratos;
using Crescer.Booking.Dominio.Entidades;
using Crescer.Booking.Dominio.Servicos;
using Crescer.Booking.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Crescer.Booking.Api.Controllers
{
    [Authorize, Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository;
        private UsuarioService usuarioService;
        private BookingContext contexto;
        private IOptions<SecuritySettings> settings;
        
        public UsuarioController(IUsuarioRepository usuarioRepository, UsuarioService usuarioService, BookingContext contexto, IOptions<SecuritySettings> settings)
        {
            this.usuarioRepository = usuarioRepository;
            this.usuarioService = usuarioService;
            this.contexto = contexto;
            this.settings = settings;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var usuariosSemSenha = this.IterarLista(usuarioRepository.ListarUsuarios());
            return Ok(usuariosSemSenha);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult Get (int id)
        {
            var usuario = usuarioRepository.Obter(id);

            var mensagem = usuarioService.Validar(usuario);

            if(mensagem.Any()) return BadRequest(mensagem);

            var usuarioSemSenha = MapearDtoParaDominio(usuario);

            return Ok(usuarioSemSenha);
        }

        // POST api/values
        [AllowAnonymous, HttpPost]
        public IActionResult Post([FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var usuarioCadastrado = usuarioRepository.SalvarUsuario(usuario);
            
            var mensagem = usuarioService.Validar(usuarioCadastrado);

            if(mensagem.Any()) return BadRequest(mensagem);

            var mensagemUsuariosIguais = usuarioService.ValidarUsuariosIguais(usuarioCadastrado.Email, usuarioRepository);

            if(mensagemUsuariosIguais.Any()) return BadRequest(mensagemUsuariosIguais);
            
            contexto.SaveChanges();
            var usuarioSemSenha = MapearDtoParaDominio(usuario);
            return CreatedAtRoute("GetUsuario", new { id = usuarioSemSenha.Id }, usuarioSemSenha);
        }

         // POST api/values
        [AllowAnonymous, HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDto loginRequest)
        {
            var usuario = usuarioRepository.ObterUsuarioPorEmailESenha(loginRequest.Email, loginRequest.Senha);
            
            var mensagem = usuarioService.ValidarLogin(usuario);

            if(mensagem.Any()) return BadRequest(mensagem);
        
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.SigningKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(ClaimTypes.Name, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario),
                },
                expires: DateTime.Now.AddMinutes(2000),
                signingCredentials: signingCredentials);

            var tokenUsuario = new JwtSecurityTokenHandler().WriteToken(token);
            var usuarioComToken = new UsuarioComTokenDto(usuario.TipoUsuario, tokenUsuario, usuario.Email);

            return Ok(usuarioComToken);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UsuarioDto usuarioRequest)
        {
            var usuario = MapearDtoParaDominio(usuarioRequest);
            var usuarioCadastrado = usuarioRepository.AtualizarUsuario(id, usuario);
            
            var mensagem = usuarioService.Validar(usuarioCadastrado);

            if(mensagem.Any()) return BadRequest(mensagem);

            var mensagemUsuariosIguais = usuarioService.ValidarUsuariosIguais(usuarioCadastrado.Email, usuarioRepository);

            if(mensagemUsuariosIguais.Any()) return BadRequest(mensagemUsuariosIguais);
            
            contexto.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuarioCadastrado = usuarioRepository.DeletarUsuario(id);
            
            var mensagem = usuarioService.Validar(usuarioCadastrado);

            if(mensagem.Any()) return BadRequest(mensagem);
            
            contexto.SaveChanges();
            return Ok();
        }

        private Usuario MapearDtoParaDominio(UsuarioDto usuarioRequest)
        {
            return new Usuario(usuarioRequest.Email, usuarioRequest.Senha, usuarioRequest.PrimeiroNome,
            usuarioRequest.UltimoNome, usuarioRequest.Cpf, usuarioRequest.DataNascimento);
        }

        private UsuarioSemSenha MapearDtoParaDominio(Usuario usuario)
        {
            return new UsuarioSemSenha(usuario.Id, usuario.Email, usuario.PrimeiroNome,
            usuario.UltimoNome, usuario.Cpf, usuario.DataNascimento);
        }

        private List<UsuarioSemSenha> IterarLista(List<Usuario> usuarios)
        {
            var usuariosSemSenha = new List<UsuarioSemSenha>();
           foreach (var usuario in usuarios)
           {
               var usuarioSemSenha = this.MapearDtoParaDominio(usuario);
               usuariosSemSenha.Add(usuarioSemSenha);
           }

           return usuariosSemSenha; 
        }
    }
}
