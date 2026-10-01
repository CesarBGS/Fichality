using Fichality.Data;
using Fichality.DTOs;
using Fichality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fichality.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutenticacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/autenticacao/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o e-mail já está cadastrado
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (usuarioExiste)
                return BadRequest(new { sucesso = false, mensagem = "Este e-mail já está em uso." });

            // Criptografa a senha simples usando BCrypt (ou Hash direto para teste inicial)
            // Para simplicidade inicial, gravamos o Hash simulado:
            var novoUsuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha), // Criptografia segura
                Perfil = "Jogador",
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { sucesso = true, mensagem = "Usuário cadastrado com sucesso!", usuarioId = novoUsuario.Id });
        }

        // POST: api/autenticacao/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Unauthorized(new { sucesso = false, mensagem = "E-mail ou senha inválidos." });

            if (!usuario.Ativo)
                return BadRequest(new { sucesso = false, mensagem = "Conta desativada." });

            return Ok(new
            {
                sucesso = true,
                mensagem = "Login realizado com sucesso!",
                usuario = new { usuario.Id, usuario.Nome, usuario.Email, usuario.Perfil }
            });
        }
    }
}