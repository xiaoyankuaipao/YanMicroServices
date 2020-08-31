using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.BillService.Infrastructure.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Person = table.Column<string>(nullable: true),
                    TotalCost = table.Column<decimal>(nullable: false),
                    BillCreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BillItemTypeEnum = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    BillId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItem_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_BillId",
                table: "BillItem",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItem");

            migrationBuilder.DropTable(
                name: "Bill");
        }
    }
}
