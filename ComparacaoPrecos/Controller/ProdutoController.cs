using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controller;

[Route("produto")]
public class ProdutoController : Microsoft.AspNetCore.Mvc.Controller {
    private readonly ProdutoService _produtoService;

    public ProdutoController(ProdutoService produtoService) {
        _produtoService = produtoService;
    }

    // GET: /produto
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.GetAllProdutos();
        return View(produtos);
    }

    // GET: /produto/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(int id)
    {
        var viewModel = await _produtoService.GetProdutoDetalhesViewModel(id);
        if (viewModel == null) 
        return NotFound();

        return View(viewModel);
    }


    [HttpGet("criar")]
    public async Task<IActionResult> Create()
    {
        var viewModel = await _produtoService.GetProdutoCreateViewModel();
        return View(viewModel);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Create(ProdutoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model = await _produtoService.RecarregarCategorias(model);
            return View(model);
        }

        await _produtoService.CriarProdutoAsync(model);
        return RedirectToAction("Index");
    }
}