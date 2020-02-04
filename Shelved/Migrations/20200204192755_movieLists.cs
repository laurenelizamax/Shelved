using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelved.Migrations
{
    public partial class movieLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SeenList",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WatchList",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WishList",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HeardList",
                table: "CD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ListenList",
                table: "CD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WishList",
                table: "CD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReadItList",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReadList",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WishList",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3067b075-35a2-498d-ad9f-a52315072619", "AQAAAAEAACcQAAAAEADKw0sppSKZ8VqXzlcxemt35iS2WcYB3XYYaXr4U44wCnq3T95N6Me1ZTF/ZhCNPA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenList",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "WatchList",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "WishList",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "HeardList",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "ListenList",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "WishList",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "ReadItList",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ReadList",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "WishList",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc73e00d-c386-4fd0-8136-fb9a64e2c49f", "AQAAAAEAACcQAAAAEOoJjtfKbVDcTGEMPqnQrKNKQ4tiM1aZWpdU0iFxjudBvyrcSNuRNYL6Usw2BRviuw==" });
        }
    }
}
