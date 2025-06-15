using System.Collections.Generic;
using System.Threading.Tasks;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Repository
{
    public interface IProdutoRepository
    {
        Task<Produto> AddProduto(Produto produto);
        Task<List<Produto>> GetAllProdutos();
        Task<Produto> GetProdutoById(int id);
        Task<Produto> UpdateProduto(Produto produto);
        Task<bool> DeleteProduto(int id);
    }
}
