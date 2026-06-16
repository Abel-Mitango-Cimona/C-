using Microsoft.EntityFrameworkCore;
using EcommerceModa.Models;

namespace EcommerceModa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dados iniciais de exemplo
            modelBuilder.Entity<Produto>().HasData(
                new Produto
                {
                    Id = 1, Nome = "Vestido Midi Floral",
                    Descricao = "Vestido midi com estampa floral delicada, perfeito para ocasiões especiais.",
                    Preco = 289.90m, Categoria = "Vestidos", Estoque = 15,
                    Tamanhos = "P,M,G,GG", Cor = "Floral Rosê",
                    DataCadastro = new DateTime(2024, 1, 10)
                },
                new Produto
                {
                    Id = 2, Nome = "Blusa de Seda Off-White",
                    Descricao = "Blusa em tecido de seda com caimento elegante e acabamento refinado.",
                    Preco = 159.90m, Categoria = "Blusas", Estoque = 22,
                    Tamanhos = "PP,P,M,G", Cor = "Off-White",
                    DataCadastro = new DateTime(2024, 1, 15)
                },
                new Produto
                {
                    Id = 3, Nome = "Calça Pantalona Bege",
                    Descricao = "Calça pantalona de alfaiataria com cinto incluso, moderna e versátil.",
                    Preco = 219.90m, Categoria = "Calças", Estoque = 8,
                    Tamanhos = "36,38,40,42,44", Cor = "Bege",
                    DataCadastro = new DateTime(2024, 2, 1)
                }
            );
        }
    }
}
