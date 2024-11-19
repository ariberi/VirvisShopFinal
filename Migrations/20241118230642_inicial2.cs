using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirvisShopFinal.Migrations
{
    /// <inheritdoc />
    public partial class inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Userid",
                table: "Carts",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_Userid",
                table: "Carts",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_Userid",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Userid",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Carts");
        }
    }
}
