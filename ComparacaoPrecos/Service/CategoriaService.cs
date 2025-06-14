using ComparacaoPrecos.Repository;

namespace ComparacaoPrecos.Service;

public class CategoriaService : ICategoriaService
{
    private readonly CategoriaRepository _categoriaRepository;

    public CategoriaService(CategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    // Buscar todas as categorias que não estão deletadas
    public async Task<IEnumerable<Categoria>> GetAllCategorias()
    {
        return await _categoriaRepository.GetAllCategorias();
    }

    // Criar uma nova categoria
    public async Task<Categoria> AddCategoria(Categoria categoria)
    {
        return await _categoriaRepository.AddCategoria(categoria);
    }

    // Atualizar categoria
    public async Task<Categoria> UpdateCategoria(Categoria categoria)
    {
        return await _categoriaRepository.UpdateCategoria(categoria);
    }

    // Deletar categoria (marcar como deletada)
    public async Task<bool> DeleteCategoria(string id)
    {
        return await _categoriaRepository.DeleteCategoria(id);
    }
}