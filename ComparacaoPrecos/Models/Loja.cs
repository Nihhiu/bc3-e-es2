using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Loja{
    [Key]
    public int LojaID {get;set;}
    [Required][MaxLength(100)]
    public string Nome {get;set;}
    [Required][MaxLength(100)]
    public string Localizacao {get;set;}
    public Boolean Deleted {get;set;}=false;
}