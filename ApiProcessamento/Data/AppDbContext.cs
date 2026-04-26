using ApiProcessamento.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProcessamento.Data
{
    /// <summary>
    /// Contexto do banco de dados para a aplicação SIMI.
    /// Responsável pela persistência dos dados no SQLite.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Tabela que armazena todas as medições dos sensores (Temperatura, Pressão e Umidade).
        /// </summary>
        public DbSet<Medicoes> Medicoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garante que a tabela tenha uma chave primária autoincrement
            modelBuilder.Entity<Medicoes>().HasKey(m => m.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}