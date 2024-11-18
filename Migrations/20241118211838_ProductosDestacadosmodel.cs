using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirvisShopFinal.Migrations
{
    /// <inheritdoc />
    public partial class ProductosDestacadosmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductosDescatados",
                columns: table => new
                {
                    codDestacado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosDescatados", x => x.codDestacado);
                    table.ForeignKey(
                        name: "FK_ProductosDescatados_Products_Productid",
                        column: x => x.Productid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosDescatados_Productid",
                table: "ProductosDescatados",
                column: "Productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosDescatados");
        }
    }
}
