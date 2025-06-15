namespace ComparacaoPrecos.Repository.Interfaces;

public interface ILojaRepository
{
    Task<Loja> AddLoja(Loja loja);
    Task<List<Loja>> GetAllLojas();
    Task<Loja> GetLojaById(int id);
    Task<bool> UpdateLoja(Loja loja);
}