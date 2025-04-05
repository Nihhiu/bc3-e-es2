using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { CategoriaID = "Cosméticos", Deleted = false },
            new Categoria { CategoriaID = "Bebidas", Deleted = false },
            new Categoria { CategoriaID = "Laticínios", Deleted = false },
            new Categoria { CategoriaID = "Carnes", Deleted = false },
            new Categoria { CategoriaID = "Peixes", Deleted = false },
            new Categoria { CategoriaID = "Produtos de Limpeza", Deleted = false },
            new Categoria { CategoriaID = "Higiene Pessoal", Deleted = false },
            new Categoria { CategoriaID = "Papelaria", Deleted = false },
            new Categoria { CategoriaID = "Brinquedos", Deleted = false },
            new Categoria { CategoriaID = "Automóveis e Acessórios", Deleted = false },
            new Categoria { CategoriaID = "Móveis", Deleted = false },
            new Categoria { CategoriaID = "Decoração", Deleted = false },
            new Categoria { CategoriaID = "Esportes e Fitness", Deleted = false },
            new Categoria { CategoriaID = "Ferramentas", Deleted = false },
            new Categoria { CategoriaID = "Iluminação", Deleted = false },
            new Categoria { CategoriaID = "Pet Shop", Deleted = false },
            new Categoria { CategoriaID = "Instrumentos Musicais", Deleted = false },
            new Categoria { CategoriaID = "Relógios e Joias", Deleted = false },
            new Categoria { CategoriaID = "Material de Construção", Deleted = false },
            new Categoria { CategoriaID = "Eletrodomésticos", Deleted = false },
            new Categoria { CategoriaID = "Saúde e Bem-Estar", Deleted = false },
            new Categoria { CategoriaID = "Artesanato", Deleted = false },
            new Categoria { CategoriaID = "Games", Deleted = false },
            new Categoria { CategoriaID = "Produtos Naturais", Deleted = false }
        );

        modelBuilder.Entity<Loja>().HasData(
            new Loja
            {
                LojaID = 1,
                Nome = "Pingo Doce",
                Latitude = 41.7030419,
                Longitude = -8.8216179,
                Deleted = false
            },
            new Loja
            {
                LojaID = 2,
                Nome = "Lidl",
                Latitude = 41.7036984,
                Longitude = -8.824256,
                Deleted = false
            },
            new Loja
            {
                LojaID = 3,
                Nome = "Continente",
                Latitude = 41.7004442,
                Longitude = -8.8391287,
                Deleted = false
            },
            new Loja
            {
                LojaID = 4,
                Nome = "Mercadona",
                Latitude = 41.7070321,
                Longitude = -8.844206,
                Deleted = false
            }
        );
    }
}