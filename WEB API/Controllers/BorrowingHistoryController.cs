using BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingHistoryController : ControllerBase
    {
        private readonly IBorrowingHistoryService _service;

        public BorrowingHistoryController(IBorrowingHistoryService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("CalculateLateFee/{id}")]
        public IActionResult CalculateLateFee(int id)
        {
            try
            {
                var lateFee = _service.CalculateLateFee(id);
                return Ok(new { LateFee = lateFee });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ReturnBook/{id}")]
        public IActionResult ReturnBook(int id)
        {
            try
            {
                _service.ReturnBook(id);
                return Ok(new { Message = "Book returned successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
