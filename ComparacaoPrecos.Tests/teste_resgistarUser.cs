using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace ComparacaoPrecos.Tests
{
    public class Tests
    {
        private IdentityUser user;

        [SetUp]
        public void Setup()
        {
            user = new IdentityUser();
        }

        [Test]
        public void Email_Invalido_Deve_Falhar_Validacao()
        {
            // Arrange
            user.Email = "email-invalido";

            // Act
            var validator = new EmailAddressAttribute();
            var resultado = validator.IsValid(user.Email);

            // Assert
            Assert.IsFalse(resultado, "O email inválido passou na validação.");
        }

        [Test]
        public void Email_Valido_Deve_Passar_Validacao()
        {
            // Arrange
            user.Email = "teste@exemplo.com";

            // Act
            var validator = new EmailAddressAttribute();
            var resultado = validator.IsValid(user.Email);

            // Assert
            Assert.IsTrue(resultado, "O email válido falhou na validação.");
        }
    }
}
