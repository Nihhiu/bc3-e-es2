using System.ComponentModel.DataAnnotations;

public class Categoria 
{
    [Key]
    [MaxLength(100)]
    public string CategoriaID {get;set;}
    public Boolean Deleted {get;set;}=false;
}