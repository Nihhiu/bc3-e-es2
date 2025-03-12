using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Produto_Loja{
    [Key]
    public int ProdutoID {get;set;}
    [ForeignKey("ProdutoID")]
    public Produto Produto{get;set;}
    [Key]
    public int LojaID {get;set;}
    public DateTime data{get;set;}=DateTime.Now;
    public DateTime hora{get;set;}=DateTime.Now;
    [Required]
    public double preco {get;set;}
    [Required]
    public int UtilizadorID{get;set;}
    [ForeignKey("UtilizadorID")]
    public Utilizador Utilizador{get;set;}
    public Boolean Deleted {get;set;}=false;
}