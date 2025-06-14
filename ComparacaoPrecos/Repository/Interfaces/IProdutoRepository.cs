namespace ComparacaoPrecos.Repository.Interfaces;

public interface IProdutoRepository
{
    public Task<Produto> AddProduto(Produto produto);
    public Task<List<Produto>> GetAllProdutos();
    public Task<Produto> GetProdutoById(int id);
    public Task<Produto> UpdateProduto(Produto produto);
    public Task<bool> DeleteProduto(int id);
}