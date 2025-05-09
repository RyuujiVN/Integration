using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class UpdateAccountDto
    {
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}