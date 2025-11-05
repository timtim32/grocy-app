using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrocyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCheckedToItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBought",
                table: "Items",
                newName: "Quantity");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Items",
                newName: "IsBought");
        }
    }
}
