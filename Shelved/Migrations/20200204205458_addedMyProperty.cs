using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelved.Migrations
{
    public partial class addedMyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MyMovies",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MyMusic",
                table: "CD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MyBooks",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5de58abc-8d60-4c0c-a788-35d13ae70f23", "AQAAAAEAACcQAAAAEMcaZoUVMKCVtAqgay9KaySXQmkJ0LBx4leMWAb9YQqTCBubQKXAaPjdjaXOEtRNVQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyMovies",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MyMusic",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "MyBooks",
                table: "Book");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3067b075-35a2-498d-ad9f-a52315072619", "AQAAAAEAACcQAAAAEADKw0sppSKZ8VqXzlcxemt35iS2WcYB3XYYaXr4U44wCnq3T95N6Me1ZTF/ZhCNPA==" });
        }
    }
}
