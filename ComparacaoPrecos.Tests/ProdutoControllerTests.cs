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
    private Mock<ProdutoService> _mockProdutoService;
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
        // Arrange
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
        };

        // Act
        var resultado = await _controller.AddPreco(_model, ProdutoId) as RedirectToActionResult;

        // Assert
        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado.ActionName, Is.EqualTo("Login"));
        Assert.That(resultado.ControllerName, Is.EqualTo("Account"));
    }

    
}