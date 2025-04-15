using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Strategies
{
    public interface IProdutoLojaStrategy
    {
        Task<ProdutoViewModel?> Build(int produtoId);
    }
}