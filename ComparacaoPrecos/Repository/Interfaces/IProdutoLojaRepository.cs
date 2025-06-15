using ComparacaoPrecos.Data;

namespace ComparacaoPrecos.Repository
{
    public interface IProdutoLojaRepository
    {
        Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja);
        Task<List<Produto_Loja>> GetAllProdutosLojas();
        Task<List<Produto_Loja>> GetProdutoLojaByProduto(int produtoId);
        Task<List<Produto_Loja>> GetProdutoLojaByLoja(int lojaId);
        Task<Produto_Loja?> GetProdutoLojaAsync(int produtoId, int lojaId);
    }
}
