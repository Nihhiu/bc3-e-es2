namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface IProdutoLojaWriter
    {
        Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja);
    }
}