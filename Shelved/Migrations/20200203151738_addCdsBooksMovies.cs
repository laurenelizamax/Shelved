using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelved.Migrations
{
    public partial class addCdsBooksMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Heard",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Book");

            migrationBuilder.AddColumn<bool>(
                name: "IsWatched",
                table: "Movie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHeard",
                table: "CD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc73e00d-c386-4fd0-8136-fb9a64e2c49f", "AQAAAAEAACcQAAAAEOoJjtfKbVDcTGEMPqnQrKNKQ4tiM1aZWpdU0iFxjudBvyrcSNuRNYL6Usw2BRviuw==" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "ApplicationUserId", "Author", "ImagePath", "IsRead", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "00000000-ffff-ffff-ffff-ffffffffffff", "Janet Evanovich", null, true, "One For The Money", "1994" },
                    { 2, "00000000-ffff-ffff-ffff-ffffffffffff", "Nick Hornby", null, true, "Fever Pitch", "1992" },
                    { 3, "00000000-ffff-ffff-ffff-ffffffffffff", "P.G. Wodehouse", null, true, "The Code of the Woosters", "1938" }
                });

            migrationBuilder.InsertData(
                table: "CD",
                columns: new[] { "Id", "ApplicationUserId", "Artist", "ImagePath", "IsHeard", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "00000000-ffff-ffff-ffff-ffffffffffff", "Sheryl Crow", null, true, "Sheryl Crow", "1996" },
                    { 2, "00000000-ffff-ffff-ffff-ffffffffffff", "The Offspring", null, true, "Americana", "1998" },
                    { 3, "00000000-ffff-ffff-ffff-ffffffffffff", "Foo Fighters", null, true, "In Your Honor", "2005" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "ApplicationUserId", "ImagePath", "IsWatched", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "00000000-ffff-ffff-ffff-ffffffffffff", null, true, "Dr. Doolittle", "2020" },
                    { 2, "00000000-ffff-ffff-ffff-ffffffffffff", null, true, "Captain America", "2011" },
                    { 3, "00000000-ffff-ffff-ffff-ffffffffffff", null, true, "Big Business", "1988" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CD",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CD",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CD",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "IsWatched",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "IsHeard",
                table: "CD");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Book");

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Heard",
                table: "CD",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d71feb33-9fea-4fd3-bd9e-bc2e7f22d422", "AQAAAAEAACcQAAAAEALqms3lxgnKc8oYNkM13iStwBF0h2cOqkzjB3uLNLvG/FHr0GhEUzCVXn8JyqxZEg==" });
        }
    }
}
