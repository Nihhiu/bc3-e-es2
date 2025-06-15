using ComparacaoPrecos.Data;

namespace ComparacaoPrecos.Repository
{
    public interface ILojaRepository
    {
        Task<Loja> AddLoja(Loja loja);
        Task<List<Loja>> GetAllLojas();
        Task<Loja> GetLojaById(int id);
    }
}
