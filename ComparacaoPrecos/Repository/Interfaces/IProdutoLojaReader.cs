namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface IProdutoLojaReader
    {
        Task<List<Produto_Loja>> GetAllProdutosLojas();
        Task<List<Produto_Loja>> GetProdutoLojaByProduto(int id);
    }
}