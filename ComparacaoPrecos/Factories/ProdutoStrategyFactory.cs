using ComparacaoPrecos.Strategies;

namespace ComparacaoPrecos.Factories
{
    public class ProdutoStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProdutoStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProdutoLojaStrategy GetStrategyByLoja(string lojaNome)
        {
            return lojaNome switch
            {
                "Loja A" => _serviceProvider.GetRequiredService<LojaAStrategy>(),
                "Loja B" => _serviceProvider.GetRequiredService<LojaBStrategy>(),
                _ => throw new NotImplementedException($"Strategy for '{lojaNome}' not implemented.")
            };
        }
    }
}