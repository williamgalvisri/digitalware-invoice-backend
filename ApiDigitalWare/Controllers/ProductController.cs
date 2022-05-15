using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;
using ApiDigitalWare.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ApiDigitalWare.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private ProductInterface _productInferface;
        public ProductController(ProductInterface productInferface)
        {
            _productInferface = productInferface;
        }

        [HttpGet("get-products-with-price")]
        public IActionResult GetProducts()
        {
            try
            {
                List<ProductWithPrice> products = _productInferface.GetProductsWithPrice();
                var data = new
                {
                    products
                };
                object result = ResponsesUtilities.ParseResponse(200, "Products Fetched", data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }

    }


}