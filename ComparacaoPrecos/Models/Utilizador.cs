using System.ComponentModel.DataAnnotations;

public class Utilizador{
    [Required][MaxLength(100)]
    public string Nome {get;set;}
    [Required]
    public string Tipo {get;set;}
    [Required][MinLength(8)]
    public string Password {get;set;}
    [Required][MaxLength(100)]
    public string Email {get;set;}
    [Key]

    public int UtilizadorID {get;set;}
    public Boolean Deleted {get;set;}=false;
}