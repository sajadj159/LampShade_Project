using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlogManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "blog");

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ShowOrder = table.Column<int>(type: "integer", nullable: false),
                    Slug = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: true),
                    Keywords = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MetaDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CanonicalAddress = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "blog",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Handler = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => new { x.EventId, x.Handler });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Attempts = table.Column<int>(type: "integer", nullable: false),
                    Error = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ShortDescription = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Slug = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: true),
                    Keywords = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MetaDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CanonicalAddress = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "blog",
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                schema: "blog",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_ProcessedOn",
                schema: "blog",
                table: "OutboxMessages",
                column: "ProcessedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "ArticleCategories",
                schema: "blog");
        }
    }
}
