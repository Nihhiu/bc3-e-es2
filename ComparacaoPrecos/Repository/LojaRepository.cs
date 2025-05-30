using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data;

namespace ComparacaoPrecos.Repository;

public class LojaRepository
{
    private readonly ApplicationDbContext _context;

    public LojaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Criar uma nova loja
    public async Task<Loja> AddLoja(Loja loja)
    {
        _context.Loja.Add(loja);
        await _context.SaveChangesAsync();
        return loja;
    }

    // Buscar todas as lojas que não estão deletadas
    public async Task<List<Loja>> GetAllLojas()
    {
        return await _context.Loja.Where(l => !l.Deleted).ToListAsync();
    }

    // Buscar loja por ID que não está deletada
    public async Task<Loja> GetLojaById(int id)
    {
        var loja = await _context.Loja.FirstOrDefaultAsync(l => l.LojaID == id && !l.Deleted) ?? throw new KeyNotFoundException($"Loja with ID {id} not found or is deleted.");
        return loja;
    }

}