using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using ComparacaoPrecos.Controller;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComparacaoPrecos.Test;

[TestFixture]
public class ProdutoControllerTests : IDisposable
{
    // Alterado para Mock da interface
    protected Mock<IProdutoService> _mockProdutoService;
    protected ProdutoController _controller;
    private ClaimsPrincipal _user;
    
    // Alterado para Mock das interfaces correspondentes
    private Mock<ICategoriaService> _mockCategoriaService;
    private Mock<ILojaService> _mockLojaService;

    [SetUp]
    public void Setup()
    {
        // 1. Criar mocks das interfaces de serviço
        _mockProdutoService = new Mock<IProdutoService>();
        _mockCategoriaService = new Mock<ICategoriaService>();
        _mockLojaService = new Mock<ILojaService>();

        // 2. Configurar usuário autenticado
        _user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "test@example.com"),
            new Claim(ClaimTypes.NameIdentifier, "123")
        }, "test"));

        // 3. Instanciar controller com os mocks das interfaces
        _controller = new ProdutoController(
            _mockProdutoService.Object,
            _mockCategoriaService.Object,
            _mockLojaService.Object
        )
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = _user }
            }
        };
    }

    // Implementar Dispose se necessário
    public void Dispose()
    {
        // Limpar recursos se necessário
    }
}
[TestFixture]
public class AddPrecoTests : ProdutoControllerTests
{
    private ProdutoViewModel _model;
    private const int ProdutoId = 1;
    private const int LojaId = 1;

    [SetUp]
    public new void Setup()
    {
        base.Setup();

        _model = new ProdutoViewModel
        {
            InfoPorLoja = new List<ProdutoLojaViewModel>
            {
                new ProdutoLojaViewModel
                {
                    LojaID = LojaId,
                    Preco = 99.99
                }
            }
        };
    }

    [Test]
    public async Task AddPreco_UsuarioNaoAutenticado_RedirecionaParaLogin()
    {
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
        };

        var resultado = await _controller.AddPreco(_model, ProdutoId) as RedirectToActionResult;

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado.ActionName, Is.EqualTo("Login"));
        Assert.That(resultado.ControllerName, Is.EqualTo("Account"));
    }

    [Test]
    public async Task AddPreco_PrecoExistenteSemConfirmacao_RetornaJsonDeConfirmacao()
    {
        // Arrange
        const double precoAntigo = 79.99;
        const double precoNovo = 99.99;

        // Preparar mock de produto existente
        var produtoLojaExistente = new Produto_Loja
        {
            ProdutoID = ProdutoId,
            LojaID = LojaId,
            preco = precoAntigo
        };

        _mockProdutoService
            .Setup(s => s.GetProdutoLojaAsync(ProdutoId, LojaId))
            .ReturnsAsync(produtoLojaExistente);

        // Usuário autenticado
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.NameIdentifier, "user-id")
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        // Request sem confirmação
        var context = new DefaultHttpContext
        {
            User = user,
            Request = { Form = new FormCollection(new Dictionary<string, StringValues>()) }
        };
        _controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Act
        var resultado = await _controller.AddPreco(_model, ProdutoId) as JsonResult;
        Assert.That(resultado, Is.Not.Null, "Esperava JsonResult não nulo");

        // Assert JSON
        var json = JsonSerializer.Serialize(resultado.Value);
        var response = JsonSerializer.Deserialize<ConfirmacaoResponse>(json);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.RequiresConfirmation, Is.True);
        Assert.That(response.OldPrice, Is.EqualTo(precoAntigo.ToString("N2")));
        Assert.That(response.NewPrice, Is.EqualTo(precoNovo.ToString("N2")));
    }

    private class ConfirmacaoResponse
    {
        [JsonPropertyName("requiresConfirmation")]
        public bool RequiresConfirmation { get; set; }

        [JsonPropertyName("oldPrice")]
        public string? OldPrice { get; set; }

        [JsonPropertyName("newPrice")]
        public string? NewPrice { get; set; }
    }
}
