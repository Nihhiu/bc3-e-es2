namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface ICategoriaReader
    {
        Task<List<Categoria>> GetAllCategorias();
        Task<Categoria> GetCategoriaById(string id);
    }
}