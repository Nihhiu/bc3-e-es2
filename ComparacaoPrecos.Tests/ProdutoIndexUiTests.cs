using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ComparacaoPrecos.Tests
{
    [TestFixture]
    public class ProdutoIndexUiTests
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
            _driver.Navigate().GoToUrl($"{BaseUrl}/produto");
            _driver.Manage().Cookies.DeleteAllCookies();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)); 
            
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.text-primary")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nome")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("nome")));
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
        public async Task FilterProdutos_ByName_NoMatchingResults_ShouldDisplayNoProductsMessage()
        {
            string nonExistentProductName = "NonExistentProduct12345";

            IWebElement nameFilterInput = _driver.FindElement(By.Id("nome"));
            IWebElement filterButton = _driver.FindElement(By.CssSelector("form button[type='submit']"));

            nameFilterInput.SendKeys(nonExistentProductName);
            filterButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement noProductsMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".alert.alert-info")));

            Assert.That(noProductsMessage.Text, Does.Contain("Nenhum produto encontrado com os filtros aplicados."));
            Assert.That(_driver.FindElements(By.CssSelector(".card")).Count, Is.EqualTo(0));
        }
    }
}
