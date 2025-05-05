using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{

    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync(QueryObjectAccount query);
        Task<Account?> GetByUsernameAsync(string username);
        Task<Account?> GetByEmailAsync(string email);
        Task<Account> CreateAsync(Account accountModel, string password);
        Task<Account?> UpdateAsync(string username, Account accountDto);
        Task<Account?> DeleteAsync(string username);
        Task<bool> UpdateRoleAsync(string username, string roleName); 
        Task<string?> GetUserRoleAsync(string username);
    }

}