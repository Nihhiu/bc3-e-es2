using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Service.Interfaces
{
    public interface ILojaService
    {
        Task<IEnumerable<Produto_Loja>> GetProdutoLojaByLoja(int lojaId);
        Task<Loja> AddLoja(Loja loja);
        Task<List<Loja>> GetAllLojas();
        Task<Loja> GetLojaById(int id);
        Task<List<ProdutoViewModel>> GetProdutosDaLoja(int id);
        Task<bool> UpdateLojaAsync(Loja loja);
    }
}