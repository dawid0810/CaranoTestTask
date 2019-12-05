using System;
using System.Text;

namespace PriceParser.BusinessCommon
{
    public class NumberParser : INumberParser
    {
        public string ConvertNumberToWords(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Given number is too small");
            }
            if (number > 999999999)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Given number is too big");
            }

            var sb = new StringBuilder();

            if (number / 1000000 > 0)
            {
                sb.Append($"{ConvertNumberToWords(number / 1000000)} million ");
                number %= 1000000;
            }
            if (number / 1000 > 0)
            {
                sb.Append($"{ConvertNumberToWords(number / 1000)} thousand ");
                number %= 1000;
            }
            if (number / 100 > 0)
            {
                sb.Append($"{ConvertNumberToWords(number / 100)} hundred ");
                number %= 100;
            }

            if (number == 0 && sb.Length > 0)
            {
                return sb.ToString().Trim();
            }

            sb.Append(ConvertTensAndUnits(number));

            return sb.ToString().Trim();
        }

        private string ConvertTensAndUnits(int number)
        {
            var sb = new StringBuilder();
            var unitsStrings = new[]
            {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
            };
            var tensStrings = new[] { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
            {
                sb.Append(unitsStrings[number]);
            }
            else
            {
                sb.Append(tensStrings[number / 10 - 1]);
                if (number % 10 > 0)
                {
                    sb.Append($"-{unitsStrings[number % 10]}");
                }
            }

            return sb.ToString();
        }
    }
}