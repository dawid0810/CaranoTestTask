using System;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using PriceParser.BusinessCommon;
using PriceParser.Service;

namespace UnitTests
{
    public class PriceParserServiceTest
    {
        private PriceParserService _service;
        private AutoMock _mock;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetStrict();
            _mock.VerifyAll = true;
            _service = new PriceParserService(_mock.Mock<IPriceParser>().Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mock.Dispose();
        }

        [Test]
        public void ReturnsSuccessOnNoException()
        {
            // arrange
            const string priceInWords = "Some price in words";
            _mock.Mock<IPriceParser>().Setup(x => x.ConvertPriceToWords(It.IsAny<decimal>()))
                .Returns(priceInWords);
            
            // act
            var result = _service.ParsePrice(1m);
            
            // assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(priceInWords, result.Value);
        }
        
        [Test]
        public void ReturnsErrorOnException()
        {
            // arrange
            var exception = new Exception("Test exception");
            _mock.Mock<IPriceParser>().Setup(x => x.ConvertPriceToWords(It.IsAny<decimal>()))
                .Throws(exception);
            
            // act
            var result = _service.ParsePrice(1m);
            
            // assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(exception, result.Error);
        }
    }
}