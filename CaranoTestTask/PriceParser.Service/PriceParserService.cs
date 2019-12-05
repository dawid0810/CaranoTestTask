using System;
using Common.Results;
using PriceParser.BusinessCommon;

namespace PriceParser.Service
{
    public class PriceParserService : IPriceParserService
    {
        private readonly IPriceParser _parser;

        public PriceParserService(IPriceParser parser)
        {
            _parser = parser;
        }

        public Result<string> ParsePrice(decimal price)
        {
            try
            {
                return new Result<string>(_parser.ConvertPriceToWords(price));
            }
            catch (Exception e)
            {
                return new Result<string>(null, e);
            }
        }
    }
}