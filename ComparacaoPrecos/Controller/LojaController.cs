using Microsoft.AspNetCore.Mvc;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Service.Interfaces;
using System.Security.Claims;

namespace ComparacaoPrecos.Controllers;

[Route("loja")]
public class LojaController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ILojaService _lojaService;

    public LojaController(ILojaService lojaService)
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

    [HttpPost("{lojaId}/delete-produto/{produtoId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduto(int lojaId, int produtoId)
    {
        if (User.Identity == null || produtoId == 0 || lojaId == 0)
        {
            TempData["ErrorMessage"] = "Dados inválidos.";
            return RedirectToAction(nameof(Index));
        }

        if (User.IsInRole("Admin") == false)
        {
            TempData["ErrorMessage"] = "Apenas utilizadores com o papel de Admin podem eliminar produtos.";
            return RedirectToAction(nameof(Index));
        }

        var loja = await _lojaService.GetLojaById(lojaId);
        var produto = await _lojaService.GetProdutoById(produtoId);

        await _lojaService.SoftDeleteProdutodaLojaAsync(lojaId, produtoId);

        
        TempData["SuccessMessage"] = "Utilizador eliminado com sucesso.";
        await _lojaService.AddDeletePrecoLog(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, loja.Nome, produto.Nome);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id}")]
    public async Task<IActionResult> Editar(int id)
    {
        var loja = await _lojaService.GetLojaById(id);
        return View(loja);
    }

    [HttpPost("editar/{id}")]
    public async Task<IActionResult> Editar(int id, Loja loja)
    {
        if (id != loja.LojaID)
        {
            return BadRequest("ID da loja não corresponde.");
        }

        if (!ModelState.IsValid)
        {
            return View(loja);
        }

        await _lojaService.UpdateLojaAsync(loja);
        TempData["SuccessMessage"] = "Loja atualizada com sucesso.";

        await _lojaService.AddLojaLog(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, "Editar" , loja.Nome, loja.LojaID);
        return RedirectToAction(nameof(Index));
    }
}