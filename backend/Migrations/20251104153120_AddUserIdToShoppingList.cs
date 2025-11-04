using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrocyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToShoppingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShoppingLists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Users_UserId",
                table: "ShoppingLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Users_UserId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingLists");

            migrationBuilder.CreateTable(
                name: "ShoppingListUser",
                columns: table => new
                {
                    ListsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SharedWithId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListUser", x => new { x.ListsId, x.SharedWithId });
                    table.ForeignKey(
                        name: "FK_ShoppingListUser_ShoppingLists_ListsId",
                        column: x => x.ListsId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListUser_Users_SharedWithId",
                        column: x => x.SharedWithId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListUser_SharedWithId",
                table: "ShoppingListUser",
                column: "SharedWithId");
        }
    }
}
