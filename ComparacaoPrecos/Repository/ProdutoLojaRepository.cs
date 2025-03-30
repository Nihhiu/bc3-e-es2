using ComparacaoPrecos.Data;
using Microsoft.EntityFrameworkCore;

namespace ComparacaoPrecos.Repository;

public class ProdutoLojaRepository{
    private readonly ApplicationDbContext _context;

    public ProdutoLojaRepository(ApplicationDbContext context) {
        _context = context;
    }

    // Criar um novo produto
    public async Task<Produto_Loja> AddProdutoLoja(Produto_Loja produtoLoja) {
        _context.Produto_Loja.Add(produtoLoja);
        await _context.SaveChangesAsync();
        return produtoLoja;
    }

    // Buscar todos os produtos que não estão deletados
    public async Task<List<Produto_Loja>> GetAllProdutosLojas() {
        return await _context.Produto_Loja.Include(p => p.Produto).Include(p => p.Loja).Where(p => !p.Produto.Deleted && !p.Loja.Deleted).ToListAsync();
    }
}