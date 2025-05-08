using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        Task<(string AccessToken, string RefreshToken)> CreateTokens(Account user);
        Task<(string AccessToken, string RefreshToken)> RefreshTokens(string refreshToken);
        string CreateRefreshToken();
        Task<string> CreateAccessToken(Account account);  
    }
}