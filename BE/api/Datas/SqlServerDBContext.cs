using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace api.Datas
{
    public class SqlServerDBContext : DbContext
    {
        public SqlServerDBContext(DbContextOptions<SqlServerDBContext> dbContextOptions)
        : base(dbContextOptions)
        {
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Dividend> Dividends { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(d => d.DepartmentID)
                .ValueGeneratedNever();

            modelBuilder.Entity<Position>()
                .Property(p => p.PositionID)
                .ValueGeneratedNever();

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeID)
                .ValueGeneratedNever();

            modelBuilder.Entity<Dividend>()
                .Property(d => d.DividendID)
                .ValueGeneratedNever();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany()
                .HasForeignKey(e => e.PositionID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Dividend>()
                .HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}