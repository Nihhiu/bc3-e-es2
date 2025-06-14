using ComparacaoPrecos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparacaoPrecos.Service;
public interface IProdutoService
{

    Task<IEnumerable<ProdutoViewModel>> GetAllProdutos();


    Task<ProdutoViewModel?> GetProdutoDetalhesViewModel(int id);

    Task CriarProdutoAsync(Produto produto);


    Task<List<Produto>> GetProdutosPorCategoria(string categoriaId);

    Task<List<ProdutoViewModel>> GetProdutosPorLoja(int LojaID);

    Task AddProdutoLoja(Produto_Loja produtoLoja);


    Task<Produto_Loja?> GetProdutoLojaAsync(int ProdutoID, int LojaID);
}