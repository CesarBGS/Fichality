using System.ComponentModel.DataAnnotations;

namespace Fichality.DTOs
{
    public class CriarFichaDto
    {
        [Required(ErrorMessage = "O nome do personagem é obrigatório.")]
        public string NomePersonagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sistema de RPG é obrigatório.")]
        public string SistemaRpg { get; set; } = string.Empty;

        [Required]
        public int UsuarioId { get; set; }
    }

    public class SalvarEstruturaDto
    {
        [Required]
        public int FichaId { get; set; }

        [Required]
        public string EstruturaJson { get; set; } = "{}";
    }
}