namespace PriceParser.BusinessCommon
{
    public interface IPriceParser
    {
        string ConvertPriceToWords(decimal price);
    }
}
