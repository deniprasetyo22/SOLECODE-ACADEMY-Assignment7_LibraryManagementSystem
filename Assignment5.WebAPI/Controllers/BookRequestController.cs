using Asp.Versioning;
using Assignment5.Domain.Models;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Persistence.Models;
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
        [HttpGet]
        public async Task<IActionResult> GetAllBookRequest()
        {
            var requests = await _bookRequestService.GetAllBookRequestAsync();

            return Ok(new {status = "Success!", data = requests });
        }
    }
}
