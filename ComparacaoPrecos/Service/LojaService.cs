using ComparacaoPrecos.Data;
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
}