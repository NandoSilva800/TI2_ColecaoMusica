using Microsoft.EntityFrameworkCore.Migrations;

namespace Colecao_Musica.Data.Migrations
{
    public partial class alterFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "e5cfcf25-dbb6-4ed2-a1af-3f464694f9fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "946c86e8-0ec4-4e32-8424-9496bd7bd09d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "0674cfa0-4279-4cc6-a6f5-320728a7e7cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "f2bd7d28-8fab-421b-ac93-e5617d831853");
        }
    }
}
