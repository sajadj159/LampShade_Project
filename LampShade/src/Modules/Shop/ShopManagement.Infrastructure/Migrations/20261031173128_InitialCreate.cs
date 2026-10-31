using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shop");

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "shop",
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
                name: "Orders",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<double>(type: "double precision", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    DiscountAmount = table.Column<double>(type: "double precision", nullable: false),
                    PayAmount = table.Column<double>(type: "double precision", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    IssueTrackingNumber = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    RefId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentProofUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "shop",
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
                name: "ProductCategories",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PictureAlt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Keywords = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    MetaDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PictureUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PictureAlt = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Heading = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    BtnText = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Link = table.Column<string>(type: "text", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    DiscountRate = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "shop",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Keywords = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MetaDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", maxLength: 1000, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "shop",
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                schema: "shop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PictureUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PictureTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PictureAlt = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPictures_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "shop",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "shop",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_ProcessedOn",
                schema: "shop",
                table: "OutboxMessages",
                column: "ProcessedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ProductId",
                schema: "shop",
                table: "ProductPictures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "shop",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "ProductPictures",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "Slides",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "shop");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "shop");
        }
    }
}
