using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Sqlite;
using ProdutosModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
// using Microsoft.EntityFrameworkCore.Utilities;


namespace Database
{
    public class DatabaseProdutos : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        
        public DatabaseProdutos(DbContextOptions<DatabaseProdutos> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converte = new ValueConverter<List<string>?, string>(elemento => string.Join(";", elemento ?? 
                new List<string>()), elemento => elemento?.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()?? new List<string>());
            
            modelBuilder.Entity<Produto>().Property(p => p.Fotos).HasConversion(converte);
            
        }
        
    }
}

