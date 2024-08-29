using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bubbleTeaProject.Migrations
{
    public partial class Mogration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    tel_num = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "character(100)", fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_pkey", x => x.tel_num);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    prod_name = table.Column<string>(type: "character(50)", fixedLength: true, maxLength: 50, nullable: true),
                    prod_price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.prod_id);
                });

            migrationBuilder.CreateTable(
                name: "ordering",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    tel_num = table.Column<int>(type: "integer", nullable: true),
                    order_price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ordering_pkey", x => x.order_id);
                    table.ForeignKey(
                        name: "ordering_tel_num_fkey",
                        column: x => x.tel_num,
                        principalTable: "customer",
                        principalColumn: "tel_num");
                });

            migrationBuilder.CreateTable(
                name: "products_in_order",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prod_in_order", x => new { x.prod_id, x.order_id });
                    table.ForeignKey(
                        name: "products_in_order_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "ordering",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "products_in_order_prod_id_fkey",
                        column: x => x.prod_id,
                        principalTable: "product",
                        principalColumn: "prod_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ordering_tel_num",
                table: "ordering",
                column: "tel_num");

            migrationBuilder.CreateIndex(
                name: "IX_products_in_order_order_id",
                table: "products_in_order",
                column: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_in_order");

            migrationBuilder.DropTable(
                name: "ordering");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
