using ComparacaoPrecos.Service.Interfaces;
using ComparacaoPrecos.Repository.Interfaces;
using ComparacaoPrecos.Migrations;

namespace ComparacaoPrecos.Service;

public class ProdutoLojaService : IProdutoLojaService
{
    private readonly IProdutoLojaRepository _produtoLojaRepository;
    private readonly ILogsRepository _logsRepository;

    public ProdutoLojaService(IProdutoLojaRepository produtoLojaRepository, ILogsRepository logsRepository)
    {
        _produtoLojaRepository = produtoLojaRepository;
        _logsRepository = logsRepository;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas()
    {
        return await _produtoLojaRepository.GetAllProdutosLojas();
    }

    // Buscar produto por id que não estão deletados
    public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByProduto(int id)
    {
        return await _produtoLojaRepository.GetProdutoLojaByProduto(id);
    }
    public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByLoja(int lojaId)
    {
        return await _produtoLojaRepository.GetProdutoLojaByLoja(lojaId);
    }

}