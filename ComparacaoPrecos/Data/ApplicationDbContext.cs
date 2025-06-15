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
    public DbSet<Logs> Logs { get; set; }

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
                Latitude = 41.7027114,
                Longitude = -8.8167361,
                Deleted = false
            },
            new Loja
            {
                LojaID = 2,
                Nome = "Lidl",
                Latitude = 41.7063573,
                Longitude = -8.8200369,
                Deleted = false
            },
            new Loja
            {
                LojaID = 3,
                Nome = "Continente",
                Latitude = 41.7043917,
                Longitude = -8.8152299,
                Deleted = false
            },
            new Loja
            {
                LojaID = 4,
                Nome = "Mercadona",
                Latitude = 41.7070973,
                Longitude = -8.8255711,
                Deleted = false
            },
            new Loja
            {
                LojaID = 5,
                Nome = "Worten",
                Latitude = 41.6949737,
                Longitude = -8.8331629,
                Deleted = false
            },
            new Loja
            {
                LojaID = 6,
                Nome = "Dimacer",
                Latitude = 41.7031532,
                Longitude = -8.8243453,
                Deleted = false
            },
            new Loja
            {
                LojaID = 7,
                Nome = "ALDI",
                Latitude = 41.695014,
                Longitude = -8.8439396,
                Deleted = false
            },
            new Loja
            {
                LojaID = 8,
                Nome = "Toyota-Macedo & Macedo",
                Latitude = 41.6988158,
                Longitude = -8.8464872,
                Deleted = false
            }
        );
    }
}