using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Data.Migrations
{
    public partial class SetNullDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5623003e-1407-403d-b369-9ff8233a9296");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c62eec01-5927-4c9d-8075-11b6b95dafaa");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c62eec01-5927-4c9d-8075-11b6b95dafaa", "807551b4-28a0-4b65-9287-8c8abd3e7c9a", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5623003e-1407-403d-b369-9ff8233a9296", "b2e3bb4e-009f-47de-b12d-473860a65526", "User", null });

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
