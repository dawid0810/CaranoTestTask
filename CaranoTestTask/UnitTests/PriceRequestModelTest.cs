using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using PriceParser.ServiceApi.Models;

namespace UnitTests
{
    public class PriceRequestModelTest
    {
        [TestCase("1")]
        [TestCase("1.01")]
        [TestCase("112312.01")]
        [TestCase("0.01")]
        public void ValidationSuccess(decimal price)
        {
            var model = new PriceRequestModel {Price = price};
            Validator.ValidateObject(model, new ValidationContext(model), true);
        }
        
        [TestCase("1.011")]
        [TestCase("-1.01")]
        [TestCase("1000000000")]
        public void ValidationFailure(decimal price)
        {
            var model = new PriceRequestModel {Price = price};
            Assert.Throws<ValidationException>(
                () => Validator.ValidateObject(model, new ValidationContext(model), true));
        }
    }
}