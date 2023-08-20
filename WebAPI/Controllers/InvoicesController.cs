using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace arde_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("getalldetailsbyinvoiceid")]
        public IActionResult GetAllDetailsByInvoiceId(int invoiceId)
        {
            var result = _invoiceService.GetAllDetailsByInvoiceId(invoiceId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getalllist")]
        public IActionResult GetAllList()
        {
            var result = _invoiceService.GetAllList();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add(InvoiceAddDto invoiceDetail)
        {
            var result = _invoiceService.Add(invoiceDetail);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int invoiceId)
        {
            var result = _invoiceService.Delete(invoiceId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult Update(InvoiceAddDto invoiceDetail)
        {
            var result = _invoiceService.Update(invoiceDetail);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
