using ComparacaoPrecos.Data;
using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        var produto = await _produtoService.GetProdutoById(id);
        if (produto == null) return NotFound();

        return View(produto);
    }
}