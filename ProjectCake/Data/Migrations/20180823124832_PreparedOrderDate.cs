using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectCake.Data.Migrations
{
    public partial class PreparedOrderDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PreparedOrderDate",
                table: "OrderCake",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparedOrderDate",
                table: "OrderCake");
        }
    }
}
