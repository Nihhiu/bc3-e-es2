namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface IProdutoReader
    {
        Task<List<Produto>> GetAllProdutos();
        Task<Produto> GetProdutoById(int id);
    }
}