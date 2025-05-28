using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class ProdutoLojaService
{
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