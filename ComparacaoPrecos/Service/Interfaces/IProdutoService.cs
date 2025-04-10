using ComparacaoPrecos.Models;
public interface IProdutoService {
    Task<IEnumerable<Produto>> GetAllProdutos();
    Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id);
    Task<ProdutoViewModel> GetProdutoCreateViewModel();
    Task<ProdutoViewModel> RecarregarCategorias(ProdutoViewModel model);
    Task CriarProdutoAsync(ProdutoViewModel model);
}