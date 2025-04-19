// using ComparacaoPrecos.Repository;

// namespace ComparacaoPrecos.Service;

// public class CategoriaService {
//     private readonly CategoriaRepository _categoriaRepository;

//     public CategoriaService(CategoriaRepository categoriaRepository) {
//         _categoriaRepository = categoriaRepository;
//     }

//     // Buscar todas as categorias que não estão deletadas
//     public async Task<IEnumerable<Categoria>> GetAllCategorias()
//     {
//         return await _categoriaRepository.GetAllCategorias();
//     }

//     // Criar uma nova categoria
//     public async Task<Categoria> AddCategoria(Categoria categoria)
//     {
//         return await _categoriaRepository.AddCategoria(categoria);
//     }

//     // Atualizar categoria
//     public async Task<Categoria> UpdateCategoria(Categoria categoria)
//     {
//         return await _categoriaRepository.UpdateCategoria(categoria);
//     }

//     // Deletar categoria (marcar como deletada)
//     public async Task<bool> DeleteCategoria(string id)
//     {
//         return await _categoriaRepository.DeleteCategoria(id);
//     }
// }

using ComparacaoPrecos.Repository.Interfaces;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Service;

public class CategoriaService
{
    private readonly ICategoriaReader _categoriaReader;
    private readonly ICategoriaWriter _categoriaWriter;

    public CategoriaService(ICategoriaReader categoriaReader, ICategoriaWriter categoriaWriter)
    {
        _categoriaReader = categoriaReader;
        _categoriaWriter = categoriaWriter;
    }

    public async Task<IEnumerable<Categoria>> GetAllCategorias()
    {
        return await _categoriaReader.GetAllCategorias();
    }

    public async Task<Categoria> AddCategoria(Categoria categoria)
    {
        return await _categoriaWriter.AddCategoria(categoria);
    }

    public async Task<Categoria> UpdateCategoria(Categoria categoria)
    {
        return await _categoriaWriter.UpdateCategoria(categoria);
    }

    public async Task<bool> DeleteCategoria(string id)
    {
        return await _categoriaWriter.DeleteCategoria(id);
    }
}
