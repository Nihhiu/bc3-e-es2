using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class ProdutoLojaService {
    private readonly ProdutoLojaRepository _produtoLojaRepository;

    public ProdutoLojaService(ProdutoLojaRepository produtoLojaRepository)
    {
        _produtoLojaRepository = produtoLojaRepository;
    }

    
    public async Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas()
    {
        return await _produtoLojaRepository.GetAllProdutosLojas();
    }

    public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByProduto(int id)
    {
        return await _produtoLojaRepository.GetProdutoLojaByProduto(id);
    }
}