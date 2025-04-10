public interface IProdutoLojaRepository{
    Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja);
    Task<List<Produto_Loja>> GetAllProdutosLojas();
    Task<List<Produto_Loja>> GetProdutoLojaByProduto(int id);
}