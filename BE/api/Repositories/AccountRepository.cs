using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Datas;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDBContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(
            AccountDBContext context,
            UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<Account>> GetAllAccountsAsync(QueryObjectAccount query)
        {
            var accounts = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.FullName))
            {
                accounts = accounts.Where(x => x.FullName != null && x.FullName.Contains(query.FullName));
            }

            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                accounts = accounts.Where(x => x.Email != null && x.Email.Contains(query.Email));
            }

            accounts = query.IsDescending
                ? accounts.OrderByDescending(x => x.LastLogin)
                : accounts.OrderBy(x => x.LastLogin);

            return await accounts.ToListAsync();
        }

        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Account> CreateAsync(Account accountModel, string password)
        {
            var result = await _userManager.CreateAsync(accountModel, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Không thể tạo tài khoản: {errors}");
            }
            return accountModel;
        }

        public async Task<Account?> DeleteAsync(string username)
        {
            var account = await _userManager.FindByNameAsync(username);
            if (account == null) return null;

            var result = await _userManager.DeleteAsync(account);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Không thể xóa tài khoản: {errors}");
            }

            return account;
        }


        public async Task<bool> UpdateRoleAsync(string username, string roleName)
        {
            var account = await _userManager.FindByNameAsync(username);
            if (account == null) return false;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return false;

            var currentRoles = await _userManager.GetRolesAsync(account);

            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(account, currentRoles);
            }

            var result = await _userManager.AddToRoleAsync(account, roleName);
            return result.Succeeded;
        }

        public async Task<string?> GetUserRoleAsync(string username)
        {
            var account = await _userManager.FindByNameAsync(username);
            if (account == null) return null;

            var roles = await _userManager.GetRolesAsync(account);
            return roles.FirstOrDefault();
        }
        public async Task<Account?> UpdateAsync(string username, Account accountDto)
        {
            var account = await _userManager.FindByNameAsync(username);
            if (account == null) return null;

            account.FullName = accountDto.FullName;
            account.DateOfBirth = accountDto.DateOfBirth;
            account.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(account);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Không thể cập nhật tài khoản: {errors}");
            }

            return account;
        }
    }
}