using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class CategoriaService {
    private readonly CategoriaRepository _categoriaRepository;

    public CategoriaService(CategoriaRepository categoriaRepository) {
        _categoriaRepository = categoriaRepository;
    }
    public async Task<IEnumerable<Categoria>> GetAllCategorias()
    {
        return await _categoriaRepository.GetAllCategorias();
    }

    public async Task<Categoria> AddCategoria(Categoria categoria)
    {
        return await _categoriaRepository.AddCategoria(categoria);
    }

    public async Task<Categoria> UpdateCategoria(Categoria categoria)
    {
        return await _categoriaRepository.UpdateCategoria(categoria);
    }

    public async Task<bool> DeleteCategoria(string id)
    {
        return await _categoriaRepository.DeleteCategoria(id);
    }
}