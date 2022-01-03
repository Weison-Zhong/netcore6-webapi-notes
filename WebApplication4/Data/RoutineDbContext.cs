using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebApplication4.Entities;

namespace WebApplication4.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }
        //把实体映射到数据库的表
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //做限制
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(x => x.Introduction).HasMaxLength(500);
            modelBuilder.Entity<Employee>().Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(x => x.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().HasOne(x => x.Company).WithMany(x => x.Employees).HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);//Restrict有员工的话无法删除,Cascade是级联删除
                                                  //做假数据
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                    Name = "Google",
                    Introduction = "Google google",
                },
                new Company
                {
                    Id = Guid.Parse("9af7f46a-ea52-4aa3-b8c3-9fd484c2af15"),
                    Name = "Microsoft",
                    Introduction = "Great",
                }
                ); ;
            modelBuilder.Entity<Employee>().HasData(
                                        new Employee
                                        {
                                            Id = Guid.Parse("9af7f46a-ea53-4aa3-b8c3-9fd484c2af12"),
                                            CompanyId = Guid.Parse("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                                            DateOfBirth = new DateTime(1997, 3, 29),
                                            EmployeeNo = "001",
                                            FirstName = "ZHong",
                                            LastName = "Weison",
                                            Gender = Gender.男
                                        },
                                         new Employee
                                         {
                                             Id = Guid.Parse("9af7f16a-ea53-4aa3-b8c3-9fd484c2af12"),
                                             CompanyId = Guid.Parse("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                                             DateOfBirth = new DateTime(1998, 3, 29),
                                             EmployeeNo = "002",
                                             FirstName = "m",
                                             LastName = "yl",
                                             Gender = Gender.女
                                         },
                                               new Employee
                                               {
                                                   Id = Guid.Parse("9af7f16a-ea53-4aa3-b8c3-9fd484c4af12"),
                                                   CompanyId = Guid.Parse("9af7f46a-ea52-4aa3-b8c3-9fd484c2af15"),
                                                   DateOfBirth = new DateTime(1998, 3, 29),
                                                   EmployeeNo = "003",
                                                   FirstName = "m",
                                                   LastName = "yl",
                                                   Gender = Gender.女
                                               }

                );
        }


    }
}
