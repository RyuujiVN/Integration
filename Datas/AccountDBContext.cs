using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Datas
{
    public class AccountDBContext : IdentityDbContext<Account>
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Name = "Employee",
            NormalizedName = "EMPLOYEE"
        },
        new IdentityRole
        {
            Name = "HR",
            NormalizedName = "HR"
        },
        new IdentityRole
        {
            Name = "Payroll", 
            NormalizedName = "PAYROLL"
        },
    };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }

}