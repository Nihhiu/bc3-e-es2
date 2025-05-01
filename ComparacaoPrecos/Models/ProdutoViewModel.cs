using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ComparacaoPrecos.Models;

public class ProdutoViewModel
{
    [Required]
    public Produto Produto { get; set; } = new Produto();

    public List<ProdutoLojaViewModel> InfoPorLoja { get; set; } = new List<ProdutoLojaViewModel>();
    
}

public class ProdutoLojaViewModel
{
    [Required]
    public string NomeLoja { get; set; }
    
    [Required]
    public double Preco { get; set; }
    
    [Required]
    public DateTime DataHora { get; set; } 
}
