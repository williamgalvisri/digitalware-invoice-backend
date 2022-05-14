using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;
using ApiDigitalWare.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ApiDigitalWare.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private CustomerInterface _customerInferface;
        public CustomerController(CustomerInterface customerInferface)
        {
            _customerInferface = customerInferface;
        }

        [HttpGet("get-customers")]
        public IActionResult GetCustomers()
        {
            try
            {
                List<TbCustomer> customers = _customerInferface.GetCustomers();
                var data = new
                {
                    customers
                };
                object result = ResponsesUtilities.ParseResponse(200, "Customer Fetched", data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new {});
                return BadRequest(result);
            }
        }

    }


}