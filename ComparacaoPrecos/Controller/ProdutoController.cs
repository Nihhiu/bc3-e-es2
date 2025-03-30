using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controller;

[Route("produto")]
public class ProdutoController : Microsoft.AspNetCore.Mvc.Controller {
    private readonly ProdutoService _produtoService;
    private readonly CategoriaService _categoriaService;

    public ProdutoController(ProdutoService produtoService, CategoriaService categoriaService) {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
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

        return View(produto);
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