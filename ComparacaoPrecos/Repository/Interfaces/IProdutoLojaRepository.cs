namespace ComparacaoPrecos.Repository.Interfaces;

public interface IProdutoLojaRepository
{
    public Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja);
    public Task<List<Produto_Loja>> GetAllProdutosLojas();
    public Task<List<Produto_Loja>> GetProdutoLojaByProduto(int produtoId);
    public Task<List<Produto_Loja>> GetProdutoLojaByLoja(int lojaId);
    public Task<Produto_Loja> GetProdutoLojaAsync(int produto, int LojaId);
    public Task<bool> SoftDeleteProdutoLojaAsync(int lojaId, int produtoId);
}