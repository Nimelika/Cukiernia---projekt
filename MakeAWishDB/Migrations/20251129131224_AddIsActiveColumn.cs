using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeAWishDB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__UserAcco__536C85E48A608285",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "ProductCategories");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ProductCategories",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryRegion",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryCountry",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UserAccounts_Email",
                table: "UserAccounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CelebrationCakeID",
                table: "Products",
                column: "CelebrationCakeID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryCountry",
                table: "Customers",
                column: "DeliveryCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryRegion",
                table: "Customers",
                column: "DeliveryRegion");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_DeliveryCountry",
                table: "Customers",
                column: "DeliveryCountry",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_DeliveryRegion",
                table: "Customers",
                column: "DeliveryRegion",
                principalTable: "Regions",
                principalColumn: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CelebrationCakes",
                table: "Products",
                column: "CelebrationCakeID",
                principalTable: "CelebrationCakes",
                principalColumn: "CelebrationCakeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_DeliveryCountry",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_DeliveryRegion",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CelebrationCakes",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "UQ_UserAccounts_Email",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Products_CelebrationCakeID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeliveryCountry",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeliveryRegion",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ProductCategories");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "UserAccounts",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserAccounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryRegion",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryCountry",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__UserAcco__536C85E48A608285",
                table: "UserAccounts",
                column: "Username",
                unique: true);
        }
    }
}
