using ComparacaoPrecos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparacaoPrecos.Service;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllCategorias();
    
    Task<Categoria> AddCategoria(Categoria categoria);
    
    Task<Categoria> UpdateCategoria(Categoria categoria);
    
    Task<bool> DeleteCategoria(string id);
}
