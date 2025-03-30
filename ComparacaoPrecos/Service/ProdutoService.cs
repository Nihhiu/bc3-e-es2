using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class ProdutoService {

    private readonly ProdutoRepository _produtoRepository;

    public ProdutoService(ProdutoRepository produtoRepository) {
        _produtoRepository = produtoRepository;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<IEnumerable<Produto>> GetAllProdutos()
    {
        return await _produtoRepository.GetAllProdutos();
    }

    // Buscar produto por id que não estão deletados
    public async Task<Produto> GetProdutoById(int id)
    {
        return await _produtoRepository.GetProdutoById(id);
    }

    // Criar um novo produto
    public async Task<Produto> AddProduto(Produto produto)
    {
        return await _produtoRepository.AddProduto(produto);
    }

    // Atualizar produto
    public async Task<Produto> UpdateProduto(Produto produto)
    {
        return await _produtoRepository.UpdateProduto(produto);
    }

    // Deletar produto (marcar como deletado)
    public async Task<bool> DeleteProduto(int id)
    {
        return await _produtoRepository.DeleteProduto(id);
    }
    
}
