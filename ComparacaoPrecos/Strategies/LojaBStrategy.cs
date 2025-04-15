using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComparacaoPrecos.Strategies
{
    public class LojaBStrategy : IProdutoLojaStrategy
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly ProdutoLojaRepository _produtoLojaRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public LojaBStrategy(ProdutoRepository produtoRepository, ProdutoLojaRepository produtoLojaRepository, CategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _produtoLojaRepository = produtoLojaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ProdutoViewModel?> Build(int produtoId)
        {
            var produto = await _produtoRepository.GetProdutoById(produtoId);
            if (produto == null) return null;

            var categorias = await _categoriaRepository.GetAllCategorias();
            var produtoLoja = await _produtoLojaRepository.GetProdutoLojaByProduto(produtoId);

            return new ProdutoViewModel
            {
                Nome = produto.Nome,
                CategoriaID = produto.CategoriaID,
                Deleted = produto.Deleted,
                Categorias = categorias
                    .Select(c => new SelectListItem { Value = c.CategoriaID, Text = c.CategoriaID })
                    .ToList(),
                InfoPorLoja = produtoLoja.Select(pl => new ProdutoLojaViewModel
                {
                    NomeLoja = pl.Loja.Nome,
                    Preco = pl.preco * 1.10 // Loja B tem um acréscimo de 10% no preço
                }).ToList()
            };
        }
    }
}