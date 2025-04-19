namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface IProdutoWriter
    {
        Task<Produto> AddProduto(Produto produto);
        Task<Produto> UpdateProduto(Produto produto);
        Task<bool> DeleteProduto(int id);
    }
}