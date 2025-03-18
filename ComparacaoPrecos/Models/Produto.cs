using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Produto 
{
    [Key]
    public int ProdutoID {get;set;}
    [MaxLength(100)][Required]
    public string Nome {get;set;}
    [Required]
    public int CategoriaID {get;set;}
    [ForeignKey("CategoriaID")]
    public Categoria Categoria {get;set;}
    [Required]
    public Boolean Deleted {get;set;}=false;
    
}