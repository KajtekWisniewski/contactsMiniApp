using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactsMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryAndSubcategoryAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Subcategories_SubcategoryId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_SubcategoryId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SubcategoryId1",
                table: "Contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId1",
                table: "Contacts",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubcategoryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubcategoryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                column: "SubcategoryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubcategoryId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubcategoryId1",
                table: "Contacts",
                column: "SubcategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Subcategories_SubcategoryId1",
                table: "Contacts",
                column: "SubcategoryId1",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
