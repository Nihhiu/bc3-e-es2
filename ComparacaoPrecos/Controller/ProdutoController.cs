using ComparacaoPrecos.Data.DB;
using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository.ProdutoRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparacaoPrecos.Controller.ProdutoController;

[Route("produto")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;

    public ProdutoController(ProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    // Criar um novo produto
    [HttpPost]
    public async Task<IActionResult> AddProduto([FromBody] Produto produto) {
        if (produto == null) return BadRequest("Produto não pode ser nulo.");

        var novoProduto = await _produtoRepository.AddProduto(produto);
        return CreatedAtAction(nameof(GetProdutoById), new { id = novoProduto.ProdutoID }, novoProduto);
    }

    [HttpGet] 
    public async Task<ActionResult<IEnumerable<Produto>>> GetAllProdutos() {
        var produtos = await _produtoRepository.GetAllProdutos();
        if (produtos == null || produtos.Count == 0) return NotFound("Nenhum produto encontrado.");
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutoById(int id) {
        var produto = await _produtoRepository.GetProdutoById(id);
         if (produto == null) return NotFound("Produto não encontrado.");
        return Ok(produto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
    {
        if (id != produto.ProdutoID) return BadRequest("ID do produto não corresponde ao ID fornecido.");

        var produtoAtualizado = await _produtoRepository.UpdateProduto(produto);
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var resultado = await _produtoRepository.DeleteProduto(id);
        if (!resultado) return NotFound("Produto não encontrado ou já deletado.");
        return NoContent();
    }
}