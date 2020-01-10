using DeltaTre.Controllers;
using DeltaTre.Core.DomainModels;
using DeltaTre.Persistence;
using DeltaTre.Persistence.Helpers;
using DeltaTre.Persistence.Repositories;
using DeltaTre.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DeltaTre.Tests.Integration.Controllers
{
    [TestFixture]
    [Isolated]
    public class HomeControllerTests 
    {
        private HomeController _homeControllerl;
        private DataContext _context;

        private Mock<IConfiguration> _mockConfiguration;

        [SetUp]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlite("Data Source=./ShortUrlsDeltaTre.db");
            _context = new DataContext(builder.Options);


            _mockConfiguration = new Mock<IConfiguration>();


        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void SaveUrl_WhenInsertAnOnLineUrl_ShouldBeInsertedIntoDataBase()
        {
            var homeController = new HomeController(configuration: _mockConfiguration.Object,
                services: new Services(unitOfWork: new UnitOfWork(ctx: _context), utilities: new Utilities()));
            var originalUrlName = "https://www.google.com/";

            homeController.SaveUrl(originalUrl: originalUrlName);

            var query = _context.ShortUrls.SingleOrDefault(x => x.OriginalUrl == originalUrlName);

            Assert.That(query, Is.Not.Null);

        }


        [Test]

        public void GetValidUrls_WhenCalled_ReturnsValidOnly()
        {
                    var shortUrlsList = new List<ShortUrl>
                    {
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre1.com", ShortenUrl = "12345678",
                            InsertDate = DateTime.Now.AddMonths(-1)
                        },
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre2.com", ShortenUrl = "12345677",
                            InsertDate = DateTime.Now.AddMonths(-2)
                        },
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre3.com", ShortenUrl = "12345676",
                            InsertDate = DateTime.Now.AddMonths(-3)
                        },
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre4.com", ShortenUrl = "12345675",
                            InsertDate = DateTime.Now.AddMonths(-4)
                        },
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre5.com", ShortenUrl = "12345674",
                            InsertDate = DateTime.Now.AddMonths(-5)
                        },
                        new ShortUrl()
                        {
                            OriginalUrl = "www.DeltaTre6.com", ShortenUrl = "12345673",
                            InsertDate = DateTime.Now.AddMonths(-6)
                        }
                    };


                    _context.ShortUrls.AddRange(shortUrlsList);
                    _context.SaveChanges();


                    var shortUrlRepository = new ShortUrlRepository(_context);

                    var validUrl = shortUrlRepository.GetValidUrls(3).Count;



                    Assert.That(validUrl , Is.EqualTo(3));

        }
    }
}
