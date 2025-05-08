using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Account> _userManager;

        public TokenService(IConfiguration config, UserManager<Account> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
        }

        public async Task<(string AccessToken, string RefreshToken)> CreateTokens(Account account)
        {
            var accessToken = await CreateToken(account, TokenType.Access);
            var refreshToken = await CreateToken(account, TokenType.Refresh);

            account.RefreshToken = refreshToken;
            account.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                Convert.ToDouble(_config["JWT:RefreshTokenExpiryInDays"]));
            await _userManager.UpdateAsync(account);

            return (accessToken, refreshToken);
        }

        private async Task<string> CreateToken(Account account, TokenType tokenType)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, account.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, account.UserName),
            new Claim(JwtRegisteredClaimNames.Email, account.Email),
            new Claim("fullName", account.FullName ?? "")
        };

            var roles = await _userManager.GetRolesAsync(account);
            var role = roles.FirstOrDefault();
            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var expires = tokenType == TokenType.Access
                ? DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JWT:AccessTokenExpiryInMinutes"]))
                : DateTime.UtcNow.AddDays(Convert.ToDouble(_config["JWT:RefreshTokenExpiryInDays"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            throw new NotImplementedException("Method not used anymore");
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokens(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["JWT:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature))
                throw new SecurityTokenException("Invalid token");

            var userId = principal.FindFirstValue(JwtRegisteredClaimNames.NameId);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.RefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new SecurityTokenException("Refresh token has expired");

            // Tạo access token mới
            var newAccessToken = await CreateToken(user, TokenType.Access);
            var newRefreshToken = await CreateToken(user, TokenType.Refresh);

            // Cập nhật refresh token mới trong db
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                Convert.ToDouble(_config["JWT:RefreshTokenExpiryInDays"]));
            await _userManager.UpdateAsync(user);

            return (newAccessToken, newRefreshToken);
        }

        public async Task<string> CreateAccessToken(Account account)
        {
            return await CreateToken(account, TokenType.Access);
        }

        private enum TokenType
        {
            Access,
            Refresh
        }
    }
}