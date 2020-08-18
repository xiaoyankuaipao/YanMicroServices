using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.ArticleService.Infrastructure.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_Articles_ArticleId",
                table: "ArticleTagRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_Articles_ArticleId",
                table: "ArticleTagRelation",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation",
                column: "TagId",
                principalTable: "ArticleTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_Articles_ArticleId",
                table: "ArticleTagRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_Articles_ArticleId",
                table: "ArticleTagRelation",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation",
                column: "TagId",
                principalTable: "ArticleTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
