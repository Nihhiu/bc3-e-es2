namespace ComparacaoPrecos.Service.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria> AddCategoria(Categoria categoria);
        Task<Categoria> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(string id);
    }
}