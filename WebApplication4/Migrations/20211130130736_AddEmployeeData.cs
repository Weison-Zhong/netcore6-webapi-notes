using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication4.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("9af7f46a-ea53-4aa3-b8c3-9fd484c2af12"), new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), new DateTime(1997, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "001", "ZHong", 1, "Weison" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c2af12"), new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af12"), new DateTime(1998, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "002", "m", 2, "yl" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c4af12"), new Guid("9af7f46a-ea52-4aa3-b8c3-9fd484c2af15"), new DateTime(1998, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "003", "m", 2, "yl" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c2af12"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("9af7f16a-ea53-4aa3-b8c3-9fd484c4af12"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("9af7f46a-ea53-4aa3-b8c3-9fd484c2af12"));
        }
    }
}
