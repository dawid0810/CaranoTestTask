using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceParser.Service;

namespace PriceParser.ServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceParserController : ControllerBase
    {
        private readonly IPriceParserService _priceParserService;

        public PriceParserController(IPriceParserService priceParserService)
        {
            _priceParserService = priceParserService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Welcome()
        {
            return Ok("Price Parser Api started");
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult<string> Withdraw([FromBody] string priceString)
        {
            var result = _priceParserService.ParsePrice(priceString);

            return new JsonResult(result);
        }
    }
}