using ComparacaoPrecos.Data;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Service.Interfaces;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Service;

public class LojaService : ILojaService
{
    private readonly ILojaRepository _lojaRepository;
    private readonly IProdutoLojaRepository _produtoLojaRepository;
    private readonly ILogsRepository _logsRepository;
    private readonly IProdutoRepository _produtoRepository;

    public LojaService(ILojaRepository lojaRepository, IProdutoLojaRepository produtoLojaRepository, ILogsRepository logsRepository, IProdutoRepository produtoRepository)
    {
        _lojaRepository = lojaRepository;
        _produtoLojaRepository = produtoLojaRepository;
        _logsRepository = logsRepository;
        _produtoRepository = produtoRepository;
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
    public async Task<bool> UpdateLojaAsync(Loja loja)
    {
        try
        {
            await _lojaRepository.GetLojaById(loja.LojaID);
            return await _lojaRepository.UpdateLoja(loja);
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating Loja with ID {loja.LojaID}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SoftDeleteProdutodaLojaAsync(int lojaId, int produtoId)
    {
        return await _produtoLojaRepository.SoftDeleteProdutoLojaAsync(lojaId, produtoId);
    }

    public async Task<Produto> GetProdutoById(int id)
    {
        return await _produtoRepository.GetProdutoById(id);
    }

    public async Task AddDeletePrecoLog(string userId, string nomeLoja, string nomeProduto)
    {
        string mensagem = "Removeu o Produto <" + nomeProduto + "> do catálogo da loja <" + nomeLoja + ">";
        await _logsRepository.AddLog(userId, mensagem);
    }

    public async Task AddLojaLog(string userId, string tipo , string nomeLoja, int? lojaId)
    {
        string mensagem;
        switch (tipo)
        {
            case "Criar":
                mensagem = "Adicionou a Loja de ID <" + lojaId + "> com o nome <" + nomeLoja + "> ao catálogo";
                break;
            case "Editar":
                mensagem = "Atualizou a Loja de ID <" + lojaId + "> para <" + nomeLoja + "> no catálogo";
                break;
            default:
                mensagem = "Ação desconhecida para a Loja <" + nomeLoja + ">";
                break;
        }
        await _logsRepository.AddLog(userId, mensagem);
    }
}