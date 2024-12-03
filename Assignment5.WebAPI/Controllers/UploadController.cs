using Asp.Versioning;
using Assignment7.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment7.WebAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                long MaxFileSize = 2 * 1024 * 1024; // 2MB
                string[] AllowedFileTypes = new[] {
                    "application/pdf",
                    "application/msword",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                };

                if (file == null || file.Length == 0)
                    return BadRequest(new { Message = "File is empty" });

                if (file.Length > MaxFileSize)
                    return BadRequest(new { Message = "File size exceeds 2MB limit" });

                if (!AllowedFileTypes.Contains(file.ContentType))
                    return BadRequest(new { Message = "Only PDF and Word documents are allowed" });

                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file to directory
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new { Message = "File uploaded succesfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
