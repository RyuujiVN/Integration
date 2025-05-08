using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ICacheService
    {
        void SetOtp(string email, string otp);
        string GetOtp(string email);
        void RemoveOtp(string email);
    }

}