using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.MySql;
using Microsoft.EntityFrameworkCore;

namespace api.Datas
{
    public class MySqlDBContext : DbContext
    {
        public MySqlDBContext(DbContextOptions<MySqlDBContext> dbContextOptions)
        : base(dbContextOptions)
        {
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
        public DbSet<Salary> Salaries { get; set; } = null!;

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

            modelBuilder.Entity<Attendance>()
                .Property(a => a.AttendanceID)
                .ValueGeneratedNever();

            modelBuilder.Entity<Salary>()
                .Property(s => s.SalaryID)
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

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}