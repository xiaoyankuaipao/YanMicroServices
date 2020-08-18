using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.SystemService.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemMenu",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Icon = table.Column<string>(maxLength: 255, nullable: true),
                    MenuType = table.Column<int>(nullable: false),
                    ParentId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMenu_SystemMenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SystemMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SystemRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemRoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: true),
                    MenuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemRoleMenu_SystemMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SystemMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SystemRoleMenu_SystemRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SystemRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 255, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUser_SystemRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SystemRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemMenu_ParentId",
                table: "SystemMenu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRoleMenu_MenuId",
                table: "SystemRoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRoleMenu_RoleId",
                table: "SystemRoleMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUser_RoleId",
                table: "SystemUser",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemRoleMenu");

            migrationBuilder.DropTable(
                name: "SystemUser");

            migrationBuilder.DropTable(
                name: "SystemMenu");

            migrationBuilder.DropTable(
                name: "SystemRole");
        }
    }
}
