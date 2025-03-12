using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Categoria 
{
    [Key]
    [MaxLength(100)]
    public string CategoriaID {get;set;}
    public Boolean Deleted {get;set;}=false;
}