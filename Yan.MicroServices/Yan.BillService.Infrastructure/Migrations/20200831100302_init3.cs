using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.BillService.Infrastructure.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_Bill_BillId",
                table: "BillItem");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "BillItem",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_Bill_BillId",
                table: "BillItem",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_Bill_BillId",
                table: "BillItem");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "BillItem",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_Bill_BillId",
                table: "BillItem",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
