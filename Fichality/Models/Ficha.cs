// Using System.ComponentModel.DataAnnotations:
// Permite adicionar validações e restrições aos campos da classe
using System.ComponentModel.DataAnnotations;
// Using System.ComponentModel.DataAnnotations.Schema:
// Permite definir como a classe será mapeada para o banco de dados
using System.ComponentModel.DataAnnotations.Schema;

namespace Fichality.Models
{
    // [Table("Fichas")]
    // Define que esta classe será mapeada para a tabela "Fichas" no banco de dados
    [Table("Fichas")]
    public class Ficha
    {
        // [Key] -> Indica que a propriedade 'Id' é a chave primária da tabela
        [Key]
        // [DatabaseGenerated] -> Diz ao banco para gerar o número do ID automaticamente (1, 2, 3...)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [Required] -> Torna o preenchimento do Nome do Personagem OBRIGATÓRIO
        [Required(ErrorMessage = "O nome do personagem é obrigatório.")]
        // [StringLength(100)] -> Limita o nome a no máximo 100 caracteres no banco
        [StringLength(100, ErrorMessage = "O nome do personagem não pode exceder 100 caracteres.")]
        public string NomePersonagem { get; set; } = string.Empty;

        // [Required] -> Torna o preenchimento do Sistema de RPG OBRIGATÓRIO (ex: D&D 5e, Tormenta20)
        [Required(ErrorMessage = "O sistema de RPG é obrigatório.")]
        [StringLength(50, ErrorMessage = "O sistema de RPG não pode exceder 50 caracteres.")]
        public string SistemaRpg { get; set; } = string.Empty;

        // =========================================================================================
        // AQUI ESTÁ O "PULO DO GATO" DO FICHALITY:
        // [Column(TypeName = "nvarchar(max)")] -> Cria um campo de texto "infinito" no SQL Server.
        // Em vez de criar centenas de tabelas para Força, Agilidade, Magias, etc., nós salvamos 
        // a estrutura inteira montada pelo usuário no JavaScript em um texto formatado como JSON.
        // Exemplo gravado aqui: {"Força": 18, "Perícias": ["Acrobacia", "Furtividade"]}
        // =========================================================================================
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string EstruturaJson { get; set; } = "{}";

        // Salva automaticamente a data e hora em que a ficha foi criada
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Salva a data e hora da última vez que o jogador editou a ficha
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // =========================================================================================
        // RELACIONAMENTO E SEGURANÇA:
        // Guarda o ID do Usuário (Jogador) que criou a ficha. 
        // Isso impede que o "Jogador A" consiga ver ou apagar a ficha do "Jogador B".
        // =========================================================================================
        [Required]
        public int UsuarioId { get; set; }

        // [ForeignKey("UsuarioId")] -> Diz ao Entity Framework que 'UsuarioId' aponta para o objeto 'Usuario'
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}