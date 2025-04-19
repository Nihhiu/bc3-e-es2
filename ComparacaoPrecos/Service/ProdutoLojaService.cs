// using ComparacaoPrecos.Data;
// using ComparacaoPrecos.Repository;

// namespace ComparacaoPrecos.Service;

// public class ProdutoLojaService {
//     private readonly ProdutoLojaRepository _produtoLojaRepository;

//     public ProdutoLojaService(ProdutoLojaRepository produtoLojaRepository)
//     {
//         _produtoLojaRepository = produtoLojaRepository;
//     }

//     // Buscar todos os produtos que não estão deletados
//     public async Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas()
//     {
//         return await _produtoLojaRepository.GetAllProdutosLojas();
//     }

//     // Buscar produto por id que não estão deletados
//     public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByProduto(int id)
//     {
//         return await _produtoLojaRepository.GetProdutoLojaByProduto(id);
//     }
// }

using ComparacaoPrecos.Repository.Interfaces;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Service;

public class ProdutoLojaService
{
    private readonly IProdutoLojaReader _produtoLojaReader;

    public ProdutoLojaService(IProdutoLojaReader produtoLojaReader)
    {
        _produtoLojaReader = produtoLojaReader;
    }

    public async Task<IEnumerable<Produto_Loja>> GetAllProdutosLojas()
    {
        return await _produtoLojaReader.GetAllProdutosLojas();
    }

    public async Task<IEnumerable<Produto_Loja>> GetProdutoLojaByProduto(int id)
    {
        return await _produtoLojaReader.GetProdutoLojaByProduto(id);
    }
}
