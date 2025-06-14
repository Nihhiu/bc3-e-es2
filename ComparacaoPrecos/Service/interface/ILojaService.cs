using ComparacaoPrecos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparacaoPrecos.Service;
public interface ILojaService
{
    Task<IEnumerable<Produto_Loja>> GetProdutoLojaByLoja(int lojaId);
    Task<Loja> AddLoja(Loja loja);
    Task<List<Loja>> GetAllLojas();
    Task<Loja> GetLojaById(int id);
    Task<List<ProdutoViewModel>> GetProdutosDaLoja(int id);
}
