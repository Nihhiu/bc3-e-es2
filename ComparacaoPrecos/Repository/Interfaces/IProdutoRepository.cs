public interface IProdutoRepository{
    Task<Produto> GetProdutoById(int id);
    Task<List<Produto>> GetAllProdutos();
    Task<Produto> AddProduto(Produto produto);
    Task<Produto> UpdateProduto(Produto produto);
    Task<bool> DeleteProduto(int id);
}