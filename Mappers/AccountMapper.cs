using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;

namespace api.Mappers
{
    public static class AccountMapper
    {
        public static AccountDto ToAccountDto(this Account accountModel)
        {
            return new AccountDto
            {
                Id = accountModel.Id,
                FullName = accountModel.FullName,
                Email = accountModel.Email,
                DateOfBirth = accountModel.DateOfBirth,
                LastLogin = accountModel.LastLogin
            };
        }
        public static Account ToAccountFromUpdate(this UpdateAccountDto accountDto)
        {
            return new Account
            {
                FullName = accountDto.FullName,
                DateOfBirth = accountDto.DateOfBirth,
                UpdatedAt = DateTime.UtcNow
            };
        }
        public static Account ToAccountFromRegister(this CreateAccountDto accountDto)
        {
            return new Account
            {
                FullName = accountDto.FullName,
                Email = accountDto.Email,
                UserName = accountDto.Email,
                DateOfBirth = accountDto.DateOfBirth,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };
        }
    }
}