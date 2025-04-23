using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controller;

[Route("produto")]
public class ProdutoController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ProdutoService _produtoService;
    private readonly CategoriaService _categoriaService;

    public ProdutoController(ProdutoService produtoService, CategoriaService categoriaService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
    }

    // GET: /produto
    [HttpGet("")]
    public async Task<IActionResult> Index(string categoria)
    {
        var categorias = await _categoriaService.GetAllCategorias();

        ViewData["Categorias"] = categorias.Select(c => new SelectListItem
        {
            Value = c.CategoriaID,
            Text = c.CategoriaID
        }).ToList();

        IEnumerable<Produto> produtos;

        if (!string.IsNullOrEmpty(categoria))
        {
            produtos = await _produtoService.GetProdutosPorCategoria(categoria);
        }
        else
        {
            produtos = await _produtoService.GetAllProdutos();
        }

        return View(produtos);
    }

    // GET: /produto/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(int id)
    {
        var viewModel = await _produtoService.GetProdutoDetalhesViewModel(id);
        if (viewModel == null) return NotFound();

        return View(viewModel);
    }

    // GET: /produto/criar
    [HttpGet("criar")]
    public async Task<IActionResult> Create()
    {
        var produto = new ProdutoViewModel();
        var categorias = await _categoriaService.GetAllCategorias();

        ViewData["Categorias"] = categorias.Select(c => new SelectListItem
        {
            Value = c.CategoriaID,
            Text = c.CategoriaID
        }).ToList();

        return View(produto);
    }

    // POST: /produto/criar
    [HttpPost("criar")]
    public async Task<IActionResult> Create(ProdutoViewModel model)
    {
        await _produtoService.CriarProdutoAsync(model.Produto);
        return RedirectToAction("Index");
    }

}