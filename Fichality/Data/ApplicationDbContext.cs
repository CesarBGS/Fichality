using Fichality.Models;
using Microsoft.EntityFrameworkCore;

namespace Fichality.Data
{
    // Herda de DbContext, que é a classe base do Entity Framework Core
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe as configurações de conexão (String de Conexão) do appsettings.json
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapeamento das tabelas que vão existir no SQL Server
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ficha> Fichas { get; set; }

        // Configuração detalhada de relacionamentos e regras de banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Garante que o e-mail do usuário seja ÚNICO no banco de dados (não permite e-mails duplicados)
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configura o relacionamento 1 para N (Um Usuário tem Muitas Fichas)
            modelBuilder.Entity<Ficha>()
                .HasOne(f => f.Usuario)
                .WithMany(u => u.Fichas)
                .HasForeignKey(f => f.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Se deletar o Usuário, deleta as Fichas dele automaticamente
        }
    }
}