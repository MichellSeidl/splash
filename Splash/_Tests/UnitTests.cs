using Moq;
using Splash.Controllers;
using Splash.Models;
using Xunit;

namespace Splash._Tests
{
    public class UnitTests
    {
        private readonly IConfiguration _configuration;
        private IndexController _indexController;

        public UnitTests() 
        { 
            _indexController = new IndexController(_configuration);
        }
        
        /// <summary>
        /// Teste unitário para login com objeto usuário nulo
        /// </summary>
        [Fact]
        public void ValidateLogin_ut()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => { _indexController.ValidateUser(new Mock<LoginViewModel>().Object); });
            Assert.Equal("Value cannot be null. (Parameter 's')", ex.Message);
        }
    }
}
