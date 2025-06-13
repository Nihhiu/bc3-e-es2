using ComparacaoPrecos.Service; // Add this line if ProdutoService is in this namespace

public interface IProdutoServiceWrapper
{
    Task<Produto_Loja> GetProdutoLojaAsync(int produtoId, int lojaId);
}

public class ProdutoServiceWrapper : IProdutoServiceWrapper
{
    private readonly ProdutoService _produtoService;

    public ProdutoServiceWrapper(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<Produto_Loja> GetProdutoLojaAsync(int produtoId, int lojaId)
    {
        var result = await _produtoService.GetProdutoLojaAsync(produtoId, lojaId);
        if (result == null)
        {
            throw new InvalidOperationException("Produto_Loja not found.");
        }
        return result;
    }
}