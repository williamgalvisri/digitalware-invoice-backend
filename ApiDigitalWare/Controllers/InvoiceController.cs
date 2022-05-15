using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Core.Entities;
using ApiDigitalWare.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiDigitalWare.Controllers
{
    [ApiController]
    [Route("invoice")]
    public class InvoiceController : ControllerBase
    {
        private InvoiceInterface _invoiceInterface;
        public InvoiceController(InvoiceInterface _interface)
        {
            _invoiceInterface = _interface;
        }

        /// <summary>
        /// Method to get invoices
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult GetInvoices()
        {
            try
            {
                List<InvoicesWithInformationCustomersEntity> invoices = _invoiceInterface.GetInovices();
                var data = new
                {
                    invoices
                };
                object result = ResponsesUtilities.ParseResponse(200, "Invoice fetched", data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Method to get a invoice by (<paramref name="id"/>).
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <param name="id">id invoice.</param>
        [HttpGet("{id}")]
        public IActionResult GetInvoiceById(decimal id)
        {
            try
            {
                CompleteInvoiceDataEntity invoice = _invoiceInterface.GetInvoiceById(id);
                var data = new
                {
                    invoice
                };
                object result = ResponsesUtilities.ParseResponse(200, "Invoice fetched", data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Method to create a invoice with (<paramref name="header"/>, <paramref name="detail"/>).
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <param name="header">Schema TbInvoice</param>,
        /// <param name="detail">Schema TbInvoice</param>
        [HttpPost]
        public IActionResult CreateInvoice(InvoicePayloadEntity payload)
        {
            try
            {
                _invoiceInterface.CreateInvoice(payload);
                object result = ResponsesUtilities.ParseResponse(200, "Inovice created", new {});
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new {});
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Method to updated a invoice by (<paramref name="id"/>).
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <param name="id">id invoice.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(decimal id, InvoicePayloadEntity payload)
        {
            try
            {
                _invoiceInterface.UpdateInvoice(id, payload);
 
                object result = ResponsesUtilities.ParseResponse(200, "Invoice updated", new {});
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Method to delete a invoice by (<paramref name="id"/>).
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <param name="id">id invoice.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(decimal id)
        {
            try
            {
                _invoiceInterface.DeleteInvoice(id);
                object result = ResponsesUtilities.ParseResponse(200, "Customer Delete", new {});
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Method to get consecutive of a invoice
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("new-consecutive")]
        public IActionResult GenerateConsecutive()
        {
            try
            {
                Guid consecutive = _invoiceInterface.GenerateConsecutive();
                var data = new
                {
                    consecutive
                };
                object result = ResponsesUtilities.ParseResponse(200, "Consecutive generated", data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                object result = ResponsesUtilities.ParseResponse(500, ex.Message, new { });
                return BadRequest(result);
            }
        }


        /// <summary>
        /// Method to get a invoice by (<paramref name="consecutive"/>).
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <param name="consecutive">consecutive invoice.</param>
        [HttpGet("get-by-consecutive/{consecutive}")]
        public IActionResult GetInvoiceByConsecutive(Guid consecutive)
        {
            try
            {
                CompleteInvoiceDataEntity invoice = _invoiceInterface.GetInvoiceByConsecutive(consecutive);
                var data = new
                {
                    invoice
                };
                object result = ResponsesUtilities.ParseResponse(200, "Invoice fetched", data);
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