using System;
using NUnit.Framework;
using PriceParser.BusinessCommon;

namespace UnitTests
{
    [TestFixture]
    public class NumberParserTest
    {
        private NumberParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new NumberParser();
        }


        [TestCase(0, "zero")]
        [TestCase(1, "one")]
        [TestCase(25, "twenty-five")]
        [TestCase(125, "one hundred twenty-five")]
        [TestCase(100, "one hundred")]
        [TestCase(45100, "forty-five thousand one hundred")]
        [TestCase(65109, "sixty-five thousand one hundred nine")]
        [TestCase(999999999, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
        [TestCase(999999900, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred")]
        [TestCase(999999002, "nine hundred ninety-nine million nine hundred ninety-nine thousand two")]
        [TestCase(999999000, "nine hundred ninety-nine million nine hundred ninety-nine thousand")]
        public void ParsesNumbers(int number, string expectedResult)
        {
            // arrange

            //act
            var result = _parser.ConvertNumberToWords(number);

            //assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        public void ThrowsArgumentOutOfRangeException_LessThan0_MoreThan999999999(int number)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _parser.ConvertNumberToWords(number));
        }
    }
}
