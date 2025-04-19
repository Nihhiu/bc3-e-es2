namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface ILojaReader
    {
        Task<List<Loja>> GetAllLojas();
        Task<Loja> GetLojaById(int id);
    }
}