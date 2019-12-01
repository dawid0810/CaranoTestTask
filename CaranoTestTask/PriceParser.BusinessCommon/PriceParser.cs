using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PriceParser.BusinessCommon
{
    public class PriceParser : IPriceParser
    {
        private readonly INumberParser _numberParser;

        public PriceParser(INumberParser numberParser)
        {
            _numberParser = numberParser;
        }

        public string ConvertPriceToWords(string priceString)
        {
            if (!IsPrice(priceString))
            {
                throw new ArgumentException("Given string is not in correct format");
            }

            var price = decimal.Parse(priceString, new CultureInfo("pl"));

            var dollars = (int)price;
            var cents = (int)((price - dollars) * 100);

            var dollarsSuffix = dollars == 1 ? "dollar" : "dollars";
            var centsSuffix = cents == 1 ? "cent" : "cents";

            var sb = new StringBuilder();

            sb.Append($"{_numberParser.ConvertNumberToWords(dollars).Trim()} {dollarsSuffix}");

            var centsString = _numberParser.ConvertNumberToWords(cents);
            if (!string.IsNullOrEmpty(centsString) && cents > 0)
            {
                sb.Append($" and {centsString} {centsSuffix}");
            }

            return sb.ToString();
        }

        private bool IsPrice(string text)
        {
            var regex = new Regex(@"^\d+(,\d{1,2})?$"); //regex that matches disallowed text
            return regex.IsMatch(text.Replace(" ", string.Empty));
        }
    }
}