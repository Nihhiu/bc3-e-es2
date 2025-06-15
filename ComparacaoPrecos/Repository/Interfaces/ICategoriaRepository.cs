using ComparacaoPrecos.Data;

namespace ComparacaoPrecos.Repository
{
    public interface ICategoriaRepository
    {
        Task<Categoria> AddCategoria(Categoria categoria);
        Task<List<Categoria>> GetAllCategorias();
        Task<Categoria> GetCategoriaById(string id);
        Task<Categoria> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(string id);
    }
}
