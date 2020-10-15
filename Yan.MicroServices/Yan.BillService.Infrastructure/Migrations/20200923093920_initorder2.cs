using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.BillService.Infrastructure.Migrations
{
    public partial class initorder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "orderitems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "abc",
                table: "orderitems",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "abc",
                table: "orderitems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "orderitems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
