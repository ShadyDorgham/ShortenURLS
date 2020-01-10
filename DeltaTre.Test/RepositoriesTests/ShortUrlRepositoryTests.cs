using DeltaTre.Core;
using DeltaTre.Core.DomainModels;
using DeltaTre.Persistence.Repositories;
using DeltaTre.Test.Extensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;

namespace DeltaTre.Test.RepositoriesTests
{
    [TestFixture]
    public class ShortUrlRepositoryTests
    {
        private ShortUrlRepository _shortUrlRepository;
        private Mock<DbSet<ShortUrl>> _mockShortUrls;
        private Mock<IDataContext> _mockContext;
        private const string ShortUrlToBeTested = "958oin4l";



        [SetUp]
        public void TestInitialize()
        {
            //Arrange
            _mockShortUrls = new Mock<DbSet<ShortUrl>>();
            _mockContext = new Mock<IDataContext>();
            _mockContext.Setup(x => x.ShortUrls).Returns(_mockShortUrls.Object);
            _shortUrlRepository = new ShortUrlRepository(_mockContext.Object);
        }

        [Test]
        public void GetValidUrls_WhenShortUrlIsNotValidReturns_ListOfZero()
        {
            //Arrange
            var shortUrl = new ShortUrl { Id = 1, OriginalUrl = "Google.com", ShortenUrl = ShortUrlToBeTested, InsertDate = DateTime.Now.AddMonths(-4) };
            
            _mockShortUrls.SetSource(new[] {shortUrl});

            //Act
            var shortUrls = _shortUrlRepository.GetValidUrls(3);

            //Assert
            Assert.That(shortUrls.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetValidUrls_WhenShortUrlIsNotValidReturns_ListOfOne()
        {
            //Arrange
            var shortUrl = new ShortUrl {InsertDate = DateTime.Now.AddMonths(-1)};
          
            _mockShortUrls.SetSource(new[] {shortUrl});

            //Act
            var shortUrls = _shortUrlRepository.GetValidUrls(3);

            //Assert
            Assert.That(shortUrls.Count, Is.EqualTo(1));

        }



        [Test]
        public void IsShortUrlExists_WhenShortUrlExists_ReturnsTrue()
        {
            //Arrange
            var shortUrl = new ShortUrl { ShortenUrl = ShortUrlToBeTested };
            _mockShortUrls.SetSource(new[] { shortUrl });
            //Act
            var isUrlExists = _shortUrlRepository.IsShortUrlExists(ShortUrlToBeTested);
            //Assert
            Assert.That(isUrlExists, Is.True);
        }


        [Test]
        public void IsShortUrlExists_WhenShortUrlNotExists_ReturnsFalse()
        {
            //Arrange
            var shortUrl = new ShortUrl { ShortenUrl = ShortUrlToBeTested };
            _mockShortUrls.SetSource(new[] { shortUrl });
            //Act
            var isUrlExists = _shortUrlRepository.IsShortUrlExists("");
            //Assert
            Assert.That(isUrlExists, Is.False);
        }


        [Test]
        public void GetByShortKey_WhenShortUrlExists_ReturnsTrue()
        {
            //Arrange
            var shortUrl = new ShortUrl { ShortenUrl = ShortUrlToBeTested };
            _mockShortUrls.SetSource(new[] { shortUrl });
            //Act
            var heExistingUrlExists = _shortUrlRepository.GetByShortKey(ShortUrlToBeTested);
            //Assert
            Assert.That(heExistingUrlExists.ShortenUrl, Is.EqualTo(ShortUrlToBeTested));
        }


        [Test]
        public void GetByShortKey_WhenShortUrlNotExists_ReturnsFalse()
        {
            //Arrange
            var shortUrl = new ShortUrl { ShortenUrl = ShortUrlToBeTested };
            _mockShortUrls.SetSource(new[] { shortUrl });
            //Act
            var heExistingUrlExists = _shortUrlRepository.GetByShortKey("");
            //Assert
            Assert.That(heExistingUrlExists, Is.Null);
        }


    }
}
