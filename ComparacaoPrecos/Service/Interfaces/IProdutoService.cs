using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAllProdutos();
        Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id);
        Task CriarProdutoAsync(Produto produto);
        Task<List<Produto>> GetProdutosPorCategoria(string categoriaId);
        Task<List<ProdutoViewModel>> GetProdutosPorLoja(int lojaId);
        Task AddProdutoLoja(Produto_Loja produtoLoja);
        Task<Produto_Loja?> GetProdutoLojaAsync(int produtoId, int lojaId);
        Task<bool> UpdateProdutoAsync(Produto produto);
    }
}