namespace ComparacaoPrecos.Repository.Interfaces
{
    public interface ICategoriaWriter
    {
        Task<Categoria> AddCategoria(Categoria categoria);
        Task<Categoria> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(string id);
    }
}