using Asp.Versioning;
using Assignment5.Domain.Models;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment7.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class BookRequestController : ControllerBase
    {
        private readonly IBookRequestService _bookRequestService;
        public BookRequestController(IBookRequestService bookRequestService)
        {
            _bookRequestService = bookRequestService;
        }

        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Library Manager, Librarian, Library User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bookrequest bookRequest)
        {
            if (bookRequest == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            try
            {
                var addBookRequest = await _bookRequestService.AddBookRequestAsync(bookRequest);

                return Ok(new { status = "Success!", data = addBookRequest });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpPut("approval/{processId}")]
        public async Task<IActionResult> ApprovalAsync(int processId, [FromBody] Process process)
        {
            if (process == null)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            try
            {
                await _bookRequestService.ApprovalAsync(processId, process);
                return Ok(new { status = "Success!", message = "Book request processed successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookRequest()
        {
            var requests = await _bookRequestService.GetAllBookRequestsAsync();

            return Ok(new {status = "Success!", data = requests });
        }
    }
}
