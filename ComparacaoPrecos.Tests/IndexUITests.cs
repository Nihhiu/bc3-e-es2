using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using SeleniumExtras.WaitHelpers;
using System;

namespace ComparacaoPrecos.Tests
{
    [TestFixture]
    public class IndexUITests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly string appUrl = "http://localhost:5056";

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();
            options.AddArgument("headless");
            options.AddArgument("disable-gpu");
            driver = new EdgeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Deve_Listar_Produtos_Quando_Acessa_Index()
        {
            driver.Navigate().GoToUrl($"{appUrl}/produto");

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".row .col")));

            var produtos = driver.FindElements(By.CssSelector(".row .col"));

            Assert.IsTrue(produtos.Count > 0, "Nenhum produto encontrado na lista.");
        }

        [Test]
        public void Deve_Criar_Novo_Produto_Com_Sucesso()
        {
            driver.Navigate().GoToUrl($"{appUrl}/produto/criar");

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Produto_Nome")));

            string nomeProduto = "Monitor 4K " + Guid.NewGuid().ToString().Substring(0, 8);
            driver.FindElement(By.Id("Produto_Nome")).SendKeys(nomeProduto);

            var categoriaDropdown = new SelectElement(driver.FindElement(By.Id("Produto_CategoriaID")));
            categoriaDropdown.SelectByIndex(1);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(ExpectedConditions.UrlContains("/produto"));

            IWebElement novoProduto = wait.Until(
                ExpectedConditions.ElementExists(By.XPath($"//div[contains(@class, 'col') and contains(., '{nomeProduto}')]"))
            );

            Assert.IsNotNull(novoProduto, "Produto criado não foi encontrado na lista");
        }

        [Test]
        public void DetalhesProduto_DeveExibirNomeProduto_E_LojasOuMensagem()
        {
            int produtoId = 8;
            driver.Navigate().GoToUrl($"{appUrl}/produto/{produtoId}");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                var tituloProduto = wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h2.card-title"))
                );

                Assert.IsNotNull(tituloProduto);
                Assert.IsNotEmpty(tituloProduto.Text);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Título do produto (h2.card-title) não foi encontrado dentro do tempo limite.");
            }

            var lojas = driver.FindElements(By.CssSelector(".card-body .card-title a"));
            var mensagemNenhumaLoja = driver.FindElements(By.CssSelector(".alert.alert-info"));

            Assert.IsTrue(lojas.Count > 0 || mensagemNenhumaLoja.Count > 0);

            if (mensagemNenhumaLoja.Count > 0)
            {
                Assert.That(mensagemNenhumaLoja[0].Text, Does.Contain("Nenhuma loja disponível"));
            }
        }

        [Test]
        public void FiltrarProdutos_DeveExibirResultadosOuMensagem()
        {
            var driver = new EdgeDriver();
            try
            {
                string appUrl = "http://localhost:5056";
                driver.Navigate().GoToUrl($"{appUrl}/produto");

                // Maximiza para evitar layout colapsado
                driver.Manage().Window.Maximize();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Espera o formulário estar visível
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("filtroProduto")));

                // Preenche o campo de nome (ex: "Lego")
                var nomeInput = driver.FindElement(By.Id("nome"));
                nomeInput.Clear();
                nomeInput.SendKeys("Lego");

                // Clica no botão "Filtrar" via JavaScript para evitar interceptação
                var botaoFiltrar = driver.FindElement(By.CssSelector("form button[type='submit']"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", botaoFiltrar);

                // Espera os resultados atualizarem (cards ou mensagem)
                wait.Until(d =>
                {
                    return d.FindElements(By.CssSelector(".card-title")).Any() ||
                           d.FindElements(By.CssSelector(".alert-info")).Any();
                });

                // Valida se apareceu pelo menos um produto OU a mensagem de alerta
                var produtos = driver.FindElements(By.CssSelector(".card-title"));
                var mensagem = driver.FindElements(By.CssSelector(".alert-info"));

                Assert.True(produtos.Any() || mensagem.Any(), "Nenhum produto ou mensagem encontrada após filtrar.");
            }
            finally
            {
                driver.Quit();
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}