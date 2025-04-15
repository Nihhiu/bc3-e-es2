using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DefaultProdutoStrategy : IProdutoStrategy
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly CategoriaRepository _categoriaRepository;
    private readonly ProdutoLojaRepository _produtoLojaRepository;

    public DefaultProdutoStrategy(ProdutoRepository produtoRepository,
                                  CategoriaRepository categoriaRepository,
                                  ProdutoLojaRepository produtoLojaRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _produtoLojaRepository = produtoLojaRepository;
    }

    public async Task<ProdutoViewModel?> Build(int id)
    {
        var produto = await _produtoRepository.GetProdutoById(id);
        if (produto == null) return null;

        var produtoLoja = await _produtoLojaRepository.GetProdutoLojaByProduto(id);
        var categorias = await _categoriaRepository.GetAllCategorias();

        return new ProdutoViewModel
        {
            Nome = produto.Nome,
            CategoriaID = produto.CategoriaID,
            Deleted = produto.Deleted,
            Categorias = categorias
                .Select(c => new SelectListItem { Value = c.CategoriaID, Text = c.CategoriaID })
                .ToList(),
            InfoPorLoja = produtoLoja?.Select(pl => new ProdutoLojaViewModel
            {
                NomeLoja = pl.Loja.Nome,
                Preco = pl.preco
            }).ToList() ?? new List<ProdutoLojaViewModel>()
        };
    }
}