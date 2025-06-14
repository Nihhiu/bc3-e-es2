using ComparacaoPrecos.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComparacaoPrecos.Models;
using System.Security.Claims;

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
    public async Task<IActionResult> Index(string categoria, int loja, DateTime? dataInicio, DateTime? dataFim, string Nome)
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

        IEnumerable<ProdutoViewModel> produtos;

        if (loja != 0)
        {
            Console.WriteLine(loja);
            produtos = await _produtoService.GetProdutosPorLoja(loja);
            Console.WriteLine(produtos.Count());
        }
        else
        {
            produtos = await _produtoService.GetAllProdutos();
        }

        if (!string.IsNullOrEmpty(categoria))
        {
            produtos = produtos.Where(p => p.Produto.CategoriaID == categoria);
        }

        if (dataInicio.HasValue)
        {
            produtos = produtos.Where(p =>
                p.InfoPorLoja.Any(loja => loja.DataHora >= dataInicio));
        }

        if (dataFim.HasValue)
        {
            produtos = produtos.Where(p =>
                p.InfoPorLoja.Any(loja => loja.DataHora <= dataFim.Value));
        }

        if (!string.IsNullOrWhiteSpace(Nome) && Nome.Length >= 2)
        {
            produtos = produtos.Where(p =>
                p.Produto.Nome != null &&
                p.Produto.Nome.ToLower().Contains(Nome.ToLower()));
        }

        return View(produtos);
    }


    // GET: /produto/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes(ProdutoViewModel model, int id)
    {
        var viewModel = await _produtoService.GetProdutoDetalhesViewModel(id);
        if (viewModel == null) return NotFound();

        foreach (var item in viewModel.InfoPorLoja)
        {
            var dias = (DateTime.UtcNow - item.DataHora).Days;
            item.credibilidade = Math.Clamp(10 - (dias / 30), 0, 10); // 1 ponto por mês
        }

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

    // GET: /produto/add_preco/{id}
    [HttpGet("add_preco/{id}")]
    public async Task<IActionResult> AddPreco(int id)
    {
        var produtoViewModel = await _produtoService.GetProdutoDetalhesViewModel(id);
        if (produtoViewModel == null) return NotFound();

        var lojas = await _lojaService.GetAllLojas();
        ViewData["Lojas"] = lojas.Select(l => new SelectListItem
        {
            Value = l.LojaID.ToString(),
            Text = l.Nome
        }).ToList();

        if (produtoViewModel.InfoPorLoja.Count == 0)
        {
            produtoViewModel.InfoPorLoja.Add(new ProdutoLojaViewModel());
        }

        return View(produtoViewModel);
    }

    // POST: /produto/add_preco/{id}
    [HttpPost("add_preco/{id}")]
    public async Task<IActionResult> AddPreco(ProdutoViewModel model, int id)
    {
        if (User.Identity?.Name == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var lojaId = model.InfoPorLoja[0].LojaID;
        var precoExistente = await _produtoService.GetProdutoLojaAsync(id, lojaId);

        if (precoExistente != null && !Request.Form.ContainsKey("confirmar"))
        {
            return Json(new 
            { 
                requiresConfirmation = true,
                oldPrice = precoExistente.preco.ToString("N2"),
                newPrice = model.InfoPorLoja[0].Preco.ToString("N2")
            });
        }

        var produtoLoja = new Produto_Loja
        {
            ProdutoID = id,
            LojaID = lojaId,
            preco = model.InfoPorLoja[0].Preco,
            DataHora = DateTime.UtcNow,
            Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };

        await _produtoService.AddProdutoLoja(produtoLoja);
        return Json(new { redirectUrl = Url.Action("Index") });
    }
}