using System.ComponentModel.DataAnnotations;

namespace PriceParser.ServiceApi.Models
{
    public class PriceRequestModel
    {
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage =  "Invalid price format")]
        [Range(0, 999999999, ErrorMessage = "Price must be in between 0 and 999 999 999")]
        public decimal Price { get; set; }
    }
}