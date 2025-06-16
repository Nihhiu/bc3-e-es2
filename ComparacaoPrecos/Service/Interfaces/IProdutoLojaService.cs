namespace ComparacaoPrecos.Service.Interfaces;
public interface IProdutoLojaService
{
    Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas();
    Task<IEnumerable<Produto_Loja>> GetProdutoLojaByProduto(int id);
    Task<IEnumerable<Produto_Loja>> GetProdutoLojaByLoja(int lojaId);
}