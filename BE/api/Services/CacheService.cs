using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace api.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _otpLifetime = TimeSpan.FromMinutes(5);

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void SetOtp(string email, string otp)
        {
            _cache.Set(GetKey(email), otp, _otpLifetime);
        }

        public string GetOtp(string email)
        {
            return _cache.Get<string>(GetKey(email));
        }

        public void RemoveOtp(string email)
        {
            _cache.Remove(GetKey(email));
        }

        private string GetKey(string email) => $"OTP_{email}";
    }
}