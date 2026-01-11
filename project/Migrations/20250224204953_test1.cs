using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Father",
                table: "News",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f233c4a-8b63-4ad6-9221-d6c3fae843ce",
                column: "ConcurrencyStamp",
                value: "a8e734e8-defd-4be5-ad30-e28839d37d80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05703e3c-2761-45e8-90b4-cab9d6d7dadb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "465546fd-bc71-48cd-a691-09ff18fd520a", "AQAAAAEAACcQAAAAEBsVynszkgSmC21RKtTZbH8LfuA5bsydwLXibEE0pbHPAVg9FvjclA+LXNtFL2SbHQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Father",
                table: "News",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
