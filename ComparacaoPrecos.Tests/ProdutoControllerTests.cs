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
using Microsoft.AspNetCore.Mvc.Routing;

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

        var mockUrlHelper = new Mock<IUrlHelper>();

        // 2) Configura para devolver sempre "/Produto/Index" (ou o que a sua lógica gerar)
        mockUrlHelper
            .Setup(x => x.Action(It.IsAny<UrlActionContext>()))
            .Returns("/produto/");

        // 3) Atribui ao controller antes de cada teste
        _controller.Url = mockUrlHelper.Object;
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

    /// <summary>
    /// Tests the <c>AddPreco</c> action to ensure that when a price already exists for a product in a store and no confirmation is provided,
    /// the controller returns a JSON response indicating that confirmation is required, along with the old and new prices.
    /// </summary>
    /// <remarks>
    /// This test sets up a mock existing product price, simulates an authenticated user, and sends a request without confirmation.
    /// It asserts that the response is a <see cref="JsonResult"/> containing a <see cref="ConfirmacaoResponse"/> with <c>RequiresConfirmation</c> set to true,
    /// and verifies that the old and new prices are correctly included in the response.
    /// </remarks>
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

    [Test]
    public async Task AddPreco_SemPrecoExistente_AdicionaPrecoERetornaRedirectUrl()
    {
        // Arrange
        const double precoNovo = 99.99;

        // Mock de GetProdutoLojaAsync para simular que não há preço anterior
        _mockProdutoService
            .Setup(s => s.GetProdutoLojaAsync(ProdutoId, LojaId))
            .ReturnsAsync((Produto_Loja?)null);

        // Capturar a chamada de AddProdutoLoja
        Produto_Loja? produtoLojaAdicionado = null;
        _mockProdutoService
            .Setup(s => s.AddProdutoLoja(It.IsAny<Produto_Loja>()))
            .Callback<Produto_Loja>(pl => produtoLojaAdicionado = pl)
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Simular utilizador autenticado
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.NameIdentifier, "user-id")
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

        // Context sem confirmação (form vazio)
        var context = new DefaultHttpContext
        {
            User = user,
            Request = { Form = new FormCollection(new Dictionary<string, StringValues>()) }
        };
        _controller.ControllerContext = new ControllerContext { HttpContext = context };

        // Ajustar o preço no view-model
        _model.InfoPorLoja[0].Preco = precoNovo;

        // Act
        var result = await _controller.AddPreco(_model, ProdutoId) as JsonResult;

        // Assert
        Assert.That(result, Is.Not.Null, "Esperava JsonResult não nulo");

        // Verifica que o serviço foi chamado
        _mockProdutoService.Verify();

        // Verifica que o objeto enviado ao serviço tem os dados corretos
        Assert.That(produtoLojaAdicionado, Is.Not.Null, "Esperava ter sido criado um Produto_Loja");
        Assert.That(produtoLojaAdicionado!.ProdutoID, Is.EqualTo(ProdutoId));
        Assert.That(produtoLojaAdicionado.LojaID, Is.EqualTo(LojaId));
        Assert.That(produtoLojaAdicionado.preco, Is.EqualTo(precoNovo));
        Assert.That(produtoLojaAdicionado.Id, Is.EqualTo("user-id"));
        Assert.That(produtoLojaAdicionado.DataHora, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));

        // Extrair o redirectUrl do JSON
        var json = JsonSerializer.Serialize(result.Value);
        using var doc = JsonDocument.Parse(json);
        var redirectUrl = doc.RootElement.GetProperty("redirectUrl").GetString();

        Assert.That(redirectUrl, Is.EqualTo("/produto/"));
    }

    [Test]
    public async Task AddPreco_PrecoVazio_RetornaBadRequestComErroDeModelState()
    {
        // Arrange
        // Simular usuário autenticado
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.NameIdentifier, "user-id")
        };
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth")),
                // Simular form sem o campo Preco
                Request = { Form = new FormCollection(new Dictionary<string, StringValues>()) }
            }
        };

        // Forçar erro de binding/validação no ModelState,
        // como se “InfoPorLoja[0].Preco” não tivesse sido enviado
        _controller.ModelState.AddModelError(
            "InfoPorLoja[0].Preco",
            "O preço é obrigatório."
        );

        // Preencher o resto do view-model (sem se preocupar com Preco)
        var vm = new ProdutoViewModel
        {
            InfoPorLoja = new List<ProdutoLojaViewModel>
            {
                new ProdutoLojaViewModel
                {
                    LojaID = LojaId,
                    // Preco não importa aqui
                }
            }
        };

        // Act
        var result = await _controller.AddPreco(vm, ProdutoId);

        // Assert
        // Deve ser BadRequestObjectResult com o próprio ModelState
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequest = (BadRequestObjectResult)result;
        Assert.That(badRequest.Value, Is.Not.Null);

        // Se quiser, pode descer no Value e verificar o erro:
        var errors = ((SerializableError)badRequest.Value)
                        .SelectMany(kvp => (string[])kvp.Value)
                        .ToList();
        Assert.That(errors, Has.One.Items);
        Assert.That(errors.First(), Does.Contain("O preço é obrigatório."));
    }

}
