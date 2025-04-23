using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComparacaoPrecos.Service;

public class ProdutoService
{

    private readonly ProdutoRepository _produtoRepository;
    private readonly CategoriaRepository _categoriaRepository;
    private readonly ProdutoLojaRepository _produtoLojaRepository;
    private readonly LojaRepository _lojaRepository;

    public ProdutoService(ProdutoRepository produtoRepository,
                          CategoriaRepository categoriaRepository,
                          ProdutoLojaRepository produtoLojaRepository,
                          LojaRepository lojaRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _produtoLojaRepository = produtoLojaRepository;
        _lojaRepository = lojaRepository;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<IEnumerable<Produto>> GetAllProdutos()
    {
        return await _produtoRepository.GetAllProdutos();
    }

    // Buscar produto por id que não estão deletados
    public async Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id)
    {
        var produto = await _produtoRepository.GetProdutoById(id);
        if (produto == null) return null;

        var produtoLoja = await _produtoLojaRepository.GetProdutoLojaByProduto(id);

        return new ProdutoViewModel
        {
            Produto = produto,
            InfoPorLoja = produtoLoja?.Select(pl => new ProdutoLojaViewModel
            {
                NomeLoja = pl.Loja.Nome,
                Preco = pl.preco
            }).ToList() ?? new List<ProdutoLojaViewModel>()
        };
    }

    // Criar produto
    public async Task CriarProdutoAsync(Produto produto)
    {
        await _produtoRepository.AddProduto(produto);
    }

    // Buscar produtos por categoria
    public async Task<List<Produto>> GetProdutosPorCategoria(string categoriaId)
{
    var produtos = await _produtoRepository.GetAllProdutos();
    return produtos.Where(p => p.CategoriaID == categoriaId).ToList();
}
}
