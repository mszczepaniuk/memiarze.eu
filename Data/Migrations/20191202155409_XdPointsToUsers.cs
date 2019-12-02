using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Data.Migrations
{
    public partial class XdPointsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
