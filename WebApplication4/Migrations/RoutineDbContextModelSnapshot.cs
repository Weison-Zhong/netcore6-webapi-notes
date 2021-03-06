// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication4.Data;

namespace WebApplication4.Migrations
{
    [DbContext(typeof(RoutineDbContext))]
    partial class RoutineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication4.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Introduction")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                            Introduction = "Google google",
                            Name = "Google"
                        },
                        new
                        {
                            Id = new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af15"),
                            Introduction = "Great",
                            Name = "Microsoft"
                        });
                });

            modelBuilder.Entity("WebApplication4.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9af7f46a-ea53-4aa3-b8c3-9fd484c2af12"),
                            CompanyId = new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                            DateOfBirth = new DateTime(1997, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "001",
                            FirstName = "ZHong",
                            Gender = 1,
                            LastName = "Weison"
                        },
                        new
                        {
                            Id = new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c2af12"),
                            CompanyId = new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"),
                            DateOfBirth = new DateTime(1998, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "002",
                            FirstName = "m",
                            Gender = 2,
                            LastName = "yl"
                        },
                        new
                        {
                            Id = new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c4af12"),
                            CompanyId = new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af15"),
                            DateOfBirth = new DateTime(1998, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "003",
                            FirstName = "m",
                            Gender = 2,
                            LastName = "yl"
                        });
                });

            modelBuilder.Entity("WebApplication4.Entities.Employee", b =>
                {
                    b.HasOne("WebApplication4.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("WebApplication4.Entities.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
