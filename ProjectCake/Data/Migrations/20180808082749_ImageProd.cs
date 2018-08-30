using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectCake.Data.Migrations
{
    public partial class ImageProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProd",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProd",
                table: "Product");
        }
    }
}