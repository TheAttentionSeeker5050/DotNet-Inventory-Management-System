using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventorium.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_ProductReferences_ProductReferenceId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReferences_ProductCategories_ProductCategoryId",
                table: "ProductReferences");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductReferences",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProductReferenceId",
                table: "ProductItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_ProductReferences_ProductReferenceId",
                table: "ProductItems",
                column: "ProductReferenceId",
                principalTable: "ProductReferences",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReferences_ProductCategories_ProductCategoryId",
                table: "ProductReferences",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_ProductReferences_ProductReferenceId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReferences_ProductCategories_ProductCategoryId",
                table: "ProductReferences");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "ProductReferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductReferenceId",
                table: "ProductItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_ProductReferences_ProductReferenceId",
                table: "ProductItems",
                column: "ProductReferenceId",
                principalTable: "ProductReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReferences_ProductCategories_ProductCategoryId",
                table: "ProductReferences",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
