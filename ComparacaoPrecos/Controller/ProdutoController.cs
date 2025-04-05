using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controller;

[Route("produto")]
public class ProdutoController : Microsoft.AspNetCore.Mvc.Controller {
    private readonly ProdutoService _produtoService;
    private readonly CategoriaService _categoriaService;
    private readonly ProdutoLojaService _produtoLojaService;
    private readonly LojaService _lojaService;

    public ProdutoController(ProdutoService produtoService, CategoriaService categoriaService, ProdutoLojaService produtoLojaService, LojaService lojaService) {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _produtoLojaService = produtoLojaService;
        _lojaService = lojaService;
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
        var produto = await _produtoService.GetProdutoById(id);
        if (produto == null) return NotFound();

        var produtoLoja = await _produtoLojaService.GetProdutoLojaByProduto(id);

        var produtoViewModel = new ProdutoViewModel
        {
            Nome = produto.Nome,
            CategoriaID = produto.CategoriaID,
            Deleted = produto.Deleted,
            Categorias = (await _categoriaService.GetAllCategorias())
                .Select(c => new SelectListItem { Value = c.CategoriaID, Text = c.CategoriaID })
                .ToList(),
            InfoPorLoja = produtoLoja != null ? produtoLoja.Select(pl => new ProdutoLojaViewModel
                {
                    NomeLoja = pl.Loja.Nome,
                    Preco = pl.preco
                }).ToList() : new List<ProdutoLojaViewModel>()
        };

        return View(produtoViewModel);
    }

    // GET: /produto/criar (Exibir formulário)
    [HttpGet("criar")]
    public async Task<IActionResult> Create() {
        var categorias = await _categoriaService.GetAllCategorias();

        var viewModel = new ProdutoViewModel
        {
            Categorias = categorias
                .Select(c => new SelectListItem { Value = c.CategoriaID, Text = c.CategoriaID })
                .ToList()
        };

        return View(viewModel);
    }


    // POST: /produto/criar (Salvar produto no BD)
    [HttpPost("criar")]
    public async Task<IActionResult> Create(ProdutoViewModel model) {
        if (!ModelState.IsValid) {
            model.Categorias = (await _categoriaService.GetAllCategorias())
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriaID,
                    Text = c.CategoriaID
                }).ToList();
            return View(model);
        }

        var produto = new Produto
        {
            Nome = model.Nome,
            CategoriaID = model.CategoriaID
        };

        Console.WriteLine($"Produto a ser criado: Nome={produto.Nome}, CategoriaID={produto.CategoriaID}");

        await _produtoService.AddProduto(produto);
        return RedirectToAction("Index");
    }
}