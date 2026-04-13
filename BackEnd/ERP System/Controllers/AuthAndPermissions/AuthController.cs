using ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models;
using ApplicationLayer.Features.AuthAndPermissions.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ERP_System.Controllers; // AppControllerBase

namespace API_ERP_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : AppControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
         IConfiguration configuration,
         ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new RegisterCommand { RegisterDto = registerDto };
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [EnableRateLimiting("auth")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            var loginCommand = new ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Models.LoginCommand { LoginDto = loginDto, IpAddress = ipAddress, UserAgent = userAgent };
            var response = await Mediator.Send(loginCommand);
            
            if (response.Succeeded && response.Data != null)
            {
                // إرسال الـ tokens في HttpOnly cookies
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // استخدم true في production مع HTTPS
                    SameSite = SameSiteMode.None, // مهم للـ CORS
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60) // نفس مدة الـ refresh token
                };

                Response.Cookies.Append("accessToken", response.Data.Token, cookieOptions);
                
                var refreshTokenCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                };
                
                Response.Cookies.Append("refreshToken", response.Data.RefreshToken, refreshTokenCookieOptions);

                // إرجاع بيانات المستخدم فقط بدون الـ tokens
                var responseWithoutTokens = new
                {
                    User = response.Data.User,
                    ExpiresAt = response.Data.ExpiresAt
                };

                return Ok(new { IsSuccess = true, Data = responseWithoutTokens, Message = response.Message });
            }

            return NewResult(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            // قراءة الـ tokens من cookies بدلاً من body
            var accessToken = Request.Cookies["accessToken"];
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Tokens not found in cookies." });
            }

            var refreshTokenDto = new RefreshTokenRequestDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();

            var refreshCommand = new RefreshTokenCommand { RefreshTokenRequestDto = refreshTokenDto, IpAddress = ipAddress, UserAgent = userAgent };
            var response = await Mediator.Send(refreshCommand);
            
            if (response.Succeeded && response.Data != null)
            {
                // تحديث الـ cookies بالـ tokens الجديدة
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                };

                Response.Cookies.Append("accessToken", response.Data.Token, cookieOptions);
                
                var refreshTokenCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                };
                
                Response.Cookies.Append("refreshToken", response.Data.RefreshToken, refreshTokenCookieOptions);

                // إرجاع بيانات المستخدم فقط بدون الـ tokens
                var responseWithoutTokens = new
                {
                    User = response.Data.User,
                    ExpiresAt = response.Data.ExpiresAt
                };

                return Ok(new { IsSuccess = true, Data = responseWithoutTokens, Message = response.Message });
            }

            return NewResult(response);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid user." });
            }

            var response = await Mediator.Send(new ChangePasswordCommand { UserId = userId, ChangePasswordDto = changePasswordDto });
            return NewResult(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(new ForgotPasswordCommand { ForgotPasswordDto = forgotPasswordDto });
            return NewResult(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Decode token if it's URL-encoded (from email link copied to POST body)
            if (!string.IsNullOrEmpty(resetPasswordDto.Token) && resetPasswordDto.Token.Contains("%"))
            {
                try
                {
                    resetPasswordDto.Token = Uri.UnescapeDataString(resetPasswordDto.Token);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to decode URL-encoded token for password reset. Email: {Email}", resetPasswordDto.Email);
                    // If decoding fails, use original token
                }
            }

            var response = await Mediator.Send(new ResetPasswordCommand { ResetPasswordDto = resetPasswordDto });
            return NewResult(response);
        }


        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Decode token if it's URL-encoded (from email link copied to POST body)
            if (!string.IsNullOrEmpty(confirmEmailDto.Token) && confirmEmailDto.Token.Contains("%"))
            {
                confirmEmailDto.Token = Uri.UnescapeDataString(confirmEmailDto.Token);
            }

            var response = await Mediator.Send(new ConfirmEmailCommand { ConfirmEmailDto = confirmEmailDto });
            return NewResult(response);
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailDto resendConfirmationEmailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(new ResendConfirmationEmailCommand { ResendConfirmationEmailDto = resendConfirmationEmailDto });
            return NewResult(response);
        }
 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // قراءة الـ refresh token من cookie
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                // حتى لو ما في refresh token، احذف الـ cookies
                Response.Cookies.Delete("accessToken");
                Response.Cookies.Delete("refreshToken");
                return Ok(new { IsSuccess = true, Message = "Logged out successfully." });
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var response = await Mediator.Send(new LogoutCommand { RefreshToken = refreshToken, IpAddress = ipAddress });
            
            // حذف الـ cookies بعد الـ logout
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            
            return NewResult(response);
        }

    }
}
