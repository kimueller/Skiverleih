using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skiverleih.Migrations
{
    public partial class LoanTimesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LoanDate",
                table: "OnLoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "OnLoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanDate",
                table: "OnLoans");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "OnLoans");
        }
    }
}
