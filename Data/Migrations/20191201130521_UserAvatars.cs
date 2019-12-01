using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Data.Migrations
{
    public partial class UserAvatars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarLink",
                table: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "AvatarLink",
                table: "AspNetUsers");
        }
    }
}
