using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class LojaService
{
    private readonly LojaRepository _lojaRepository;

    public LojaService(LojaRepository lojaRepository)
    {
        _lojaRepository = lojaRepository;
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