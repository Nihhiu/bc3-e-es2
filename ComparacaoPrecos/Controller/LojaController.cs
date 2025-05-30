using Microsoft.AspNetCore.Mvc;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controllers;

[Route("loja")]
public class LojaController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly LojaService _lojaService;

    public LojaController(LojaService lojaService)
    {
        _lojaService = lojaService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var lojas = await _lojaService.GetAllLojas();
        return View(lojas);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(int id)
    {
        // Busca a entidade loja pura (com latitude/longitude etc)
        var loja = await _lojaService.GetLojaById(id);
        if (loja == null) return NotFound();
            
        ViewData["IsAdmin"] = User.Identity.IsAuthenticated && User.IsInRole("Admin");

        if (User.IsInRole("Admin"))
        {
            var produtosDaLoja = await _lojaService.GetProdutosDaLoja(id);

            ViewData["Produtos"] = produtosDaLoja
                .OrderByDescending(p => p.InfoPorLoja.Max(i => i.DataHora))
                .ToList();

        }

        return View(loja);
    }
    

}