using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using ApplicationLayer.Features.AuthAndPermissions.UserPermission.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Common;

namespace ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers
{
    public abstract class AuthHandlerBase
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IConfiguration _configuration;
        protected readonly JwtSettings _jwtSettings;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly HttpClient _httpClient;
        protected readonly EmailSettingsDto _emailSettings;
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;

        protected AuthHandlerBase(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IOptions<EmailSettingsDto> emailSettings,
            ILogger logger,
            IMediator mediator,
            IOptions<JwtSettings> jwtOptions = null)
        {
            _userManager = userManager;
            _uow = uow;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _mediator = mediator;
            _jwtSettings = jwtOptions?.Value ?? new JwtSettings
            {
                Key = _configuration["Jwt:Key"] ?? string.Empty,
                Issuer = _configuration["Jwt:Issuer"] ?? string.Empty,
                Audience = _configuration["Jwt:Audience"] ?? string.Empty,
                ExpirationMinutes = int.TryParse(_configuration["Jwt:ExpirationMinutes"], out var m) ? m : 60,
                RefreshTokenExpirationDays = int.TryParse(_configuration["Jwt:RefreshTokenExpirationDays"], out var d) ? d : 7
            };
        }

        protected async Task<string> GenerateJwtTokenAsync(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim("FullName", user.FullName ?? string.Empty),
                new Claim("TokenVersion", user.TokenVersion.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Get permissions via mediator which returns Response<IEnumerable<string>>
            var permResponse = await _mediator.Send(new GetUserPermissionCodesQuery { UserId = user.Id });
            IEnumerable<string> permissions = Enumerable.Empty<string>();
            if (permResponse != null && permResponse.Data != null)
            {
                permissions = permResponse.Data;
            }

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key ?? throw new InvalidOperationException("JWT Key not configured")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpirationMinutes)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(hashBytes);
        }

        protected async Task<string> GenerateRefreshTokenAsync(Guid userId, string? ipAddress, string? userAgent, string jwtId)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = HashToken(refreshToken),
                JwtId = jwtId,
                ExpiresOn = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtSettings.RefreshTokenExpirationDays)),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                UserAgent = userAgent
            };

            await _uow.RefreshTokens.AddAsync(refreshTokenEntity);
            await _uow.CompleteAsync();

            return refreshToken;
        }

        protected ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key ?? throw new InvalidOperationException("JWT Key not configured"))),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get principal from expired JWT token");
                return null;
            }
        }
    }
}
