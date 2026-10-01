using Fichality.Data;
using Fichality.DTOs;
using Fichality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fichality.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FichaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ficha/usuario/1 (Listar todas as fichas de um jogador)
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObterFichasPorUsuario(int usuarioId)
        {
            var fichas = await _context.Fichas
                .Where(f => f.UsuarioId == usuarioId)
                .Select(f => new
                {
                    f.Id,
                    f.NomePersonagem,
                    f.SistemaRpg,
                    f.DataCriacao,
                    f.DataAtualizacao
                })
                .ToListAsync();

            return Ok(fichas);
        }

        // GET: api/ficha/5 (Obter uma ficha específica com o JSON completo)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var ficha = await _context.Fichas.FindAsync(id);

            if (ficha == null)
                return NotFound(new { sucesso = false, mensagem = "Ficha não encontrada." });

            return Ok(ficha);
        }

        // POST: api/ficha/criar
        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarFichaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaFicha = new Ficha
            {
                NomePersonagem = dto.NomePersonagem,
                SistemaRpg = dto.SistemaRpg,
                UsuarioId = dto.UsuarioId,
                EstruturaJson = "{}", // Inicia com um JSON vazio
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            _context.Fichas.Add(novaFicha);
            await _context.SaveChangesAsync();

            return Ok(new { sucesso = true, mensagem = "Ficha criada com sucesso!", fichaId = novaFicha.Id });
        }

        // PUT: api/ficha/salvar-estrutura (Grava a árvore JSON dinâmica enviada pelo Front-end)
        [HttpPut("salvar-estrutura")]
        public async Task<IActionResult> SalvarEstrutura([FromBody] SalvarEstruturaDto dto)
        {
            var ficha = await _context.Fichas.FindAsync(dto.FichaId);

            if (ficha == null)
                return NotFound(new { sucesso = false, mensagem = "Ficha não encontrada." });

            ficha.EstruturaJson = dto.EstruturaJson;
            ficha.DataAtualizacao = DateTime.UtcNow;

            _context.Fichas.Update(ficha);
            await _context.SaveChangesAsync();

            return Ok(new { sucesso = true, mensagem = "Estrutura da ficha atualizada com sucesso!" });
        }
    }
}