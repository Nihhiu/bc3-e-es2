using ComparacaoPrecos.Models;
public interface IProdutoFacade
{
    Task<IEnumerable<Produto>> ObterLista();
    Task<ProdutoViewModel?> ObterDetalhes(int id);
    Task<ProdutoViewModel> CriacaoViewModel();
    Task<ProdutoViewModel> RecarregarCategorias(ProdutoViewModel model);
    Task CriarProduto(ProdutoViewModel model);
}
