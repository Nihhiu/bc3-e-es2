using ComparacaoPrecos.Models;
public class ProdutoFacade : IProdutoFacade
{
    private readonly IProdutoService _produtoService;

    public ProdutoFacade(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<IEnumerable<Produto>> ObterLista()
        => await _produtoService.GetAllProdutos();

    public async Task<ProdutoViewModel?> ObterDetalhes(int id)
        => await _produtoService.GetProdutoDetalhesViewModel(id);

    public async Task<ProdutoViewModel> CriacaoViewModel()
        => await _produtoService.GetProdutoCreateViewModel();

    public async Task<ProdutoViewModel> RecarregarCategorias(ProdutoViewModel model)
        => await _produtoService.RecarregarCategorias(model);

    public async Task CriarProduto(ProdutoViewModel model)
        => await _produtoService.CriarProdutoAsync(model);
}