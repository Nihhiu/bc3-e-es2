using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class ProdutoLojaService {
    private readonly ProdutoLojaRepository _produtoLojaRepository;

    public ProdutoLojaService(ProdutoLojaRepository produtoLojaRepository)
    {
        _produtoLojaRepository = produtoLojaRepository;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas()
    {
        return await _produtoLojaRepository.GetAllProdutosLojas();
    }
}