using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controller;

[Route("produto")]
public class ProdutoController : Microsoft.AspNetCore.Mvc.Controller {
    private readonly IProdutoFacade _produtoFacade;

    public ProdutoController(IProdutoFacade produtoFacade)
    {
        _produtoFacade = produtoFacade;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoFacade.ObterLista();
        return View(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(int id)
    {
        var viewModel = await _produtoFacade.ObterDetalhes(id);
        if (viewModel == null) return NotFound();

        return View(viewModel);
    }

    [HttpGet("criar")]
    public async Task<IActionResult> Create()
    {
        var viewModel = await _produtoFacade.CriacaoViewModel();
        return View(viewModel);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Create(ProdutoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = await _produtoFacade.RecarregarCategorias(model);
            return View(model);
        }

        await _produtoFacade.CriarProduto(model);
        return RedirectToAction("Index");
    }
}