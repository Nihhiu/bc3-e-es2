using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Strategy.Concrete;

public class LojaStrategy : IProdutoLojaStrategy
{
    private readonly ProdutoLojaRepository _produtoLojaRepository;

    public LojaStrategy(ProdutoLojaRepository produtoLojaRepository)
    {
        _produtoLojaRepository = produtoLojaRepository;
    }

    public string LojaNome => "Loja A";

    public async Task<ProdutoViewModel> Build(int produtoId)
    {
        var produtos = await _produtoLojaRepository.GetProdutoLojaByProduto(produtoId);

        return new ProdutoViewModel
        {
            Nome = "Produto Loja A",
            InfoPorLoja = produtos
                .Where(p => p.Loja.Nome == LojaNome)
                .Select(p => new ProdutoLojaViewModel
                {
                    NomeLoja = LojaNome,
                    Preco = p.preco
                }).ToList()
        };
    }
}