using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.SystemService.Infrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemRoleMenu_SystemMenu_MenuId",
                table: "SystemRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemRoleMenu_SystemRole_RoleId",
                table: "SystemRoleMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRoleMenu_SystemMenu_MenuId",
                table: "SystemRoleMenu",
                column: "MenuId",
                principalTable: "SystemMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRoleMenu_SystemRole_RoleId",
                table: "SystemRoleMenu",
                column: "RoleId",
                principalTable: "SystemRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemRoleMenu_SystemMenu_MenuId",
                table: "SystemRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemRoleMenu_SystemRole_RoleId",
                table: "SystemRoleMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRoleMenu_SystemMenu_MenuId",
                table: "SystemRoleMenu",
                column: "MenuId",
                principalTable: "SystemMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemRoleMenu_SystemRole_RoleId",
                table: "SystemRoleMenu",
                column: "RoleId",
                principalTable: "SystemRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
