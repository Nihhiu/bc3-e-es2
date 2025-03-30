using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparacaoPrecos.Repository;

public class ProdutoRepository {
    private readonly ApplicationDbContext _context;

    public ProdutoRepository(ApplicationDbContext context) {
        _context = context;
    }

    // Criar um novo produto
    public async Task<Produto> AddProduto(Produto produto) {
        _context.Produto.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<List<Produto>> GetAllProdutos() {
        return await _context.Produto.Where(p => !p.Deleted).ToListAsync();
    }

    // Buscar produto por ID que não está deletado
    public async Task<Produto> GetProdutoById(int id) {
        var produto = await _context.Produto.FirstOrDefaultAsync(p => p.ProdutoID == id && !p.Deleted) ?? throw new KeyNotFoundException($"Produto with ID {id} not found or is deleted.");
        return produto;
    }

    // Atualizar produto
    public async Task<Produto> UpdateProduto(Produto produto) {
        _context.Produto.Update(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    // Deletar produto (marcar como deletado)
    public async Task<bool> DeleteProduto(int id) {
        var produto = await GetProdutoById(id);
        if (produto == null) return false;
        
        produto.Deleted = true;
        _context.Produto.Update(produto);
        await _context.SaveChangesAsync();
        return true;
    }
}