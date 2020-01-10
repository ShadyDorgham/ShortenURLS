using DeltaTre.Persistence.Helpers;
using NUnit.Framework;
using System;

namespace DeltaTre.Test.PersistenceTest.Helpers
{
    [TestFixture]
   public class UtilitiesTests
    {
        private readonly Utilities _utilities;

        public UtilitiesTests()
        {
            _utilities = new Utilities();
        }

        
        [Test]
        [TestCase("https://consent.yahoo.com/collectConsent?sessionId=1_cc-session_784f6a09-07d7-41e1-b3e7-aa40f356cd1e&lang=en-GB&inline=false", false)]
        [TestCase("http://www.yahoo.com/", true)]
        [TestCase("http://www.google.com", true)]
        public void IsValidUri_TheUrlIsOnLine_ReturnTrueElseFalse(string url, bool expectedResult)
        {
            //Arrange
            var result = _utilities.IsValidUri(url).Result;
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetTheResponseFromTheRequestedUrl_TheUrlIsOnLine_ReturnsAstring()
        {
            //Arrange
            var result = _utilities.GetTheResponseFromTheRequestedUrl("http://www.google.com").Result;
            //Assert
            Assert.That(result, Does.Not.Null);
        }

        [Test]
        public void GetTheResponseFromTheRequestedUrl_TheUrlIsOffLine_ReturnsNull()
        {
            //Arrange
            var result = _utilities.GetTheResponseFromTheRequestedUrl("https://consent.yahoo.com/collectConsent?sessionId=1_cc-session_784f6a09-07d7-41e1-b3e7-aa40f356cd1e&lang=en-GB&inline=false").Result;
            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetRandomUrl_WhenCalled_AlwaysReturns8Characters()
        {
            // Arrange
            var expectedCount = 8;
            // Act
            var result = _utilities.GetRandomUrl();

            // Assert
            Assert.That(expectedCount, Is.EqualTo(result.Length));
        }


        [Test]
        [TestCase( "2019/09/01","2019/06/01" , 3)]
        [TestCase("2020/01/01", "2019/01/01", 12)]
        public void MonthsDifferenceBetweenTwoDates(DateTime start , DateTime end , int differenceInMonths)
        {
            // Act
            var result = _utilities.MonthsDifferenceBetweenTwoDates(start , end );

            // Assert
            Assert.That(result , Is.EqualTo(differenceInMonths));
        }

    }
}
