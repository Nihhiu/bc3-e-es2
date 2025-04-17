using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser
{
}

public class Loja
{
    [Key]
    public int LojaID { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }

    [Required]
    public double Latitude { get; set; } 

    [Required]
    public double Longitude { get; set; }
    
    public bool Deleted { get; set; } = false;
}

public class Categoria
{
    [Key]
    [MaxLength(100)]
    public string CategoriaID { get; set; }
    
    public bool Deleted { get; set; } = false;
}

public class Produto
{
    [Key]
    public int ProdutoID { get; set; }
    [MaxLength(100)]
    [Required]
    public string Nome { get; set; }
    [Required]
    public string CategoriaID { get; set; }
    [ForeignKey("CategoriaID")]
    public Categoria Categoria { get; set; }
    [Required]
    public bool Deleted { get; set; } = false;

}

public class Produto_Loja
{
    public int ProdutoID { get; set; }

    [ForeignKey("ProdutoID")]
    public Produto Produto { get; set; }

    public int LojaID { get; set; }

    [ForeignKey("LojaID")]
    public Loja Loja { get; set; }

    public DateTime DataHora { get; set; } = DateTime.Now;

    [Required]
    public double preco { get; set; }

    [Required][Range(0, 5)]
    public int credibilidade { get; set; } = 5; 

    [Required]
    public string Id { get; set; }

    [ForeignKey("Id")]
    public ApplicationUser ApplicationUser { get; set; }
    
    public bool Deleted { get; set; } = false;
}