using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ComparacaoPrecos.Data.DB;
public class ApplicationUser : IdentityUser
{
    public string Tipo { get; set; } = "User";
}

public class Loja
{
    [Key]
    public int LojaID { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Localizacao { get; set; }
    
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

    public DateTime data { get; set; } = DateTime.Now.Date;
    public TimeSpan hora { get; set; } = DateTime.Now.TimeOfDay;

    [Required]
    public double preco { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey("Id")]
    public ApplicationUser ApplicationUser { get; set; }
    
    public bool Deleted { get; set; } = false;
}  

