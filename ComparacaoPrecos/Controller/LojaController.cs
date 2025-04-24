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
}