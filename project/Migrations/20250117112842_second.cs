using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "6537176a-2b1e-48fa-9239-61dd3dc85040");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f17e2709-1782-49eb-95cf-084c03441577", "AQAAAAEAACcQAAAAEIm+Bx9y/I1R2lZjP+PgjouCjxxD2ohyR2u/gFegchidFVw+6uVa466bZQ8p72MKGQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "10386e76-1c9e-4ddb-ae27-82c003c1b6d9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9081b995-88c7-4a5b-966d-0bf205632597", "AQAAAAEAACcQAAAAENNEWgrdC+/mFh+oqN/7OE+D0mxSKiQM2wgldSnF1itkkG7y4pYcHU0jWkoC0hQHXw==" });
        }
    }
}
