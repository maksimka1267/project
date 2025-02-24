using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class update_news : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "557e4bb6-d9ac-4ee0-997c-01afc53b94b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd796031-0a03-41e6-a22d-7ecb345e97c2", "AQAAAAEAACcQAAAAELI6jbpc9eGdEU9+mIlXMi6wi6MGcY+uAU05EmfV4cdTUEdqs7ysFeCSpFH6ZIzshA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "4b209071-b494-4020-9d1b-350c33e396a1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "499f4a53-a187-4563-abd0-7e4aeef2e700", "AQAAAAEAACcQAAAAEESy6r2A9oxDVelTf3hPg7nQDqzq0V0EnK6w5PxYnDeJoDxbjnojJGned4wG6544VQ==" });
        }
    }
}
