using Asp.Versioning;
using Assignment7.Application.DTOs.Account;
using Assignment7.Application.Interfaces.IService;
using Assignment7.Domain.Models.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment5.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.SignUpAsync(model);

            if (result.Status == "Error")
            {
                return BadRequest(new AuthResponse { Status = "Error", Message = result.Message });
            }

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (result.Status == "Error")
            {
                return Unauthorized(new { status = result.Status, message = result.Message });
            }

            SetRefreshTokenCookie("AuthToken", result.Token, result.TokenExpiresOn);
            SetRefreshTokenCookie("RefreshToken", result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);

        }
        private void SetRefreshTokenCookie(string tokenType, string? token, DateTime? expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  // Hanya dapat diakses oleh server
                Secure = true,    // Hanya dikirim melalui HTTPS
                SameSite = SameSiteMode.Strict, // Cegah serangan CSRF
                Expires = DateTime.Now.AddDays(3) // Waktu kadaluarsa token
            };

            Response.Cookies.Append(tokenType, token, cookieOptions);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("set-role")]
        public async Task<IActionResult> CreateRoleAsync(string rolename)
        {
            var result = await _authService.CreateRoleAsync(rolename);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignToRoleAsync(string userName, string rolename)
        {
            var result = await _authService.AssignToRoleAsync(userName, rolename);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Hapus cookie
                Response.Cookies.Delete("AuthToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Delete("RefreshToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok("Logout successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred during logout");
            }
        }



    }
}
