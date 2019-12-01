using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Data.Migrations
{
    public partial class Link2PathInProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "896a7d77-0f92-4eaf-87b2-0edb481aaa3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eab0c728-3138-4d2f-868c-fd412d15dc05");

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "AvatarLink",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Memes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c62eec01-5927-4c9d-8075-11b6b95dafaa", "807551b4-28a0-4b65-9287-8c8abd3e7c9a", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5623003e-1407-403d-b369-9ff8233a9296", "b2e3bb4e-009f-47de-b12d-473860a65526", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5623003e-1407-403d-b369-9ff8233a9296");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c62eec01-5927-4c9d-8075-11b6b95dafaa");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Memes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eab0c728-3138-4d2f-868c-fd412d15dc05", "dd55ff16-6976-4e52-aed1-9d8ecbbe05a0", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "896a7d77-0f92-4eaf-87b2-0edb481aaa3f", "6d5446e0-e40e-4c98-8b71-21abddb8695e", "User", null });
        }
    }
}
