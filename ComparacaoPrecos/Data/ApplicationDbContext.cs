using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data.DB;

namespace ComparacaoPrecos.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    
    public DbSet<Loja> Loja { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Produto_Loja> Produto_Loja { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Sua configuração adicional, como chaves compostas:
        modelBuilder.Entity<Produto_Loja>()
            .HasKey(pl => new { pl.ProdutoID, pl.LojaID });
    }
}