using ComparacaoPrecos.Models;

public interface IProdutoLojaStrategy
{
    Task<ProdutoViewModel> Build(int produtoId);
    string LojaNome { get; }
}