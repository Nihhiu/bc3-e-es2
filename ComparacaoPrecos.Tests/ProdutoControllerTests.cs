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


namespace ComparacaoPrecos.Test;

[TestFixture]
public class ProdutoControllerTests : IDisposable
{
    protected Mock<ProdutoService> _mockProdutoService;
    protected ProdutoController _controller;
    private ClaimsPrincipal _user;
    private Mock<CategoriaService> _mockCategoriaService;
    private Mock<LojaService> _mockLojaService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .Options;

        var mockDbContext = new Mock<ApplicationDbContext>(options);
        
        var mockProdutoRepository   = new Mock<ProdutoRepository>(mockDbContext.Object);
        var mockCategoriaRepository = new Mock<CategoriaRepository>(mockDbContext.Object);
        var mockProdutoLojaRepository = new Mock<ProdutoLojaRepository>(mockDbContext.Object);
        var mockLojaRepository      = new Mock<LojaRepository>(mockDbContext.Object);

        // 3. Mock just the services (or even let them use the real repos if you prefer)
        _mockProdutoService = new Mock<ProdutoService>(
            mockProdutoRepository.Object,
            mockCategoriaRepository.Object,
            mockProdutoLojaRepository.Object,
            mockLojaRepository.Object
        );
        _mockCategoriaService = new Mock<CategoriaService>(mockCategoriaRepository.Object);
        _mockLojaService = new Mock<LojaService>(
            mockLojaRepository.Object,
            mockProdutoLojaRepository.Object
        );

        // 4. Build controller with authenticated user
        _user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "test@example.com"),
            new Claim(ClaimTypes.NameIdentifier, "123")
        }, "test"));

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


    [TearDown]
    public void TearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        _controller?.Dispose();
    }
    

    
}

[TestFixture]
public class AddPrecoTests : ProdutoControllerTests
{
    private ProdutoViewModel _model;
    private const int ProdutoId = 1;
    private const int LojaId = 100;

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
                    Preco = (double)99.99m
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
    public async Task AddPreco_PrecoNegativo_AdicionaComSucesso()
    {
        // Arrange
        var model = new ProdutoViewModel
        {
            InfoPorLoja = new List<ProdutoLojaViewModel>
            {
                new ProdutoLojaViewModel { LojaID = 100, Preco = -5.99 }
            }
        };

        _mockProdutoService.Setup(s => s.GetProdutoLojaAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync((Produto_Loja)null);

        // Act
        var result = await _controller.AddPreco(model, 1) as JsonResult;

        // Assert
        _mockProdutoService.Verify(s =>
            s.AddProdutoLoja(It.Is<Produto_Loja>(pl =>
                pl.preco == -5.99
            )),
            Times.Once
        );
    }
    
    [Test]
    public async Task AddPreco_PrecoComMuitasCasasDecimais_TruncadoCorretamente()
    {
        // Arrange
        var model = new ProdutoViewModel
        {
            InfoPorLoja = new List<ProdutoLojaViewModel>
            {
                new ProdutoLojaViewModel { LojaID = 100, Preco = 12.3456789 }
            }
        };
        
        _mockProdutoService
            .Setup(s => s.GetProdutoLojaAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((Produto_Loja?)null); 

        // Act
        var result = await _controller.AddPreco(model, 1) as JsonResult;

        // Assert
        _mockProdutoService.Verify(s => 
            s.AddProdutoLoja(It.Is<Produto_Loja>(pl => 
                (double)pl.preco == 12.35 // Arredondamento para 2 casas
            )), 
            Times.Once
        );
    }
}