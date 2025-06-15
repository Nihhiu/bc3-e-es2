namespace ComparacaoPrecos.Tests;

using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Service;
using System.ComponentModel.DataAnnotations;

public class Tests
{
    // [SetUp]
    // public void Setup()
    // {
    // }

    // [Test]
    // public void Test1()
    // {
    //     Assert.Pass();
    // }

    [TestFixture]
    public class ProductUnitTests
    {
        // private Mock<ProdutoRepository> _mockProdutoRepository;
        // private ProdutoService _produtoService;

        // [SetUp]
        // public void Setup()
        // {
        //     _mockProdutoRepository = new Mock<ProdutoRepository>(null!);
        //     _produtoService = new ProdutoService(
        //         _mockProdutoRepository.Object,
        //         Mock.Of<CategoriaRepository>(),
        //         Mock.Of<ProdutoLojaRepository>(),
        //         Mock.Of<LojaRepository>()
        //     );
        // }

        private Mock<IProdutoRepository> _mockProdutoRepository;
        private Mock<ICategoriaRepository> _mockCategoriaRepository;
        private Mock<IProdutoLojaRepository> _mockProdutoLojaRepository;
        private Mock<ILojaRepository> _mockLojaRepository;
        private ProdutoService _produtoService;

        [SetUp]
        public void Setup()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockCategoriaRepository = new Mock<ICategoriaRepository>();
            _mockProdutoLojaRepository = new Mock<IProdutoLojaRepository>();
            _mockLojaRepository = new Mock<ILojaRepository>();

            _produtoService = new ProdutoService(
                _mockProdutoRepository.Object,
                _mockCategoriaRepository.Object,
                _mockProdutoLojaRepository.Object,
                _mockLojaRepository.Object);
        }

        [Test]
        public void CreateProduct_NullName_ShouldFail()
        {
            var produto = new Produto { Nome = null, CategoriaID = "Cosméticos" };

            var ex = Assert.ThrowsAsync<ValidationException>(() =>
                _produtoService.CriarProdutoAsync(produto));

            Assert.That(ex!.Message, Does.Contain("não pode ser nulo"));
        }

        [TestCase("Perfect World!")]
        [TestCase("World of W@rCraft")]
        [TestCase("#Call Of Duty")]
        [TestCase("Need for $peed")]
        [TestCase("[DOTA 2")]
        [TestCase("DOTA 2]")]
        [TestCase("{League of Legends")]
        [TestCase("League of Legends}")]
        [TestCase("\\Counter-Strike")]
        [TestCase("<Marvel Rivals")]
        [TestCase("Marvel Rivals>")]
        [TestCase("Apex º Legends")]
        [TestCase("Apex ª Legends")]
        [TestCase("'Left 4 Dead 2")]
        [TestCase("Destiny?")]
        [TestCase("Destiny = 2")]
        [TestCase("/PUBG")]
        [TestCase("Lost ´Ark")]
        [TestCase("`Lost Ark")]
        [TestCase("Portal~")]
        [TestCase("Por^tal")]
        [TestCase("War,Hammer")]
        [TestCase("W.arHammer")]
        [TestCase("WarHamm;er")]
        [TestCase("War\"Frame")]
        [TestCase("«Tribos")]
        [TestCase("Tri»bos")]
        [TestCase("Elden_Ring")]
        [TestCase("Dark > Souls")]
        [TestCase("Dark < Souls")]
        public void CreateProduct_InvalidCharacter_ShouldFail(string invalidName)
        {
            var produto = new Produto { Nome = invalidName, CategoriaID = "Games" };

            var ex = Assert.ThrowsAsync<ValidationException>(() =>
                _produtoService.CriarProdutoAsync(produto));

            Assert.That(ex!.Message, Does.Contain("caracteres inválidos"));
        }

        [TestCase("Lipton limão")]
        [TestCase("Café")]
        [TestCase("Sumol de Laranja")]
        [TestCase("Limonada")]
        [TestCase("Guaraná")]
        [TestCase("Sangria")]
        [TestCase("Vinho Tinto")]
        [TestCase("Nestea de pêssego")]
        [TestCase("Água das pedras")]
        [TestCase("Compal de pêra")]
        public async Task CreateProduct_ValidName_ShouldPass(string validName)
        {
            Produto? savedProduto = null;

            _mockProdutoRepository
                .Setup(r => r.AddProduto(It.IsAny<Produto>()))
                .Callback<Produto>(p => savedProduto = p)
                .ReturnsAsync((Produto p) => p);

            var produto = new Produto { Nome = validName, CategoriaID = "Bebidas" };

            await _produtoService.CriarProdutoAsync(produto);

            Assert.IsNotNull(savedProduto);
            Assert.AreEqual(validName, savedProduto!.Nome);
        }
    }

}