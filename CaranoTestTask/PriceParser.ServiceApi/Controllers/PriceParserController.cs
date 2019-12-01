using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceParser.Service;
using PriceParser.ServiceApi.Models;

namespace PriceParser.ServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceParserController : Controller
    {
        private readonly IPriceParserService _priceParserService;

        public PriceParserController(IPriceParserService priceParserService)
        {
            _priceParserService = priceParserService;
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return Ok("Price Parser Api started");
        }

        [HttpPost]
        public ActionResult Parse([FromBody] PriceRequestModel priceRequest)
        {
            var result = _priceParserService.ParsePrice(priceRequest.PriceString);
            
            if (result.IsSuccess)
            {
                return new JsonResult(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}