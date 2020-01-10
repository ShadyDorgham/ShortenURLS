using DeltaTre.Controllers;
using DeltaTre.Core.IServices;
using DeltaTre.Core.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace DeltaTre.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IServices> _mockServices;

        public HomeControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockServices = new Mock<IServices>();
        }
        [Test]
        public void NotFound_IfThereIsErrorMessages_ReturnBadRequest()
        {
            var responseResultVm = new ResponseResultVm { ErrorMessage = "Errors" };
           

            _mockServices.Setup(x => x.ShortUrlServices.GetTheResponseUrl("", 3)).Returns(responseResultVm);
            _mockConfiguration.Setup(x => x.GetSection("UrlValidMonths").Value).Returns("3");
            var homeController = new HomeController(_mockConfiguration.Object, _mockServices.Object);
            var result = homeController.NotFound("");
            Assert.That(result , Is.TypeOf<BadRequestObjectResult>());
        }
        
        [Test]
        public void NotFound_IfThereIsNonErrorMesages_ReturnRedirect()
        {
            //Arrange
            
            var responseResultVm = new ResponseResultVm { ErrorMessage = null ,  ResponseUrl   = "redirectionUrl" };
    

            //Act
            _mockServices.Setup(x => x.ShortUrlServices.GetTheResponseUrl("", 3)).Returns(responseResultVm);
            _mockConfiguration.Setup(x => x.GetSection("UrlValidMonths").Value).Returns("3");
            var homeController = new HomeController(_mockConfiguration.Object, _mockServices.Object);
            var result = homeController.NotFound("");


            //Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
        

        [Test]
        public void SaveUrl_IfThereIsAnError_ReturnBadRequest()
        {
            //Act
            _mockServices.Setup(x => x.ShortUrlServices.SaveUrl("")).Returns("Error");
            var homeController = new HomeController(_mockConfiguration.Object, _mockServices.Object);
            var result = homeController.SaveUrl("");



            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }


        [Test]
        public void SaveUrl_IfThereIsNoError_ReturnOk()
        {
            //Act
            _mockServices.Setup(x => x.ShortUrlServices.SaveUrl("")).Returns("");
            var homeController = new HomeController(_mockConfiguration.Object,_mockServices.Object);
            var result = homeController.SaveUrl("");

            
            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
