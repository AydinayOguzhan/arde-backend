using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
