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
            _driver.Navigate().GoToUrl($"{BaseUrl}/loja");
            _driver.Manage().Cookies.DeleteAllCookies();

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
    }
}
