using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ComparacaoPrecos.Tests
{
    [TestFixture]
    public class LojaIndexUiTests
    {
        private IWebDriver _driver;
        private const string BaseUrl = "http://localhost:5056"; 

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string driverPath = TestContext.CurrentContext.TestDirectory;
            var chromeOptions = new ChromeOptions();
            _driver = new ChromeDriver(driverPath, chromeOptions);
            _driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void Setup()
        {
            _driver.Manage().Cookies.DeleteAllCookies(); 
            _driver.Navigate().GoToUrl($"{BaseUrl}/loja");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.text-primary")));
        }

        [TearDown]
        public void TearDown()
        {
           
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        [Test]
        public async Task LojaIndex_PageLoadsSuccessfully_AndDisplaysStores()
        {
            Assert.That(_driver.Title, Does.Contain("Lista de Lojas"));

            IWebElement mainHeading = _driver.FindElement(By.CssSelector("h1.text-primary"));
            Assert.That(mainHeading.Text, Is.EqualTo("Lista de Lojas"));

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ReadOnlyCollection<IWebElement> storeCards = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".card.h-100")));
            
            Assert.That(storeCards.Count, Is.GreaterThanOrEqualTo(1), "Expected at least one store to be displayed on the page.");
            await Task.CompletedTask; 
        }

        [Test]
        public async Task LojaIndex_ClickFirstStoreCard_NavigatesToDetalhesPage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15)); 
            
            IWebElement firstStoreCardLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.text-decoration-none.text-dark")));
            
            string initialUrl = _driver.Url; 

            firstStoreCardLink.Click();

            wait.Until(ExpectedConditions.UrlMatches($@"^{BaseUrl}/loja/\d+$"));

            Assert.That(_driver.Url, Is.Not.EqualTo(initialUrl), "URL should have changed after clicking a store card.");
            Assert.That(_driver.Url, Does.Match($@"^{BaseUrl}/loja/\d+$"), "URL should match the expected format for Detalhes page with an ID (e.g., /loja/1).");
            await Task.CompletedTask; 
        }
        [Test]
        public async Task LojaIndex_AdminFullWorkflow_VerifiesFeaturesAndDetails()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20)); 

            _driver.Navigate().GoToUrl($"{BaseUrl}/Identity/Account/Login");
            
            IWebElement emailInput = _driver.FindElement(By.Id("Input_Email"));
            IWebElement passwordInput = _driver.FindElement(By.Id("Input_Password"));
            IWebElement loginButton = _driver.FindElement(By.Id("login-submit"));

            emailInput.SendKeys("admin@gmail.com");
            passwordInput.SendKeys("Admin@123");
            loginButton.Click();
            try
            {
                wait.Until(ExpectedConditions.UrlToBe(BaseUrl + "/")); 
                Console.WriteLine("DEBUG: Redirecionado para o BaseUrl ('/') após o login.");
                IWebElement welcomeMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.nav-link[title='Manage']")));
                Assert.That(welcomeMessage.Text, Does.Contain("Hello admin@gmail.com!"), "Mensagem de boas-vindas do admin não encontrada após login.");
                Console.WriteLine("DEBUG: Admin logado com sucesso! Mensagem de boas-vindas encontrada.");
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"--- DEBUG INFO: Login Admin Falhou ou Redirecionamento Incorreto ---");
                Console.WriteLine($"Exception Message: {ex.Message}");
                Console.WriteLine($"Current URL: {_driver.Url}");
                Console.WriteLine($"Page Title: {_driver.Title}");
                Console.WriteLine($"Page Source Excerpt:\n{_driver.PageSource.Substring(0, Math.Min(2000, _driver.PageSource.Length))}"); 
                Console.WriteLine($"--- END DEBUG INFO ---");
                throw new AssertionException("Falha ao logar como administrador ou redirecionamento incorreto após login.");
            }
            _driver.Navigate().GoToUrl($"{BaseUrl}/loja");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.text-primary")));
            Console.WriteLine("DEBUG: Navegado para a página de listagem de lojas.");
            IWebElement firstStoreCardLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.text-decoration-none.text-dark")));
            firstStoreCardLink.Click();
            Console.WriteLine("DEBUG: Clicado no primeiro cartão de loja.");

            wait.Until(ExpectedConditions.UrlMatches($@"^{BaseUrl}/loja/\d+$"));
            Console.WriteLine($"DEBUG: Navegado para a página de detalhes da loja: {_driver.Url}");
            
            Assert.That(_driver.Title, Does.Contain("Detalhes da Loja"), "O título da página de detalhes está incorreto.");

            IWebElement storeNameInDetails = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2.card-title.text-primary")));
            Assert.That(storeNameInDetails.Text, Does.Not.Empty, "O nome da loja na página de detalhes não deve ser vazio.");

            IWebElement productsTable = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".table.table-hover")));
            Assert.That(productsTable.Displayed, Is.True, "A tabela de produtos não está visível na página de detalhes.");

            Assert.That(_driver.PageSource, Does.Contain("Televisão"), "Produto 'Televisão' não encontrado na tabela de detalhes.");
            Assert.That(_driver.PageSource, Does.Contain("Maçã"), "Produto 'Maçã' não encontrado na tabela de detalhes.");
            Assert.That(_driver.PageSource, Does.Contain("Banana"), "Produto 'Banana' não encontrado na tabela de detalhes.");

            Console.WriteLine("Fluxo completo do utilizador Admin na página de Lojas e detalhes da loja verificado com sucesso.");
            await Task.CompletedTask;
        }
    }
}