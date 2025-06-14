using ComparacaoPrecos.Models;
using ComparacaoPrecos.Service.Interfaces;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Service;

public class ProdutoService : IProdutoService
{

    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IProdutoLojaRepository _produtoLojaRepository;
    private readonly ILojaRepository _lojaRepository;

    public ProdutoService(IProdutoRepository produtoRepository,
                          ICategoriaRepository categoriaRepository,
                          IProdutoLojaRepository produtoLojaRepository,
                          ILojaRepository lojaRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _produtoLojaRepository = produtoLojaRepository;
        _lojaRepository = lojaRepository;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<IEnumerable<ProdutoViewModel>> GetAllProdutos()
    {
        var produtos = await _produtoRepository.GetAllProdutos();
        var produtosLoja = await _produtoLojaRepository.GetAllProdutosLojas();

        var result = produtos
            .Select(produto =>
            {
                var infoPorLoja = produtosLoja
                    .Where(pl => pl.ProdutoID == produto.ProdutoID)
                    .Select(pl => new ProdutoLojaViewModel
                    {
                        LojaID = pl.LojaID,
                        NomeLoja = pl.Loja.Nome,
                        Preco = pl.preco,
                        DataHora = pl.DataHora
                    })
                    .ToList();

                return new ProdutoViewModel
                {
                    Produto = produto,
                    InfoPorLoja = infoPorLoja
                };
            })
            .ToList();

        return result;
    }

    // Buscar produto por id que não estão deletados
    public async Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id)
    {
        var produto = await _produtoRepository.GetProdutoById(id);
        if (produto == null) return null;

        var produtoLoja = await _produtoLojaRepository.GetProdutoLojaByProduto(id);

        return new ProdutoViewModel
        {
            Produto = produto,
            InfoPorLoja = produtoLoja?.Select(pl => new ProdutoLojaViewModel
            {
                LojaID = pl.Loja.LojaID,
                NomeLoja = pl.Loja.Nome,
                Preco = pl.preco,
                DataHora = pl.DataHora
            }).ToList() ?? new List<ProdutoLojaViewModel>()
        };
    }

    // Criar produto
    public async Task CriarProdutoAsync(Produto produto)
    {
        await _produtoRepository.AddProduto(produto);
    }

    // Buscar produtos por categoria
    public async Task<List<Produto>> GetProdutosPorCategoria(string categoriaId)
    {
        var produtos = await _produtoRepository.GetAllProdutos();
        return produtos.Where(p => p.CategoriaID == categoriaId).ToList();
    }

    public async Task<List<ProdutoViewModel>> GetProdutosPorLoja(int LojaID)
    {
        var produtos = await _produtoRepository.GetAllProdutos();
        var produtosLoja = await _produtoLojaRepository.GetProdutoLojaByLoja(LojaID);

        var result = produtos
    .Where(produto => produtosLoja.Any(pl => pl.ProdutoID == produto.ProdutoID)) 
    .Select(produto =>
    {
        var infoPorLoja = produtosLoja
            .Where(pl => pl.ProdutoID == produto.ProdutoID)
            .Select(pl => new ProdutoLojaViewModel
            {
                LojaID = pl.Loja.LojaID,
                NomeLoja = pl.Loja.Nome,
                Preco = pl.preco,
                DataHora = pl.DataHora
            }).ToList();

        return new ProdutoViewModel
        {
            Produto = produto,
            InfoPorLoja = infoPorLoja
        };
    }).ToList();

        return result;
    }

    public async Task AddProdutoLoja(Produto_Loja produtoLoja)
    {
        await _produtoLojaRepository.AddProdutoLoja(produtoLoja);
    }

    public async Task<Produto_Loja?> GetProdutoLojaAsync(int ProdutoID, int LojaID)
    {
        return await _produtoLojaRepository.GetProdutoLojaAsync(ProdutoID, LojaID);
    }

}