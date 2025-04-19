namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface ILojaWriter
    {
        Task<Loja> AddLoja(Loja loja);
    }
}