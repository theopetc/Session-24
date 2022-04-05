using BlackCoffeeshop.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlackCoffeeshop.Model;

namespace BlackCoffeeshop.EF.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(employee => employee.ID);
            builder.Property(employee => employee.ID).ValueGeneratedOnAdd();

            builder.Property(employee => employee.Name).HasMaxLength(10);
            builder.Property(employee => employee.Surname).HasMaxLength(30);
            builder.Property(employee => employee.EmployeeType).HasConversion(employeeType => employeeType.ToString(), employeeType => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType)).HasMaxLength(15);
            builder.Property(employee => employee.SalaryPerMonth).HasPrecision(7, 2);


            builder.HasMany(employee => employee.Transactions).WithOne(transaction => transaction.Employee).HasForeignKey(transaction => transaction.EmployeeID);
        }
    }
}
