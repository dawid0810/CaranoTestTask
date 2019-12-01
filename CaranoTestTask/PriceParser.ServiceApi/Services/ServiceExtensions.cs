using Microsoft.Extensions.DependencyInjection;
using PriceParser.BusinessCommon;
using PriceParser.Service;

namespace PriceParser.ServiceApi.Services
{
    public static class ServiceExtensions
    {
        public static void RegisterPriceParserServices(this IServiceCollection services)
        {
            services.AddScoped<IPriceParserService, PriceParserService>();
            services.AddScoped<IPriceParser, BusinessCommon.PriceParser>();
            services.AddScoped<INumberParser, NumberParser>();
        }
    }
}