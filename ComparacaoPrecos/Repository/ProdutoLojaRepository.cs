using ComparacaoPrecos.Data;
using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Repository;

public class ProdutoLojaRepository : IProdutoLojaRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoLojaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Criar um novo produto
    public async Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja)
    {
        var existing = await _context.Produto_Loja
            .FirstOrDefaultAsync(pl =>
                pl.ProdutoID == produtoLoja.ProdutoID &&
                pl.LojaID == produtoLoja.LojaID);

        if (existing != null)
        {
            existing.preco = produtoLoja.preco;
            existing.DataHora = DateTime.UtcNow;
            existing.Id = produtoLoja.Id;
        }
        else
        {
            _context.Produto_Loja.Add(produtoLoja);
        }

        await _context.SaveChangesAsync();
        return existing ?? produtoLoja;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<List<Produto_Loja>> GetAllProdutosLojas()
    {
        return await _context.Produto_Loja.Where(p => !p.Produto.Deleted && !p.Loja.Deleted).ToListAsync();
    }

    // Buscar produto por ProdutoID que não está deletado
    public async Task<List<Produto_Loja>> GetProdutoLojaByProduto(int id)
    {
        return await _context.Produto_Loja.Include(p => p.Loja).Where(p => p.ProdutoID == id && !p.Produto.Deleted && !p.Loja.Deleted && !p.Deleted).ToListAsync()
               ?? throw new InvalidOperationException("Produto_Loja not found or is deleted.");
    }

    public async Task<List<Produto_Loja>> GetProdutoLojaByLoja(int id)
    {
        return await _context.Produto_Loja
            .Include(p => p.Produto)
            .Where(p => p.LojaID == id && !p.Produto.Deleted && !p.Loja.Deleted && !p.Deleted)
            .ToListAsync()
            ?? throw new InvalidOperationException("Produto_Loja not found or is deleted.");
    }

    public async Task<Produto_Loja> GetProdutoLojaAsync(int ProdutoID, int LojaID)
    {
        var result = await _context.Produto_Loja
            .FirstOrDefaultAsync(pl => pl.ProdutoID == ProdutoID && pl.LojaID == LojaID && !pl.Deleted);

        return result ?? throw new InvalidOperationException("Produto_Loja not found.");
    }

    public async Task<bool> SoftDeleteProdutoLojaAsync(int lojaId, int produtoId)
    {
        var produtoLoja = await _context.Produto_Loja
            .FirstOrDefaultAsync(pl => pl.LojaID == lojaId && pl.ProdutoID == produtoId);

        if (produtoLoja == null)
        {
            return false;
        }

        produtoLoja.Deleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}