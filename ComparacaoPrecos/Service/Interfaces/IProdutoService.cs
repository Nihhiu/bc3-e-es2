using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Service.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetAllProdutos();
    Task<Produto?> GetProdutoById(int id);
    Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id);
    Task CriarProdutoAsync(Produto produto);
    Task<List<Produto>> GetProdutosPorCategoria(string categoriaId);
    Task<List<ProdutoViewModel>> GetProdutosPorLoja(int lojaId);
    Task AddProdutoLoja(Produto_Loja produtoLoja);
    Task<Produto_Loja?> GetProdutoLojaAsync(int produtoId, int lojaId);
    Task<bool> UpdateProdutoAsync(Produto produto);
    Task<bool> SoftDeleteProdutoAsync(int id);
    Task AddLogProduto(string userId, string tipo, string? nomeProduto);
    Task AddLogPreco(string userId, string tipo, string? nomeLoja, string? nomeProduto, double? preco);
}