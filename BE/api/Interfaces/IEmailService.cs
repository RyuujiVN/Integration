using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string toEmail, string otp);
    }
}