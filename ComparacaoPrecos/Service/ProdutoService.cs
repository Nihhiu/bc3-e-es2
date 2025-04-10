using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComparacaoPrecos.Service;

public class ProdutoService {

    private readonly ProdutoRepository _produtoRepository;
    private readonly CategoriaRepository _categoriaRepository;
    private readonly ProdutoLojaRepository _produtoLojaRepository;
    private readonly LojaRepository _lojaRepository;

    public ProdutoService(ProdutoRepository produtoRepository, 
                          CategoriaRepository categoriaRepository, 
                          ProdutoLojaRepository produtoLojaRepository, 
                          LojaRepository lojaRepository) {
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


    // Listar o formulário de criação de produto
    public async Task<ProdutoViewModel> GetProdutoCreateViewModel()
    {
        var categorias = await _categoriaRepository.GetAllCategorias();

        return new ProdutoViewModel
        {
            Categorias = categorias
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriaID,
                    Text = c.CategoriaID
                }).ToList()
        };
    }

    // Recarregar categorias no formulário de criação de produto
    public async Task<ProdutoViewModel> RecarregarCategorias(ProdutoViewModel model)
    {
        var categorias = await _categoriaRepository.GetAllCategorias();
        model.Categorias = categorias
            .Select(c => new SelectListItem
            {
                Value = c.CategoriaID,
                Text = c.CategoriaID
            }).ToList();

        return model;
    }

    // Criar produto
    public async Task CriarProdutoAsync(ProdutoViewModel model)
    {
        var produto = new Produto
        {
            Nome = model.Nome,
            CategoriaID = model.CategoriaID
        };

        await _produtoRepository.AddProduto(produto);
    }
    
}
