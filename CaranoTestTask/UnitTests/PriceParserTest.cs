using System;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using PriceParser.BusinessCommon;

namespace UnitTests
{
    [TestFixture]
    public class PriceParserTest
    {
        private PriceParser.BusinessCommon.PriceParser _parser;
        private AutoMock _mock;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetStrict();
            _mock.VerifyAll = true;
            _parser = new PriceParser.BusinessCommon.PriceParser(_mock.Mock<INumberParser>().Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mock.Dispose();
        }

        [TestCase("0", "zero dollars", "zero", "")]
        [TestCase("1", "one dollar", "one", "")]
        [TestCase("25,1", "twenty-five dollars and ten cents", "twenty-five", "ten")]
        [TestCase("125,1", "one hundred twenty-five dollars and ten cents", "one hundred twenty-five", "ten")]
        [TestCase("100", "one hundred dollars", "one hundred", "")]
        [TestCase("0,01", "zero dollars and one cent", "zero", "one")]
        [TestCase("45 100", "forty-five thousand one hundred dollars", "forty-five thousand one hundred", "")]
        [TestCase("45100", "forty-five thousand one hundred dollars", "forty-five thousand one hundred", "")]
        [TestCase("999 999 999,99",
            "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents",
            "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine",
            "ninety-nine")]
        public void ParsesPrices(string price, string expectedResult, string dollarString, string centsString)
        {
            // arrange
            _mock.Mock<INumberParser>().SetupSequence(x => x.ConvertNumberToWords(It.IsAny<int>()))
                .Returns(dollarString).Returns(centsString);

            //act
            var result = _parser.ConvertPriceToWords(price);

            //assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("asd")]
        [TestCase("123.34")]
        [TestCase("123,345")]
        [TestCase(",34")]
        [TestCase("12,34a")]
        public void ThrowsArgumentOutOfRangeException_WrongStringFormat(string priceString)
        {
            Assert.Throws<ArgumentException>(() => _parser.ConvertPriceToWords(priceString));
        }

        [Test]
        public void ThrowsNumberParserExceptions()
        {
            //arrange
            _mock.Mock<INumberParser>().Setup(x => x.ConvertNumberToWords(It.IsAny<int>())).Throws(new Exception());

            //act

            //assert
            Assert.Throws<Exception>(() => _parser.ConvertPriceToWords("1"));
        }
    }
}
