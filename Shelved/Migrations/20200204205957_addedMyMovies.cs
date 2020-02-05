using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelved.Migrations
{
    public partial class addedMyMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6f37830-041f-43c7-b494-9e8ce57665ec", "AQAAAAEAACcQAAAAEA6+F9fZLI1l2poLZ+o1EEJ3cNEmaQwQCRUn9tn4lGM2VYM8oMTjt1kkiXJv/F8bzg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5de58abc-8d60-4c0c-a788-35d13ae70f23", "AQAAAAEAACcQAAAAEMcaZoUVMKCVtAqgay9KaySXQmkJ0LBx4leMWAb9YQqTCBubQKXAaPjdjaXOEtRNVQ==" });
        }
    }
}
