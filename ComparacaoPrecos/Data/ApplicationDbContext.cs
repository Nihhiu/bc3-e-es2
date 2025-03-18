using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComparacaoPrecos.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ensure "User" is the default value for Tipo
        builder.Entity<IdentityUser>()
            .Property<string>("Tipo")
            .HasMaxLength(256)
            .HasColumnType("text")
            .HasDefaultValue("User");
    }
}
/*
using Microsoft.EntityFrameworkCore;


namespace ComparacaoPrecos.DAL
{
    public class ComparacaoPrecosContext : DbContext
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        {
        }
        
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Produto_Loja> Produto_Loja { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

*/