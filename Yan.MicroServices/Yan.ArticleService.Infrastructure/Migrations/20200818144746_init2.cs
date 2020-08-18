using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yan.ArticleService.Infrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_ArticleTagId",
                table: "ArticleTagRelation");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTagRelation_ArticleTagId",
                table: "ArticleTagRelation");

            migrationBuilder.DropColumn(
                name: "ArticleTagId",
                table: "ArticleTagRelation");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "ArticleTagRelation",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ArticleTag",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTagRelation_TagId",
                table: "ArticleTagRelation",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation",
                column: "TagId",
                principalTable: "ArticleTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_TagId",
                table: "ArticleTagRelation");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTagRelation_TagId",
                table: "ArticleTagRelation");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "ArticleTagRelation",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArticleTagId",
                table: "ArticleTagRelation",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ArticleTag",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTagRelation_ArticleTagId",
                table: "ArticleTagRelation",
                column: "ArticleTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTagRelation_ArticleTag_ArticleTagId",
                table: "ArticleTagRelation",
                column: "ArticleTagId",
                principalTable: "ArticleTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
