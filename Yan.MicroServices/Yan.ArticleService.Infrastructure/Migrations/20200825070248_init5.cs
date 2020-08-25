using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.ArticleService.Infrastructure.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageAggregates",
                table: "MessageAggregates");

            migrationBuilder.RenameTable(
                name: "MessageAggregates",
                newName: "Message");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "MessageAggregates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageAggregates",
                table: "MessageAggregates",
                column: "Id");
        }
    }
}
