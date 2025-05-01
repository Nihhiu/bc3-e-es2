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
    private readonly LojaService _lojaService;

    public ProdutoController(ProdutoService produtoService, CategoriaService categoriaService, LojaService lojaService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _lojaService = lojaService;
    }

    // GET: /produto
    [HttpGet("")]
    public async Task<IActionResult> Index(string categoria, int loja, DateTime? dataInicio, DateTime? dataFim)
    {
        var categorias = await _categoriaService.GetAllCategorias();
        var lojas = await _lojaService.GetAllLojas();

        ViewData["Categorias"] = categorias.Select(c => new SelectListItem
        {
            Value = c.CategoriaID,
            Text = c.CategoriaID
        }).ToList();

        ViewData["Lojas"] = lojas.Select(l => new SelectListItem
        {
            Value = l.LojaID.ToString(),
            Text = l.Nome
        }).ToList();

        IEnumerable<Produto> produtos;

        if (loja != 0)
        {
            produtos = await _produtoService.GetProdutosPorLoja(loja);
        }
        else
        {
            produtos = await _produtoService.GetAllProdutos();
        }

        if (!string.IsNullOrEmpty(categoria))
        {
            produtos = produtos.Where(p => p.CategoriaID == categoria).ToList();
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