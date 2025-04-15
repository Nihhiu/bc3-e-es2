public class ProdutoStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ProdutoStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IProdutoStrategy GetStrategyByLoja(string lojaNome)
    {
        return lojaNome.ToLower() switch
        {
            "lojacomdesconto" => _serviceProvider.GetRequiredService<LojaDescontoStrategy>(),
            _ => _serviceProvider.GetRequiredService<DefaultProdutoStrategy>()
        };
    }
}
