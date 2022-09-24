using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playground.Infrastructure.Data.Migrations.Playground
{
    public partial class InitializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cover_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    publish_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag_color",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bg_color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    border_color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    text_color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_color", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    color_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_tag_tag_color_color_id",
                        column: x => x.color_id,
                        principalTable: "tag_color",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "article_tag",
                columns: table => new
                {
                    article_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_tag", x => new { x.article_id, x.tag_id });
                    table.ForeignKey(
                        name: "fk_article_tag_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_tag_tag_id",
                table: "article_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_slug",
                table: "articles",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tag_color_id",
                table: "tag",
                column: "color_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_tag");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "tag_color");
        }
    }
}
