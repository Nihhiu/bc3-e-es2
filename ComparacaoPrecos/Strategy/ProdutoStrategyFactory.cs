public class ProdutoStrategyFactory
{
    private readonly IEnumerable<IProdutoLojaStrategy> _estrategias;

    public ProdutoStrategyFactory(IEnumerable<IProdutoLojaStrategy> estrategias)
    {
        _estrategias = estrategias;
    }

    public IProdutoLojaStrategy GetStrategyByLoja(string lojaNome)
    {
        var strategy = _estrategias.FirstOrDefault(e => e.LojaNome == lojaNome);

        if (strategy == null)
            throw new ArgumentException($"Nenhuma estratégia implementada para a loja: {lojaNome}");

        return strategy;
    }
}