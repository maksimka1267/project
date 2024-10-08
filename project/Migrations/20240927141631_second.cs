using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "TextFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "93d6a084-065b-43eb-8f38-1f34c2bddf2b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f96f77a6-80c1-423d-ba98-07cddcc9bfca", "AQAAAAEAACcQAAAAEGWWvMg2lXjN+gQ8X80Et5eeOT6VsiC9oaikYMBmER0Z/WA69uQ3xj5HtZEOAev1uw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "TextFields");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "ea7a2cf3-0de1-42e4-8ae4-9d2ee8ed11bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8de18fc7-1c8c-4daa-8797-54a3dc17057a", "AQAAAAEAACcQAAAAEB5uuvNfeNHqpV8aGBNokNk952D3VPueVnl/u5XmAyXWXrHRgwBP6DkoBG8nLoNJyA==" });
        }
    }
}
