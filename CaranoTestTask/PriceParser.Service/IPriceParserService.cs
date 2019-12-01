﻿using Common.Results;

namespace PriceParser.Service
{
    public interface IPriceParserService
    {
        Result<string> ParsePrice(string price);
    }
}
