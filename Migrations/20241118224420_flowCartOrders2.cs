using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirvisShopFinal.Migrations
{
    /// <inheritdoc />
    public partial class flowCartOrders2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_id",
                table: "Orders",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_id",
                table: "Carts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_id",
                table: "Carts",
                column: "id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_id",
                table: "Orders",
                column: "id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
