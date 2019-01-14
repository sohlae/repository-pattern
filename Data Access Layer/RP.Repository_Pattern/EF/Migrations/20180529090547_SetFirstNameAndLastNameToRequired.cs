using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RP.DataAccess.RepositoryPattern.EF.Migrations
{
    public partial class SetFirstNameAndLastNameToRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }
    }
}
