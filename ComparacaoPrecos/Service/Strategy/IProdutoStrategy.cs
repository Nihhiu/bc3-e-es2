using ComparacaoPrecos.Models;

public interface IProdutoStrategy
{
    Task<ProdutoViewModel?> Build(int produtoId);
}