using ComparacaoPrecos.Data;
using Microsoft.EntityFrameworkCore;

namespace ComparacaoPrecos.Repository;

public class CategoriaRepository{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Categoria> AddCategoria(Categoria categoria) {
        _context.Categoria.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<List<Categoria>> GetAllCategorias() {
        return await _context.Categoria.Where(c => !c.Deleted).ToListAsync();
    }

    public async Task<Categoria> GetCategoriaById(string id) {
        return await _context.Categoria.FirstOrDefaultAsync(c => c.CategoriaID == id && !c.Deleted) 
               ?? throw new InvalidOperationException("Categoria not found.");
    }

    public async Task<Categoria> UpdateCategoria(Categoria categoria) {
        _context.Categoria.Update(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> DeleteCategoria(string id) {
        var categoria = await GetCategoriaById(id);
        if (categoria == null) return false;

        categoria.Deleted = true;
        _context.Categoria.Update(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}