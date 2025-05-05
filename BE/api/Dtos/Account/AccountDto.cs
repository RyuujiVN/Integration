using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class AccountDto
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastLogin { get; set; }
        public string? Role { get; set; }
    }
}