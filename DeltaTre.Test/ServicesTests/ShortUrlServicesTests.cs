using DeltaTre.Core;
using DeltaTre.Core.DomainModels;
using DeltaTre.Core.IHelpers;
using DeltaTre.Core.VM;
using DeltaTre.Persistence.Helpers;
using DeltaTre.Persistence.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DeltaTre.Test.ServicesTests
{
    [TestFixture]
    public class ShortUrlServicesTests
    {
        private Mock<IUnitOfWork> _uow;
        private Mock<IUtilities> _utilities;

        public ShortUrlServicesTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _utilities = new Mock<IUtilities>();
        }

        [Test]
        public void SaveUrl_WhenTheUrlIsNotOnline_ReturnMessageNotOnline()
        {
            //Arrange
            var utilities = new Utilities();

            //Act
            var shortUrlServices = new ShortUrlServices(_uow.Object , utilities);
            var result = shortUrlServices.SaveUrl("https://consent.yahoo.com/collectConsent?sessionId=1_cc-session_784f6a09-07d7-41e1-b3e7-aa40f356cd1e&lang=en-GB&inline=false");

            //Assert
            Assert.That(result , Is.EqualTo("Sorry the URL you entered is not live") );
        }


        [Test]
        public void SaveUrl_WhenTheUrlIsOnline_GetRandomUrlAndCompleteShouldBeTouched()
        {

            //Act
            _utilities.Setup(x => x.IsValidUri("")).Returns(Task.FromResult(true));
            _uow.Setup(x => x.ShortUrlRepository.IsShortUrlExists("")).Returns(false);
            var shortUrlServices = new ShortUrlServices(_uow.Object, _utilities.Object);
            var saveResult = shortUrlServices.SaveUrl("");


            //Assert
            _utilities.Verify(x => x.GetRandomUrl(), Times.Once);
            _uow.Verify(x => x.Complete(), Times.Once);
            Assert.That(saveResult, Is.EqualTo(""));
        }

        [Test]
        public void GetTheResponseUrl_WhenTheShortLinkNotExists_ReturnTheUrlYouEnterEDIsNotExist()
        {
            //Arrange
            var responseResult = new ResponseResultVm
            {
                ErrorMessage = "The Url you entered is not exist",
                ResponseUrl = null
            };
            
            //Act
            _uow.Setup(x => x.ShortUrlRepository.GetByShortKey("")).Returns((ShortUrl)null);
            var shortUrlServices = new ShortUrlServices(_uow.Object, _utilities.Object);
            var result   = shortUrlServices.GetTheResponseUrl("" , 3);
            _utilities.Verify(x=>x.GetTheResponseFromTheRequestedUrl("") , Times.Never );

            //Assert
            Assert.That(result.ErrorMessage , Is.EqualTo(responseResult.ErrorMessage));
            Assert.That(result.ResponseUrl, Is.EqualTo(responseResult.ResponseUrl));
        }

        [Test]
        public void GetTheResponseUrl_WhenTheLinkIsNotValidAnyMore_ReturnTheUrlYouEnterEDNotValidAnyMore()
        {
            //Arrange
            var responseResult = new ResponseResultVm
            {
                ErrorMessage = "The Url you entered is not exist",
                ResponseUrl = null
            };



            //Act
            _uow.Setup(x => x.ShortUrlRepository.GetByShortKey("")).Returns(new ShortUrl());
            _utilities.Setup(x => x.MonthsDifferenceBetweenTwoDates(new DateTime(), new DateTime())).Returns(4);
            var shortUrlServices = new ShortUrlServices(_uow.Object, _utilities.Object);
            var result = shortUrlServices.GetTheResponseUrl("", 3);
            _utilities.Verify(x => x.GetTheResponseFromTheRequestedUrl(""), Times.Never);


            //Assert
            Assert.That(result.ErrorMessage, Does.Contain("is not live"));
            Assert.That(result.ResponseUrl, Is.EqualTo(responseResult.ResponseUrl));
        }

        [Test]
        public void GetTheResponseUrl_WhenTheShortLinkExistsAnyTheOriginalLinkIsNotLive_ReturnTheOriginal()
        {
            //Act
            _uow.Setup(x => x.ShortUrlRepository.GetByShortKey("")).Returns(new ShortUrl());
            _utilities.Setup(x => x.GetTheResponseFromTheRequestedUrl("")).Returns(Task.FromResult<string>(null));
            var shortUrlServices = new ShortUrlServices(_uow.Object, _utilities.Object);
            var result = shortUrlServices.GetTheResponseUrl("", 3);
            
            //Assert
            Assert.That(result.ErrorMessage, Does.Contain("is not live"));
            Assert.That(result.ResponseUrl, Is.EqualTo(null));
        }

    }
}
