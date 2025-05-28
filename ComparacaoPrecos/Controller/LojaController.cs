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
        var viewModel = await _lojaService.GetLojaById(id);
        if (viewModel == null) return NotFound();

        return View(viewModel);
    }
    [HttpGet("{lojaId}/produtos")]
    public async Task<IActionResult> ProdutosPorLoja(int lojaId)
    {
        if (!User.IsInRole("Admin"))
        {
            return Unauthorized(); // Or RedirectToAction("Index", "Home");
        }

        var produtos = await _lojaService.GetProdutoLojaByLoja(lojaId);
        return View(produtos); // Or return Json(produtos);
    }

}