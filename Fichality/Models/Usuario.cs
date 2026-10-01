// Using System.ComponentModel.DataAnnotations:
// Permite adicionar validações e restrições aos campos da classe
using System.ComponentModel.DataAnnotations;
// Using System.ComponentModel.DataAnnotations.Schema:
// Permite definir como a classe será mapeada para o banco de dados
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fichality.Models
{
    // [Table("Fichas")]
    // Define que esta classe será mapeada para a tabela "Fichas" no banco de dados
    [Table("Usuarios")]
    public class Usuario
    {
        // [Key] -> Indica que a propriedade 'Id' é a chave primária da tabela
        [Key]
        // [DatabaseGenerated] -> Diz ao banco para gerar o número do ID automaticamente (1, 2, 3...)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(150, ErrorMessage = "O e-mail não pode exceder 150 caracteres.")]
        public string Email { get; set; } = string.Empty;

        // Guarda o HASH da senha (criptografada com BCrypt ou PBKDF2), NUNCA a senha em texto puro
        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        // Define o perfil de acesso: "Jogador", "Mestre" ou "Administrador"
        [Required]
        [StringLength(20)]
        public string Perfil { get; set; } = "Jogador";

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // =========================================================================================
        // RELACIONAMENTO (1 para N):
        // Um usuário pode ter VÁRIAS fichas cadastradas no Fichality.
        // O [JsonIgnore] impede que o C# entre em um loop infinito na hora de transformar em JSON.
        // =========================================================================================
        [JsonIgnore]
        public ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();
    }
}