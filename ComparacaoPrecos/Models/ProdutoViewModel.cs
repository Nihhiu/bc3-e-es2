using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ComparacaoPrecos.Models;

public class ProdutoViewModel
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    public string CategoriaID { get; set; }

    public List<SelectListItem> Categorias { get; set; } = new List<SelectListItem>();

    public bool Deleted { get; set; } = false;

    public List<ProdutoLojaViewModel> InfoPorLoja { get; set; } = new List<ProdutoLojaViewModel>();
    
}

public class ProdutoLojaViewModel
{
    [Required]
    public string NomeLoja { get; set; }
    
    [Required]
    public double Preco { get; set; }
}
