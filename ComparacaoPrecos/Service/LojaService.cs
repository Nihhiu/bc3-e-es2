using ComparacaoPrecos.Data;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class LojaService
{
    private readonly LojaRepository _lojaRepository;
    private readonly ProdutoLojaRepository _produtoLojaRepository;

    public LojaService(LojaRepository lojaRepository, ProdutoLojaRepository produtoLojaRepository)
    {
        _lojaRepository = lojaRepository;
        _produtoLojaRepository = produtoLojaRepository;
    }

    public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByLoja(int lojaId)
    {
        return await _produtoLojaRepository.GetProdutoLojaByLoja(lojaId);
    }

    // Criar uma nova loja
    public async Task<Loja> AddLoja(Loja loja)
    {
        return await _lojaRepository.AddLoja(loja);
    }

    // Buscar todas as lojas que não estão deletadas
    public async Task<List<Loja>> GetAllLojas()
    {
        return await _lojaRepository.GetAllLojas();
    }

    // Buscar loja por ID que não está deletada
    public async Task<Loja> GetLojaById(int id)
    {
        return await _lojaRepository.GetLojaById(id);
    }

    // Buscar dados dos produtos de uma loja específica e preços
    public async Task<List<ProdutoViewModel>> GetProdutosDaLoja(int id)
    {
        var produtosLoja = await _produtoLojaRepository.GetProdutoLojaByLoja(id);
        
        Console.WriteLine($"Produtos encontrados para a loja {id}: {produtosLoja.Count}");
        
        return produtosLoja
            .GroupBy(pl => pl.Produto)
            .Select(g => new ProdutoViewModel
            {
                Produto = g.Key,

                InfoPorLoja = g.Select(pl => new ProdutoLojaViewModel
                {
                    LojaID = id,
                    NomeLoja = pl.Loja.Nome,
                    Preco = (double)pl.preco,
                    DataHora = pl.DataHora,
                }).ToList()
            })
            .ToList();
    }

}