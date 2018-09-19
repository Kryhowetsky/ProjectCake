using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectCake.Data.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Responds",
                table: "Responds");

            migrationBuilder.RenameTable(
                name: "Responds",
                newName: "Respond");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Respond",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Respond",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Respond",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Respond",
                table: "Respond",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Respond_ProductId",
                table: "Respond",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respond_Product_ProductId",
                table: "Respond",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respond_Product_ProductId",
                table: "Respond");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Respond",
                table: "Respond");

            migrationBuilder.DropIndex(
                name: "IX_Respond_ProductId",
                table: "Respond");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Respond");

            migrationBuilder.RenameTable(
                name: "Respond",
                newName: "Responds");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Responds",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Responds",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responds",
                table: "Responds",
                column: "Id");
        }
    }
}
