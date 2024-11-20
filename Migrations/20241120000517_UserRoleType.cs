using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirvisShopFinal.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_roleidRole",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_roleidRole",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "roleidRole",
                table: "Users",
                newName: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "roleidRole");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.idRole);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleidRole",
                table: "Users",
                column: "roleidRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_roleidRole",
                table: "Users",
                column: "roleidRole",
                principalTable: "Role",
                principalColumn: "idRole",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
